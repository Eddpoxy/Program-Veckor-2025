using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;

   //public void ShowResponses(Response[] responses)
   // {
   //     float responeBoxHeigth = 0;
   //     foreach(Response response in responses)
   //     {
   //         GameObject responeButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
   //         responeButton.gameObject.SetActive(true);
   //         responeButton.GetComponent<TMP_Text>().text = response.ResponseText;
   //         responeButton.GetComponent<Button>().onClick.AddListener(call() => OnpickedResponse(response));
   //     }
   //}
    private void OnpickedResponse(Response response)
    {

    }
}
