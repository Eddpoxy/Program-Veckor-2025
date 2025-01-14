using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseCanvas;
    bool Paused = false;

    // Update is called once per frame 
    private void Start()
    {
        PauseCanvas.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        { 
            if (Paused == true)
            {
                PauseCanvas.SetActive(false);
                Paused = false;
                Time.timeScale = 1;
            } 
            else
            {
                PauseCanvas.SetActive(true);
                Paused = true;
                Time.timeScale = 0;
            }
           
        } 
    } 
    public void MainMenu()
    {
        SceneManager.LoadScene("Leon Scene");
    }
    public void Return()
    {
        PauseCanvas.SetActive(false);
        Time.timeScale = 1;
        Paused = false;
    }
} 
