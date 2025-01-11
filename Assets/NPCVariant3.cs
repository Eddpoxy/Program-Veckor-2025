using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCVariant3 : NPCS
{
   
    // Overriding Start to modify dialogue specific to NPCVariant1
    protected override void Start()
    {
        base.Start(); // Call base class Start to ensure EntranceTransform is set

        Dialogue = new List<string>
        {
            "Hey i have a lot of food and need to hide it. Can you just watch it for a day?",
            "Thank you, DONT TOUCH IT",
            "alright fair enough"
        };
       
    }

    // If you want to modify walk behavior, override WalkIntoScene here
    protected override void WalkIntoScene()
    {

        base.WalkIntoScene();
    }
    public override void YesReply()
    {
        GameManager.Instance.StartCoroutine(GameManager.Instance.AddFood(10));
        ExitScene();
    }
    public override void NoReply()
    {

        ExitScene();
    }

}
