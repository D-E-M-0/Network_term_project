using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Photon.Pun.UtilityScripts
{
	public class PunPlayerScores : MonoBehaviour
	{
		public const string PlayerProgressProp = "prog";
		public const string PlayerEvilProp = "evil";
		public const string PlayerGoodProp = "good";
		public const string PlayerActionCount = "ac";
		public const string PlayerStress = "stress";
		public const string PlayerRestCheck = "Rest";
		public const string PlayerTurn = "playerturn";
		public const string PlayerProgressRate = "ppr";
	}

	public static class ScoreExtensions
	{
		#region Progress calculator
		public static void SetProgress(this Player player, int newProgress)
		{
			Hashtable progress = new Hashtable();  // using PUN's implementation of Hashtable
			progress[PunPlayerScores.PlayerProgressProp] = newProgress;

			player.SetCustomProperties(progress);  // this locally sets the score and will sync it in-game asap.
		}

		public static void AddProgress(this Player player, int progressToAddToCurrent)
		{
			int current = player.GetProgress();
			current = current + progressToAddToCurrent;
			if (current < 0)
				current = 0;
			if (current > 100)
				current = 100;
			Hashtable progress = new Hashtable();  // using PUN's implementation of Hashtable
			progress[PunPlayerScores.PlayerProgressProp] = current;

			player.SetCustomProperties(progress);  // this locally sets the score and will sync it in-game asap.
		}

		public static int GetProgress(this Player player)
		{
			object progress;
			if (player.CustomProperties.TryGetValue(PunPlayerScores.PlayerProgressProp, out progress))
			{
				return (int)progress;
			}
			return 0;
		}
		#endregion
		#region Evil Calculator
		public static void SetEvil(this Player player, int newEvil)
		{
			Hashtable evil = new Hashtable();  // using PUN's implementation of Hashtable
			evil[PunPlayerScores.PlayerEvilProp] = newEvil;

			player.SetCustomProperties(evil);  // this locally sets the score and will sync it in-game asap.
		}

		public static void AddEvil(this Player player, int evilToAddToCurrent)
		{
			int current = player.GetEvil();
			current = current + evilToAddToCurrent;

			if (current < 0)
				current = 0;
			if (current > 10)
				current = 10;
			Hashtable evil = new Hashtable();  // using PUN's implementation of Hashtable
			evil[PunPlayerScores.PlayerEvilProp] = current;

			player.SetCustomProperties(evil);  // this locally sets the score and will sync it in-game asap.
		}

		public static int GetEvil(this Player player)
		{
			object evil;
			if (player.CustomProperties.TryGetValue(PunPlayerScores.PlayerEvilProp, out evil))
			{
				return (int)evil;
			}

			return 0;
		}
		#endregion
		#region Good Calculator
		public static void SetGood(this Player player, int newGood)
		{
			Hashtable good = new Hashtable();  // using PUN's implementation of Hashtable
			good[PunPlayerScores.PlayerGoodProp] = newGood;

			player.SetCustomProperties(good);  // this locally sets the score and will sync it in-game asap.
		}

		public static void AddGood(this Player player, int goodToAddToCurrent)
		{
			int current = player.GetGood();
			current = current + goodToAddToCurrent;
			if (current < 0)
				current = 0;
			if (current > 10)
				current = 10;
			Hashtable good = new Hashtable();  // using PUN's implementation of Hashtable
			good[PunPlayerScores.PlayerGoodProp] = current;

			player.SetCustomProperties(good);  // this locally sets the score and will sync it in-game asap.
		}

		public static int GetGood(this Player player)
		{
			object good;
			if (player.CustomProperties.TryGetValue(PunPlayerScores.PlayerGoodProp, out good))
			{
				return (int)good;
			}

			return 0;
		}
		#endregion
		#region ActionCount
		public static void SetActionCount(this Player player, int newActionCount)
		{
			Hashtable ActionCount = new Hashtable();  // using PUN's implementation of Hashtable
			ActionCount[PunPlayerScores.PlayerActionCount] = newActionCount;

			player.SetCustomProperties(ActionCount);  // this locally sets the score and will sync it in-game asap.
		}

		public static void AddActionCount(this Player player, int acToAddToCurrent)
		{
			int current = player.GetActionCount();
			current = current + acToAddToCurrent;

			Hashtable ActionCount = new Hashtable();  // using PUN's implementation of Hashtable
			ActionCount[PunPlayerScores.PlayerActionCount] = current;

			player.SetCustomProperties(ActionCount);  // this locally sets the score and will sync it in-game asap.
		}
		public static int GetActionCount(this Player player)
		{
			object ActionCount;
			if (player.CustomProperties.TryGetValue(PunPlayerScores.PlayerActionCount, out ActionCount))
			{
				return (int)ActionCount;
			}
			return 1;
		}
		#endregion
		#region Rest
		public static void SetRest(this Player player, bool newRest)
		{
			Hashtable Rest = new Hashtable();  // using PUN's implementation of Hashtable
			Rest[PunPlayerScores.PlayerRestCheck] = newRest;
			player.SetCustomProperties(Rest);  // this locally sets the score and will sync it in-game asap.
		}
		public static bool GetRest(this Player player)
		{
			object RestChecker;
			if (player.CustomProperties.TryGetValue(PunPlayerScores.PlayerRestCheck, out RestChecker))
			{
				return (bool)RestChecker;
			}
			return false;
		}
		#endregion
		#region Stress Calculator
		public static void SetStress(this Player player, int newStress)
		{
			Hashtable Stress = new Hashtable();  // using PUN's implementation of Hashtable
			Stress[PunPlayerScores.PlayerStress] = newStress;

			player.SetCustomProperties(Stress);  // this locally sets the score and will sync it in-game asap.
		}

		public static void AddStress(this Player player, int stressToAddToCurrent)
		{
			int current = player.GetStress();
			current = current + stressToAddToCurrent;
			if (current < 0)
				current = 0;
			if (current > 100)
				current = 100;
			Hashtable Stress = new Hashtable();  // using PUN's implementation of Hashtable
			Stress[PunPlayerScores.PlayerStress] = current;

			player.SetCustomProperties(Stress);  // this locally sets the score and will sync it in-game asap.
		}

		public static int GetStress(this Player player)
		{
			object Stress;
			if (player.CustomProperties.TryGetValue(PunPlayerScores.PlayerStress, out Stress))
			{
				return (int)Stress;
			}
			return 0;
		}
		#endregion Stress Calculator
		#region Turn
		public static void SetTurn(this Player player, bool newTurn)
		{
			Hashtable turn = new Hashtable();
			turn[PunPlayerScores.PlayerTurn] = newTurn;

			player.SetCustomProperties(turn);
		}

		public static bool GetTurn(this Player player)
		{
			object turn;
			if (player.CustomProperties.TryGetValue(PunPlayerScores.PlayerTurn, out turn))
			{
				return (bool)turn;
			}
			return true;
		}
		#endregion
		#region ProgressRate
		public static void SetProgressRate(this Player player, int newProgressRate)
		{
			Hashtable ProgressRate = new Hashtable();
			ProgressRate[PunPlayerScores.PlayerProgressRate] = newProgressRate;

			player.SetCustomProperties(ProgressRate);
		}
		public static void AddProgressRate(this Player player, int pprToAddToCurrent)
		{
			int current = player.GetProgressRate();
			current = current + pprToAddToCurrent;
			if (current < 0)
				current = 0;
			if (current > 100)
				current = 100;
			Hashtable ProgressRate = new Hashtable();  // using PUN's implementation of Hashtable
			ProgressRate[PunPlayerScores.PlayerProgressRate] = current;

			player.SetCustomProperties(ProgressRate);  // this locally sets the score and will sync it in-game asap.
		}
		public static int GetProgressRate(this Player player)
		{
			object ProgressRate;
			if (player.CustomProperties.TryGetValue(PunPlayerScores.PlayerProgressRate, out ProgressRate))
			{
				return (int)ProgressRate;
			}
			return 0;
		}
		#endregion
	}
}