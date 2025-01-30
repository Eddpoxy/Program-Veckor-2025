using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCVariant2 : NPCS
{
 
    protected override void Start()
    {
        base.Start(); 

        Dialogue = new List<string>
        {
            "Woah you are like... Spherical man, I dont know where I am but im digging this place man. May I just stand here and sniff for a while?",
            "I found all these cool stuff on the ground here, Take them man they are freakin awesome",
            "Well ima borrow sum grub man i'll see you later"
        };
       
    }

   
    protected override void WalkIntoScene()
    {

        base.WalkIntoScene();
    }
    public override void YesReply()
    {
        
        GameManager.Instance.StartCoroutine(GameManager.Instance.AddFood(6));
        StartCoroutine(WaitForTextAndExit());
    }
    public override void NoReply()
    {
        GameManager.Instance.StartCoroutine(GameManager.Instance.RemoveFood(4));
        StartCoroutine(WaitForTextAndExit());
    }

}
