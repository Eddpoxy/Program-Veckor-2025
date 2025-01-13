using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenuscript : MonoBehaviour
    
{
    public GameObject back;

    void Start()
    {
  
    }
    private void playgame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); // Laddar scener eller nivår för spelet
    }
    private void quitgame()
    {
        Application.Quit();

    }
}

