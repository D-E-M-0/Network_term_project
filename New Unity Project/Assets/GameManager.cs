using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour {
    public GameObject Popup;
    public GameObject Bar;
    
    private float good = 0;
    private StatusLoad status;
    private string statusPath = "Assets/test.json";

    // Use this for initialization
    void Start () {
        LoadStatus();
	}
	
	// Update is called once per frame
	void Update () {

    }
    
    public void WhenButtonClick()
    {
        Popup.gameObject.SetActive(true);
    }

    public void closeButton()
    {
        Popup.gameObject.SetActive(false);
    }

    public void Action1()
    {
        status.good += 10;

        RectTransform rt = Bar.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(10 + status.good, 42);
        rt.localPosition += Vector3.right * 5;
        //Bar.gameObject.transform.localScale = new Vector3(good, 1, 0);
        //Bar.gameObject.transform.SetPositionAndRotation(new Vector3(500 + good, 345, 0), new Quaternion());
        Popup.gameObject.SetActive(false);
    }

    [SerializeField]
    private void LoadStatus()
    {
        using (StreamReader stream = new StreamReader(statusPath))
        {
            string json = stream.ReadToEnd();
            status = JsonUtility.FromJson<StatusLoad>(json);
        }

    }
}
