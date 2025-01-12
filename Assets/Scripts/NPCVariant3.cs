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
            "Thanks for watching the food for me! As a thanks i'll give you some extra scrap i found today"
        };
        }
        else
        {
            Dialogue = new List<string>
        {
            "Hey i have a lot of food and need to hide it. Can you just watch it for a day?",
            "Thank you, DONT TOUCH IT",
            "alright fair enough"
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
            Debug.Log(Dialogue[0]); // Log the custom dialogue for this case
            GameManager.Instance.StartCoroutine(GameManager.Instance.RemoveFood(10)); // Take 10 food automatically 
            GameManager.Instance.StartCoroutine(GameManager.Instance.AddFood(5)); // Take 10 food automatically  
            yield return new WaitForSeconds(2);
            ExitScene(); // NPC leaves the scene
            yield break; // Skip the player's choice
        }

        // Otherwise, fall back to the default choice behavior
        yield return base.ShowDialogueChoices();
    }
    public override void YesReply()
    {
        GameManager.Instance.RecordChoice(npcID, "Yes");
        GameManager.Instance.StartCoroutine(GameManager.Instance.AddFood(10));
        ExitScene();
    }
    public override void NoReply()
    {
        GameManager.Instance.RecordChoice(npcID, "No");
        ExitScene();
    }

}
