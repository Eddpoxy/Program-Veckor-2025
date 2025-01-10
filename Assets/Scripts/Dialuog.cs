using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class Dialuog : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBow;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;
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
        foreach (string dialogue in dialogueObject.Dialogue)
        {
            yield return typewrittereffect.Run(dialogue, textLabel);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        closeDialogueBox();
    }
    private void closeDialogueBox()
    {
        dialogueBow.SetActive(false);
        textLabel.text = string.Empty;
    }

}
