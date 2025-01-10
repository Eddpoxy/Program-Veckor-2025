using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCS : MonoBehaviour
{

    public List<string> Dialogue; // Dialogue list can be modified by variants
    public Transform EntranceTransform; // The entrance position NPC will walk to
    private float moveSpeed = 2f; // NPC movement speed

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // Ensure that EntrancePosition exists in the scene
        GameObject entranceObject = GameObject.Find("EntrancePosition");
        if (entranceObject != null)
        {
            EntranceTransform = entranceObject.transform;
        }
        else
        {
            Debug.LogError("EntrancePosition not found in the scene!");
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // If EntranceTransform is available, make NPC walk towards it
        if (EntranceTransform != null)
        {
            WalkIntoScene();
        }
    }

    // NPC walks toward the entrance position
    protected virtual void WalkIntoScene()
    {
        if (EntranceTransform != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, EntranceTransform.position, moveSpeed * Time.deltaTime);

            // Stop movement when close enough
            if (Vector3.Distance(transform.position, EntranceTransform.position) < 0.1f)
            {
                Debug.Log("Reached entrance");
            }
        }
    }

    // Display the first line of dialogue (can be overridden by variants)
    public virtual void TypeDialogue()
    {
        if (Dialogue.Count > 0)
        {
            Debug.Log(Dialogue[0]);
        }
        else
        {
            Debug.LogWarning("No dialogue available.");
        }
    }

}
