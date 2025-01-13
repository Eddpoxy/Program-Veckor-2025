using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCVariant1 : NPCS
{

    // Overriding Start to modify dialogue specific to NPCVariant1
    protected override void Start()
    {
        base.Start(); // Call base class Start to ensure EntranceTransform is set

        string previousChoice = GameManager.Instance.GetChoice(npcID);
        if (previousChoice == "No")
        {
            Dialogue = new List<string>
            {
                "You said 'No' last time... I won't ask again. I'm taking 10 food now."
            };
        }
        else
        {
            Dialogue = new List<string>
            {
                "Hi! May I borrow some food? I promise I will make it worth it!",
                "Thank you so much for your kindness!",
                "How dare you!"
            };
        }
    }

    // If you want to modify walk behavior, override WalkIntoScene here
    protected override void WalkIntoScene()
    {
        base.WalkIntoScene();
    }

    // Modified ShowDialogueChoices to show text bubble when NPC remembers previous choices
    protected override IEnumerator ShowDialogueChoices()
    {
        string previousChoice = GameManager.Instance.GetChoice(npcID);

        if (previousChoice == "No")
        {
            // Show the custom dialogue when the NPC remembers a previous choice
            ShowTextBubble(Dialogue[0]);  // Show text bubble with the dialogue about "No"
            Debug.Log(Dialogue[0]); // Log the custom dialogue for this case
            GameManager.Instance.StartCoroutine(GameManager.Instance.RemoveFood(10)); // Take 10 food automatically
            yield return new WaitForSeconds(2);  // Wait for the text to be visible
            ExitScene();  // NPC leaves the scene after a set time
            yield break;  // Skip further dialogue choices and exit
        }

        // Otherwise, fall back to the default choice behavior if no previous choice or a different choice
        yield return base.ShowDialogueChoices();
    }

    public override void YesReply()
    {
        GameManager.Instance.RecordChoice(npcID, "Yes");
        GameManager.Instance.StartCoroutine(GameManager.Instance.RemoveFood(10));
        ExitScene();
    }

    public override void NoReply()
    {
        GameManager.Instance.RecordChoice(npcID, "No");
        ExitScene();
    }
}
