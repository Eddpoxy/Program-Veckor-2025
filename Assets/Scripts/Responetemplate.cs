using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Responetemplate : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;

    public Dialuog dialogueui;

    private Response[] currentResponses; // Store the current responses
    private int currentResponseIndex = 0; // Track the selected response

    public void Start()
    {
        dialogueui = GetComponent<Dialuog>();
        
    }
    public void ShowResponses(Response[] responses)
    {
        foreach(Response response in responses)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                Debug.Log("you pressed Y");
                OnpickedResponse(response);

            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                Debug.Log("you pressed N");
                currentResponseIndex++;
                if (currentResponseIndex >= currentResponses.Length)
                {
                    currentResponseIndex = 0; // Loop back to the first response
                }
            }
        }

    }
    public void OnpickedResponse(Response response)
    {
        Debug.Log("responded");
        dialogueui.showdDialogue(response.DialogueObject);
    }
    
}
