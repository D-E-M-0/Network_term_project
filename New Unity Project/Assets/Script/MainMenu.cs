using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour {

    public void GoToRegister()
    {
        SceneManager.LoadScene("RegisterScene");
    }
    public void GoToLogin()
    {
        SceneManager.LoadScene("LoginScene");
    }
}
