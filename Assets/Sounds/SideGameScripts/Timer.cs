using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Time;
    [SerializeField] private int TimeInSeconds = 60;

    private SpriteRenderer spriten;
    private int currentTime;

    private void Start()
    {
        currentTime = TimeInSeconds;

        GetComponent<SpriteRenderer>();

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
        SceneManager.LoadScene("first start scene");
    }
}
