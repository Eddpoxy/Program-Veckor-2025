using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenuscript : MonoBehaviour

{
    public GameObject back;

    
        public void playgame()
        {
            Debug.Log("switch to next scene");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Laddar scener eller nivår för spelet
        }
    
    
    public void quitgame()
    {
        Application.Quit();

    }
}


