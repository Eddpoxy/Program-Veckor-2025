using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCVariant4 : NPCS
{
    int FoodAmount;
    // Overriding Start to modify dialogue specific to NPCVariant1
    protected override void Start()
    {
        FoodAmount = Random.Range(2, 10);
        base.Start(); // Call base class Start to ensure EntranceTransform is set

        Dialogue = new List<string>
        {
            "Bhahaehh I giv " + FoodAmount + " pice or takke  " + FoodAmount + " pices mabe. Wan pley?",
            "Alright lets play",
            "BUhu yu no fun, bieee"
        };

    }

    // If you want to modify walk behavior, override WalkIntoScene here
    protected override void WalkIntoScene()
    {

        base.WalkIntoScene();
    }
    public override void YesReply()
    {
        if (currentTextBubble != null)
        {
            Destroy(currentTextBubble); // Destroy the existing text bubble
        }
        if (Random.value < 0.5f) // 50% chance
        {          
            GameManager.Instance.StartCoroutine(GameManager.Instance.AddFood(FoodAmount));
            Dialogue[1] = "yuoho u winnne!!!";
        } 
        else
        {
            GameManager.Instance.StartCoroutine(GameManager.Instance.RemoveFood(FoodAmount));
            Dialogue[1] = "Wooaooawo I wun! yipe kiyaye!";
        }
        ShowTextBubble(Dialogue[1]);

        // Exit after showing the dialogue
        StartCoroutine(WaitForTextAndExit());
        //ExitScene();
    }
    public override void NoReply()
    {
        GameManager.Instance.StartCoroutine(GameManager.Instance.RemoveFood(1));
        StartCoroutine(WaitForTextAndExit());
    }

}
