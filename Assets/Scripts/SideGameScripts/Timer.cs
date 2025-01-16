using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Time;
    [SerializeField] private int TimeInSeconds = 90;
    private int currentTime;

    private void Start()
    {
        currentTime = TimeInSeconds;


        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        while(currentTime > 0)
        {
            Time.text = currentTime.ToString();

            yield return new WaitForSeconds(1f);

            currentTime--;
        }

        Time.text = "Time's up!";
    }
}
