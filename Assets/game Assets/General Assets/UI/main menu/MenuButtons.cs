using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("Level 1");
        Debug.Log("Loading Level 1");
    }

    public void QuitButton()
    {
        Application.Quit();
        Debug.Log("Quit Successfully");
    }



}
