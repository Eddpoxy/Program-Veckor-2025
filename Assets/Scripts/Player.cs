using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool playerAnswer;

    private bool hasAnswered = false;

    private void Update()
    {
        if (!hasAnswered)
        {
            if (Input.GetKey(KeyCode.Y))
            {
                playerAnswer = true; //Yes
                hasAnswered = true;
                Debug.Log("Player has answered Yes");
            }
            else if (Input.GetKey(KeyCode.N))
            {
                playerAnswer = false; //No
                hasAnswered = true;
                Debug.Log("Player has answered No");
            }
        }
    }

    public bool GetPlayerAnswer()
    {
        return playerAnswer;
    }

    public void ResetAnswer()
    {
        hasAnswered = false;
    }
}
