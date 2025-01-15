using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import the TextMeshPro namespace

public class countdownTimer : MonoBehaviour
{
    public TMP_Text timerText; // Reference to the TextMeshPro Text element
    public float timeRemaining = 120; // 2 minutes in seconds
    private bool timerRunning = false;

    void Start()
    {
        timerRunning = true; // Start the timer
        UpdateTimerText();
    }

    void Update()
    {
        if (timerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerText();
            }
            else
            {
                timeRemaining = 0;
                timerRunning = false;
                UpdateTimerText();
                TimerFinished();
            }
        }
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void TimerFinished()
    {
        Debug.Log("Timer has finished!");
        // Add additional actions here, such as ending the game or triggering events.
    }
}
