using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCVariant1 : NPCS
{
    // Overriding Start to modify dialogue specific to NPCVariant1
    protected override void Start()
    {
        base.Start(); // Call base class Start to ensure EntranceTransform is set

        // Set custom dialogue for this variant
        Dialogue = new List<string> { "Hi! May I borrow some food? I promise I will make it worth it!" };

        // Optionally, call TypeDialogue() if you want to display dialogue immediately
        TypeDialogue();
    }

    // If you want to modify walk behavior, override WalkIntoScene here
    protected override void WalkIntoScene()
    {
        // Optional: You can add special movement behavior for this variant here
        base.WalkIntoScene(); // Still calls the base NPC walk behavior
    }

}
