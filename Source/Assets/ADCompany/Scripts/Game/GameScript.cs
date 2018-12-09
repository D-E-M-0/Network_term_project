using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

namespace Photon.Pun.Demo.Asteroids
{
	public class GameScript : MonoBehaviourPunCallbacks
	{
		#region GameObject
		// each player has each object
		public GameObject Player_A_n;
		public GameObject Player_B_n;
		public GameObject Progress_b_A;
		public GameObject Progress_b_B;
		public GameObject Progress_A;
		public GameObject Progress_B;

		public GameObject StressGauge1;
		public GameObject GoodGauge1;
		public GameObject EvilGauge1;
		public GameObject PRGauge1;
		public GameObject StressGauge2;
		public GameObject GoodGauge2;
		public GameObject EvilGauge2;
		public GameObject PRGauge2;

		public GameObject TurnEndActor1;
		public GameObject TurnEndActor2;

		public GameObject ActorAction1;
		public GameObject ActorAction2;
		public GameObject Action_rest;
		public GameObject Action_work;

		public GameObject ActorUpgrade1;
		public GameObject ActorUpgrade2;
		public GameObject Upgrade_practice;
		public GameObject Upgrade_traing;

		public GameObject ActorDistract1;
		public GameObject ActorDistract2;
		public GameObject Distract_distract;
		public GameObject Distract_addWork;
		public GameObject ActorDistract_Evil_Skill;

		public GameObject ActionPopup;
		public GameObject UpgradePopup;
		public GameObject DistractPopup;

		public GameObject GameSetPanel;
		public Text whoWin;
		public GameObject BackToLobby;

		public GameObject[] StatusInfo = new GameObject[8];
		#endregion

		//infomation of player
		private Dictionary<int, GameObject> playerListEntries;

