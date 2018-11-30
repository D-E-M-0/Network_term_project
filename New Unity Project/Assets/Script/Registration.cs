using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Registration : MonoBehaviour
{

    public InputField name;
    public InputField password;
    public Button submitButton;

    public void CallRegister()
    {
        StartCoroutine(Register());
    }
    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", name.text);
        form.AddField("password", password.text);
        WWW www = new WWW("http://localhost/menu/register.php");
        yield return www;
        if(www.text == "0")
        {
            Debug.Log("User create Successfully.");
            UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");
        }
        else
        {
            Debug.Log("User creation Failed ERORR #" + www.text);
        }
    }
    public void VerifyingInputs()
    {
        submitButton.interactable = (name.text.Length >= 10 && password.text.Length >= 8);
    }
}
