using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName= "Dialogue/DialougeObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField][TextArea] public string[] dialogue;
    [SerializeField] public Response[] responses;

    public string[] Dialogue => dialogue;

    public bool HasResponses => Responses != null && Responses.Length > 0;

    public Response[] Responses => responses;

}
