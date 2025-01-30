using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameoverscene : MonoBehaviour
{
    
    public void StartGame()
    {
        SceneManager.LoadScene("Edwards Scene"); 
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}







