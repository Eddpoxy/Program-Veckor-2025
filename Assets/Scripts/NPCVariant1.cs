using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCVariant1 : NPCS
{
    // Overriding Start to modify dialogue specific to NPCVariant1
    protected override void Start()
    {
        base.Start(); // Call base class Start to ensure EntranceTransform is set

        Dialogue = new List<string> 
        { 
            "Hi! May I borrow some food? I promise I will make it worth it!", 
            "Thank you so much for your kindness!", 
            "How dare you!" 
        };
    }

    // If you want to modify walk behavior, override WalkIntoScene here
    protected override void WalkIntoScene()
    {
        
        base.WalkIntoScene(); 
    } 
    public void YesReply()
    {
        GameManager.Instance.StartCoroutine(GameManager.Instance.RemoveFood(10));
        ExitScene();
    }
    public void NoReply()
    {

        ExitScene();
    }

}
