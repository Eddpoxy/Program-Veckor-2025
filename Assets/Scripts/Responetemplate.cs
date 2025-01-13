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
   
    public void Start()
    {
        dialogueui = GetComponent<Dialuog>();
      
    }

    public void ShowResponses(Response[] responses)
    {
      
        foreach(Response response in responses)
        {
             Instantiate(responseButtonTemplate.gameObject, responseContainer);
            gameObject.SetActive(true);
            GetComponent<TMP_Text>().text = response.ResponseText;
            if (Input.GetKeyDown(KeyCode.Y)) 
            {
                OnpickedResponse(response);
            }
            else
            {
                OnpickedResponse(response);
            }
        }
        responseBox.gameObject.SetActive(true);
    }
  
    public void OnpickedResponse(Response response)
    {
        dialogueui.showdDialogue(response.DialogueObject);
    }
    
}
