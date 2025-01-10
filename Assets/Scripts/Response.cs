using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Response 
{
    [SerializeField] private string responseText;
    [SerializeField] private DialogueObject dialogueObject;


    public string ResponseText => responseText;

    private DialogueObject DialogueObject => dialogueObject;
}
