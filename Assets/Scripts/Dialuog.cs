using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class Dialuog : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBow;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;

    private Responetemplate responseHandler;
    //private void Start()
    //{
    //  textLabel.text = "hello!\nThis is my secund line";
    //}
    //private void Start()
    //{
    //GetComponent<Typewrittereffect>().Run(textToType: "This is a bit of text!\n Hello.", textLabel);
    //}
    private Typewrittereffect typewrittereffect;
    private void Start()
    {
        typewrittereffect = GetComponent<Typewrittereffect>();
        responseHandler = GetComponent<Responetemplate>();
        closeDialogueBox();
        showdDialogue(testDialogue);
    }

    public void showdDialogue(DialogueObject dialogueObject)
    {
        dialogueBow.SetActive(true);
        StartCoroutine(routine:StepThroughDialogue(dialogueObject));
    }
    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
       // yield return new WaitForSeconds(2);
        //foreach (string dialogue in dialogueObject.Dialogue)
        //{
        //    yield return typewrittereffect.Run(dialogue, textLabel);
        //    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        //}

        for (int i =0; i< dialogueObject.dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];
            yield return typewrittereffect.Run(dialogue, textLabel);
            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;
            
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            
        }

        if(dialogueObject.HasResponses)
        {
           responseHandler.ShowResponses(dialogueObject.Responses);
              
        }
        else
        {
            closeDialogueBox();
        }
        
    }
    private void closeDialogueBox()
    {
        dialogueBow.SetActive(false);
        textLabel.text = string.Empty;
    }

}
