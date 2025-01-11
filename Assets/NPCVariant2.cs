using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCVariant2 : NPCS
{
 
    // Overriding Start to modify dialogue specific to NPCVariant1
    protected override void Start()
    {
        base.Start(); // Call base class Start to ensure EntranceTransform is set

        Dialogue = new List<string>
        {
            "hi",
            "here",
            "damm"
        };
       
    }

    // If you want to modify walk behavior, override WalkIntoScene here
    protected override void WalkIntoScene()
    {

        base.WalkIntoScene();
    }
    public override void YesReply()
    {
        Debug.Log("NPCVariant2.YesReply called. Adding food...");
        GameManager.Instance.StartCoroutine(GameManager.Instance.AddFood(10));
        ExitScene();
    }
    public override void NoReply()
    {
        GameManager.Instance.StartCoroutine(GameManager.Instance.RemoveFood(1));
        ExitScene();
    }

}
