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
    [SerializeField] private GameObject Crack;
    [SerializeField] private GameObject Player;
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

        Instantiate<GameObject>(Crack, Player.transform.position, Quaternion.identity);

        SceneManager.LoadScene("first start scene");
    }
}
