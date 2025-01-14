using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCVariant4 : NPCS
{
    int FoodAmount;
    // Overriding Start to modify dialogue specific to NPCVariant1
    protected override void Start()
    {
        FoodAmount = Random.Range(1, 10);
        base.Start(); // Call base class Start to ensure EntranceTransform is set

        Dialogue = new List<string>
        {
            "Hello, Wanna play a game? If i flip the coin and it lands on my side, ill take " + FoodAmount + " pieces of food but if it lands on the opposite ill give you the " + FoodAmount + " pieces instead",
            "Alright lets play",
            "Fine then, byeeee"
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
            Dialogue[1] = "Alright you win sucker";
        } 
        else
        {
            GameManager.Instance.StartCoroutine(GameManager.Instance.RemoveFood(FoodAmount));
            Dialogue[1] = "HahAHAH I WINNN!! you SUCK!!!";
        }
        ShowTextBubble(Dialogue[1]);

        // Exit after showing the dialogue
        StartCoroutine(WaitForTextAndExit());
        //ExitScene();
    }
    public override void NoReply()
    {
        GameManager.Instance.StartCoroutine(GameManager.Instance.RemoveFood(1));
        ExitScene();
    }

}
