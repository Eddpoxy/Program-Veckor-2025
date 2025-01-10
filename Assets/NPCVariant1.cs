using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCVariant1 : NPCS
{
    protected override void Start()
    {
        base.Start();  
    }
    public override void WalkIntoScene()
    {
        base.WalkIntoScene();  
        Debug.Log("NPCVariant1 is walking differently.");
    }

    new void Update()
    {
        base.Update(); 
    }
}
