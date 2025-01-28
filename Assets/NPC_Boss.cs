using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Boss : NPCS
{
    // Overriding Start to modify dialogue specific to NPCVariant1
    protected override void Start()
    {
        base.Start(); // Call base class Start to ensure EntranceTransform is set

        Dialogue = new List<string>
        {
            "Im so freakking hungry rn!!! If i don't eat i will collapse!!!",
            "OOAOAOAHAAH IM FALLING",
            "Thank you!!! I can finally continue my rampage!!"
        };

    }

    // If you want to modify walk behavior, override WalkIntoScene here
    protected override void WalkIntoScene()
    {

        base.WalkIntoScene();
    }
    public override void YesReply()
    {

        GameManager.Instance.StartCoroutine(GameManager.Instance.RemoveFood(25));
        StartCoroutine(WaitForTextAndExit());
    }
    public override void NoReply()
    {
        
        StartCoroutine(WaitForTextAndExit());
        GameManager.Instance.GameOver();
    }

}
