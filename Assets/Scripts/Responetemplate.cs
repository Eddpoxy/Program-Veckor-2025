using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Responetemplate : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;

    public Dialuog dialogueui;
    public void Start()
    {
        dialogueui = GetComponent<Dialuog>();
    }

    public void ShowResponses(Response[] responses)
    {
       float responeBoxHeigth = 0;
        foreach(Response response in responses)
        {
            GameObject responeButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responeButton.gameObject.SetActive(true);
            responeButton.GetComponent<TMP_Text>().text = response.ResponseText;
            if (Input.GetKeyDown(KeyCode.Y)) 
            {
                OnpickedResponse(response);
            }
        }
   }
    public void OnpickedResponse(Response response)
    {
        dialogueui.showdDialogue(response.DialogueObject);
    }
}
