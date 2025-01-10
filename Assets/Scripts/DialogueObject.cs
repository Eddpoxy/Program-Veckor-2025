using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName= "Dialogue/DialougeObject")]

public class DialogueObject : ScriptableObject
{
    [SerializeField][TextArea] private string[] dialogue;
    [SerializeField] private Response[] responses;

    public string[] Dialogue => dialogue;

    public Response[] Responses => responses;

}