		//for '연수'
		private bool trainingFlagA = false, trainingFlagB = false;
		float oneTurnTime = 60.0f; // set time limit in one turn
		#region UNITY
		public void Awake()
		{
			playerListEntries = new Dictionary<int, GameObject>();
			// player A's nickname and work progress
			Player_A_n.GetComponent<Text>().color = AsteroidsGame.GetColor(PhotonNetwork.PlayerList[0].GetPlayerNumber());
			Player_A_n.GetComponent<Text>().text = string.Format("{0}\nProgress : {1}", PhotonNetwork.PlayerList[0].NickName, PhotonNetwork.PlayerList[0].GetProgress());
			playerListEntries.Add(PhotonNetwork.PlayerList[0].ActorNumber, Player_A_n);

			// player B
			Player_B_n.GetComponent<Text>().color = AsteroidsGame.GetColor(PhotonNetwork.PlayerList[1].GetPlayerNumber());
			Player_B_n.GetComponent<Text>().text = string.Format("{0}\nProgress : {1}", PhotonNetwork.PlayerList[1].NickName, PhotonNetwork.PlayerList[1].GetProgress());
			playerListEntries.Add(PhotonNetwork.PlayerList[1].ActorNumber, Player_B_n);

			// host player get first turn
			PhotonNetwork.PlayerList[0].SetTurn(true);
			PhotonNetwork.PlayerList[1].SetTurn(false);

			if (PhotonNetwork.LocalPlayer.ActorNumber == 1) // active host player's object
			{
				TurnEndActor1.SetActive(true);
				ActorAction1.SetActive(true);
				ActorUpgrade1.SetActive(true);
				ActorDistract1.SetActive(true);
				StressGauge1.SetActive(true);
				GoodGauge1.SetActive(true);
				EvilGauge1.SetActive(true);
				PRGauge1.SetActive(true);
				for (int i = 0; i < 4; i++)
					StatusInfo[i].SetActive(true);
			}
			else // other player
			{
				TurnEndActor2.SetActive(true);
				ActorAction2.SetActive(true);
				ActorUpgrade2.SetActive(true);
				ActorDistract2.SetActive(true);
				StressGauge2.SetActive(true);
				GoodGauge2.SetActive(true);
				EvilGauge2.SetActive(true);
				PRGauge2.SetActive(true);
				for (int i = 4; i < 8; i++)
					StatusInfo[i].SetActive(true);
			}

		}
		#region TurnButton
		public void TurnButtonClick() // when turn end button clicked
		{
			// disable every button
			ActionPopup.SetActive(false);
			UpgradePopup.SetActive(false);
			DistractPopup.SetActive(false);

			/* player get 5 stress in each turn
			 * LocalPlayer is current user */
			PhotonNetwork.LocalPlayer.AddStress(5);
			PhotonNetwork.LocalPlayer.AddProgress(PhotonNetwork.LocalPlayer.GetProgressRate());


			if (PhotonNetwork.LocalPlayer.GetTurn()) // check isMyTurn
			{
				if (PhotonNetwork.LocalPlayer.ActorNumber == 1) // if current turn is host player
				{
					#region PlayerList 1
					PhotonNetwork.PlayerList[0].SetTurn(false);
					if (trainingFlagA)
					{
						PhotonNetwork.PlayerList[0].SetActionCount(0);
						PhotonNetwork.PlayerList[0].SetRest(true);
					}
					else
						PhotonNetwork.PlayerList[0].SetActionCount(2);

					#endregion
					#region PlayerList 2
					if (PhotonNetwork.PlayerList[1].GetGood() >= 3)
					{
						PhotonNetwork.PlayerList[1].AddProgress(2);
						PhotonNetwork.PlayerList[1].AddProgressRate(2);
					}
					PhotonNetwork.PlayerList[1].SetTurn(true);
					if (PhotonNetwork.PlayerList[1].GetActionCount() == 0)
					{
						if (PhotonNetwork.PlayerList[1].GetRest())
						{
							PhotonNetwork.PlayerList[1].SetRest(false);
						}
					}
					trainingFlagA = false;
					#endregion
				}
				else // other player turn
				{
					#region PlayerList 2
					PhotonNetwork.PlayerList[1].SetTurn(false);
					if (trainingFlagB)
					{
						PhotonNetwork.PlayerList[1].SetActionCount(0);
						PhotonNetwork.PlayerList[1].SetRest(true);
					}
					else
					{ PhotonNetwork.PlayerList[1].SetActionCount(2); }
					#endregion
					#region PlayerList 1
					if (PhotonNetwork.PlayerList[0].GetGood() >= 3)
					{
						PhotonNetwork.PlayerList[0].AddProgress(2);
						PhotonNetwork.PlayerList[0].AddProgressRate(2);
					}
					PhotonNetwork.PlayerList[0].SetTurn(true);
					if (PhotonNetwork.PlayerList[0].GetActionCount() == 0)
					{
						if (PhotonNetwork.PlayerList[0].GetRest())
						{
							PhotonNetwork.PlayerList[0].SetRest(false);
						}
					}
					trainingFlagB = false;
					#endregion
				}
			}
		}
		#endregion
		#region PopupCloseButton
		public void PopupCloseButton() // close popup
		{
			ActionPopup.SetActive(false);
			UpgradePopup.SetActive(false);
			DistractPopup.SetActive(false);
		}
		#endregion
		#region ActionButtonClick
		public void ActionButtonClick() // when '행동' button click
		{
			if (PhotonNetwork.LocalPlayer.GetTurn())
			{
				if (ActionPopup.activeSelf)
					ActionPopup.SetActive(false);
				else
					ActionPopup.SetActive(true);

				UpgradePopup.SetActive(false);
				DistractPopup.SetActive(false);
			}
		}
		public void RestButton() // '휴식'
		{
			if (PhotonNetwork.LocalPlayer.GetActionCount() > 0)
			{
				PhotonNetwork.LocalPlayer.AddStress(-10);
				PhotonNetwork.LocalPlayer.AddActionCount(-1);
			}
			ActionPopup.SetActive(false);
		}

		public void WorkButton() // '업무'
		{
			if (PhotonNetwork.LocalPlayer.GetActionCount() > 0)
			{
				PhotonNetwork.LocalPlayer.AddProgress(3);
				PhotonNetwork.LocalPlayer.AddActionCount(-1);
			}
			ActionPopup.SetActive(false);
		}

		#endregion
		#region UpgradeButtonClick
		public void UpgradeButtonClick() // when '자기관리' button clicked
		{
			if (PhotonNetwork.LocalPlayer.GetTurn())
			{
				if (UpgradePopup.activeSelf)
					UpgradePopup.SetActive(false);
				else
					UpgradePopup.SetActive(true);

				ActionPopup.SetActive(false);
				DistractPopup.SetActive(false);
			}
		}

		public void PracticeButton() // '연습'
		{
			if (PhotonNetwork.LocalPlayer.GetActionCount() > 0)
			{
				PhotonNetwork.LocalPlayer.SetProgressRate(
					PhotonNetwork.LocalPlayer.GetProgressRate() + 2);
				PhotonNetwork.LocalPlayer.AddActionCount(-1);
				PhotonNetwork.LocalPlayer.AddGood(1);
				PhotonNetwork.LocalPlayer.AddEvil(-1);
				UpgradePopup.SetActive(false);
			}
		}



