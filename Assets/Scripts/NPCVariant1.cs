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
    protected override IEnumerator ShowDialogueChoices()
    {
        string previousChoice = GameManager.Instance.GetChoice(npcID);

        if (previousChoice == "No")
        {
            Debug.Log(Dialogue[0]); // Log the custom dialogue for this case
            GameManager.Instance.StartCoroutine(GameManager.Instance.RemoveFood(10)); // Take 10 food automatically
            ExitScene(); // NPC leaves the scene
            yield break; // Skip the player's choice
        }

        // Otherwise, fall back to the default choice behavior
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
