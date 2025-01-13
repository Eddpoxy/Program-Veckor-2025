using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameoverscene : MonoBehaviour
{
    
    public void StartGame()
    {
        SceneManager.LoadScene("Edwards Scene"); // Byt ut "GameScene" mot namnet på din spelscen
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit(); // Avslutar spelet (fungerar endast i byggd version)
    }
}