		public void TrainingButton() // '연수'
		{
			if (PhotonNetwork.LocalPlayer.GetActionCount() > 1)
			{
				PhotonNetwork.LocalPlayer.AddProgressRate(15);
				if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
					trainingFlagA = true;
				else
					trainingFlagB = true;
				PhotonNetwork.LocalPlayer.AddActionCount(-2);
				PhotonNetwork.LocalPlayer.AddGood(1);
				PhotonNetwork.LocalPlayer.AddEvil(-1);
				UpgradePopup.SetActive(false);
			}
		}

		#endregion
		#region DistractButtonClick
		public void DistractButtonClick() // when '방해' button clicked
		{
			if (PhotonNetwork.LocalPlayer.GetTurn())
			{
				if (DistractPopup.activeSelf)
					DistractPopup.SetActive(false);
				else
					DistractPopup.SetActive(true);

				ActionPopup.SetActive(false);
				UpgradePopup.SetActive(false);
			}
			if (PhotonNetwork.LocalPlayer.GetEvil() >= 5)
				ActorDistract_Evil_Skill.SetActive(true);
			else
				ActorDistract_Evil_Skill.SetActive(false);
		}

		public void DistactButton() // '방해'
		{
			if (PhotonNetwork.LocalPlayer.GetActionCount() > 0)
			{
				if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
				{
					PhotonNetwork.PlayerList[1].AddProgressRate(-2);
				}
				else
				{
					PhotonNetwork.PlayerList[0].AddProgressRate(-2);
				}
				PhotonNetwork.LocalPlayer.AddActionCount(-1);
				PhotonNetwork.LocalPlayer.AddGood(-1);
				PhotonNetwork.LocalPlayer.AddEvil(1);
			}
			DistractPopup.SetActive(false);
		}

		public void AddWorkButton() // (상대)'업무 추가'
		{
			if (PhotonNetwork.LocalPlayer.GetActionCount() > 0)
			{
				if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
				{
					PhotonNetwork.PlayerList[1].AddProgress(-2);
				}
				else
				{
					PhotonNetwork.PlayerList[0].AddProgress(-2);
				}
				PhotonNetwork.LocalPlayer.AddActionCount(-1);
				PhotonNetwork.LocalPlayer.AddGood(-1);
				PhotonNetwork.LocalPlayer.AddEvil(1);
			}
			DistractPopup.SetActive(false);
		}
		public void DevilSkillButton() // unique skill : if evil stat is over 4, the button in active
		{
			if (PhotonNetwork.LocalPlayer.GetActionCount() > 0)
			{
				if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
				{
					PhotonNetwork.PlayerList[1].AddProgress(-5);
					PhotonNetwork.PlayerList[1].AddProgressRate(-5);
				}
				else
				{
					PhotonNetwork.PlayerList[0].AddProgress(-5);
					PhotonNetwork.PlayerList[0].AddProgressRate(-5);
				}
				PhotonNetwork.LocalPlayer.AddActionCount(-1);
				PhotonNetwork.LocalPlayer.AddEvil(1);
				PhotonNetwork.LocalPlayer.AddGood(-1);
			}
			DistractPopup.SetActive(false);
		}
		#endregion

		public void OpenLobby() // open lobby scene
		{
			SceneManager.LoadSceneAsync("LobbyScene");
			PhotonNetwork.LeaveRoom();
		}
		#endregion
		#region Data Update


		public override void OnPlayerLeftRoom(Player otherPlayer) // when player left the room, remove the data
		{
			Destroy(playerListEntries[otherPlayer.ActorNumber].gameObject);
			playerListEntries.Remove(otherPlayer.ActorNumber);
		}

		public void Update()
		{
			// turn time limit
			if (PhotonNetwork.LocalPlayer.GetTurn())
			{
				oneTurnTime -= Time.deltaTime;
				if (oneTurnTime <= 0.0f)
				{
					TurnButtonClick();
					oneTurnTime = 60.0f;
				}
			}


			StatusInfo[0].GetComponent<Text>().text = string.Format("Progress Rate : {0}", PhotonNetwork.PlayerList[0].GetProgressRate());
			StatusInfo[1].GetComponent<Text>().text = string.Format("Stress : {0}", PhotonNetwork.PlayerList[0].GetStress());
			StatusInfo[2].GetComponent<Text>().text = string.Format("Goodness : {0}", PhotonNetwork.PlayerList[0].GetGood());
			StatusInfo[3].GetComponent<Text>().text = string.Format("Vicious : {0}", PhotonNetwork.PlayerList[0].GetEvil());
			StatusInfo[4].GetComponent<Text>().text = string.Format("Progress Rate : {0}", PhotonNetwork.PlayerList[1].GetProgressRate());
			StatusInfo[5].GetComponent<Text>().text = string.Format("Stress : {0}", PhotonNetwork.PlayerList[1].GetStress());
			StatusInfo[6].GetComponent<Text>().text = string.Format("Goodness : {0}", PhotonNetwork.PlayerList[1].GetGood());
			StatusInfo[7].GetComponent<Text>().text = string.Format("Vicious : {0}", PhotonNetwork.PlayerList[1].GetEvil());
		}

