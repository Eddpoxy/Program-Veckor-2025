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
            "Woah you are like... Spherical man, I dont know where I am but im digging this place man ",
            "I found all these cool stuff on the ground here, Take them man they are freakin awesome",
            "Well ima borrow sum grub man i'll see you later"
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
        StartCoroutine(WaitForTextAndExit());
    }
    public override void NoReply()
    {
        GameManager.Instance.StartCoroutine(GameManager.Instance.RemoveFood(1));
        StartCoroutine(WaitForTextAndExit());
    }

}
