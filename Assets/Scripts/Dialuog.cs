using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Dialuog : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBow;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;

    private Responetemplate responseHandler;
    private Typewrittereffect typewrittereffect;
    public mainmenuscript Mainmenuscript;

    public void Start()
    {
        typewrittereffect = GetComponent<Typewrittereffect>();
        responseHandler = GetComponent<Responetemplate>();
       
        showdDialogue(testDialogue);
        Mainmenuscript = GetComponent<mainmenuscript>();
    }

    public void showdDialogue(DialogueObject dialogueObject)//visar Dialogue
    {
        dialogueBow.SetActive(true);
        StartCoroutine(routine:StepThroughDialogue(dialogueObject));
    }
    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)//går igenom Dialogue
    {
     

        for (int i =0; i< dialogueObject.dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];
            yield return typewrittereffect.Run(dialogue, textLabel); 
            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;
            
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));//väntar tills man trycker space

        }
        
        closeDialogueBox();





    }
    private void closeDialogueBox()
    {
        Debug.Log("ending dialouge");
        dialogueBow.SetActive(false);
        textLabel.text = string.Empty;
        
           Mainmenuscript.playgame();
        
    }
   



}