		// run when the playerListEntries is updated
		public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
		{
			GameObject entry;
			if (playerListEntries.TryGetValue(targetPlayer.ActorNumber, out entry))
			{
				entry.GetComponent<Text>().text = string.Format("{0}", targetPlayer.NickName);

				// update gauge
				if (targetPlayer.ActorNumber == 1)
				{
					Progress_A.GetComponent<Image>().fillAmount = 0.01f * targetPlayer.GetProgress();
					StressGauge1.GetComponent<Image>().fillAmount = 0.01f * targetPlayer.GetStress();
					GoodGauge1.GetComponent<Image>().fillAmount = 0.1f * targetPlayer.GetGood();
					EvilGauge1.GetComponent<Image>().fillAmount = 0.1f * targetPlayer.GetEvil();
					PRGauge1.GetComponent<Image>().fillAmount = 0.01f * targetPlayer.GetProgressRate();
				}
				else
				{
					Progress_B.GetComponent<Image>().fillAmount = 0.01f * targetPlayer.GetProgress();
					StressGauge2.GetComponent<Image>().fillAmount = 0.01f * targetPlayer.GetStress();
					GoodGauge2.GetComponent<Image>().fillAmount = 0.1f * targetPlayer.GetGood();
					EvilGauge2.GetComponent<Image>().fillAmount = 0.1f * targetPlayer.GetEvil();
					PRGauge2.GetComponent<Image>().fillAmount = 0.01f * targetPlayer.GetProgressRate();
				}
				#region TurnEnd Button
				// disable/enable the buttons
				#region targetPlayer.ActorNumber == 1
				if (targetPlayer.GetTurn() && targetPlayer.ActorNumber == 1)
				{
					if (targetPlayer.GetActionCount() > 0)
					{
						TurnEndActor1.GetComponent<Button>().interactable = true;
						ActorAction1.GetComponent<Button>().interactable = true;
						ActorUpgrade1.GetComponent<Button>().interactable = true;
						ActorDistract1.GetComponent<Button>().interactable = true;
					}
					else
					{
						TurnEndActor1.GetComponent<Button>().interactable = true;
						ActorAction1.GetComponent<Button>().interactable = false;
						ActorUpgrade1.GetComponent<Button>().interactable = false;
						ActorDistract1.GetComponent<Button>().interactable = false;
					}
				}
				else if ((!targetPlayer.GetTurn()) && targetPlayer.ActorNumber == 1)
				{
					TurnEndActor1.GetComponent<Button>().interactable = false;
					ActorAction1.GetComponent<Button>().interactable = false;
					ActorUpgrade1.GetComponent<Button>().interactable = false;
					ActorDistract1.GetComponent<Button>().interactable = false;
				}
				#endregion
				#region targetPlayer.ActorNumber == 2
				if (targetPlayer.GetTurn() && targetPlayer.ActorNumber == 2)
				{
					if (targetPlayer.GetActionCount() > 0)
					{
						TurnEndActor2.GetComponent<Button>().interactable = true;
						ActorAction2.GetComponent<Button>().interactable = true;
						ActorUpgrade2.GetComponent<Button>().interactable = true;
						ActorDistract2.GetComponent<Button>().interactable = true;
					}
					else
					{
						TurnEndActor2.GetComponent<Button>().interactable = true;
						ActorAction2.GetComponent<Button>().interactable = false;
						ActorUpgrade2.GetComponent<Button>().interactable = false;
						ActorDistract2.GetComponent<Button>().interactable = false;
					}
				}
				else if ((!targetPlayer.GetTurn()) && targetPlayer.ActorNumber == 2)
				{
					TurnEndActor2.GetComponent<Button>().interactable = false;
					ActorAction2.GetComponent<Button>().interactable = false;
					ActorUpgrade2.GetComponent<Button>().interactable = false;
					ActorDistract2.GetComponent<Button>().interactable = false;
				}
				#endregion

				// game finish trigger
				if (PhotonNetwork.PlayerList[0].GetProgress() >= 100)
				{
					whoWin.text = PhotonNetwork.PlayerList[0].NickName + "님이 승리하셨습니다!";
					GameSetPanel.SetActive(true);
				}
				else if (PhotonNetwork.PlayerList[1].GetProgress() >= 100)
				{
					whoWin.text = PhotonNetwork.PlayerList[1].NickName + "님이 승리하셨습니다!";
					GameSetPanel.SetActive(true);
				}
				#endregion
			}
		}
		#endregion
	}
}