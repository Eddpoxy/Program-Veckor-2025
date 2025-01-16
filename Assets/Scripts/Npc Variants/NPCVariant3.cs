using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCVariant3 : NPCS
{
   
    // Overriding Start to modify dialogue specific to NPCVariant1
    protected override void Start()
    {
        base.Start(); // Call base class Start to ensure EntranceTransform is set
        string previousChoice = GameManager.Instance.GetChoice(npcID);
        if (previousChoice == "Yes")
        {
            Dialogue = new List<string>
        {
            "Thanks for watching man. I'll share a bit as a thanks. But dont mention this to ANYONE!"
        };
        }
        else
        {
            Dialogue = new List<string>
        {
            "Sup, I might done some pretty illegal stuff but I got a bunch of stuff I just need to hide for a while. Can you watch it for just a day or two?",
            "Thanks man. If you touch it you're dead",
            "Well then you are just another victim to me!"
        };
        }
       
       
    }

    // If you want to modify walk behavior, override WalkIntoScene here
    protected override void WalkIntoScene()
    {

        base.WalkIntoScene();
    }
    protected override IEnumerator ShowDialogueChoices()
    {
        string previousChoice = GameManager.Instance.GetChoice(npcID);

        if (previousChoice == "Yes")
        {
            ShowTextBubble(Dialogue[0]);
            Debug.Log(Dialogue[0]); // Log the custom dialogue for this case
            GameManager.Instance.StartCoroutine(GameManager.Instance.RemoveFood(30)); // Take 10 food automatically 
            GameManager.Instance.StartCoroutine(GameManager.Instance.AddFood(5)); // Take 10 food automatically  
            yield return new WaitForSeconds(2);
            StartCoroutine(WaitForTextAndExit());
            yield break; // Skip the player's choice
        }

        // Otherwise, fall back to the default choice behavior
        yield return base.ShowDialogueChoices();
    }
    public override void YesReply()
    {
        GameManager.Instance.RecordChoice(npcID, "Yes");
        GameManager.Instance.StartCoroutine(GameManager.Instance.AddFood(30));
        StartCoroutine(WaitForTextAndExit());
    }
    public override void NoReply()
    {
        GameManager.Instance.RecordChoice(npcID, "No");
        GameManager.Instance.StartCoroutine(GameManager.Instance.RemoveFood(10));
        StartCoroutine(WaitForTextAndExit());
    }

}
