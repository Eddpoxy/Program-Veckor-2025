using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCS : MonoBehaviour
{
    protected int Name;
    protected List<string> Dialog;
    public Transform EntranceTransform;
    private float moveSpeed = 2f;

    protected virtual void Start()
    {
        // Ensure EntrancePosition is present in the scene
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

    protected void Update()
    {
        if (EntranceTransform != null)
        {
            WalkIntoScene();
        }
    }

    public virtual void WalkIntoScene()
    {
        if (EntranceTransform != null)
        {
            // Move towards the EntranceTransform
            transform.position = Vector3.MoveTowards(transform.position, EntranceTransform.position, moveSpeed * Time.deltaTime);

            // Optional: Stop movement when close enough
            if (Vector3.Distance(transform.position, EntranceTransform.position) < 0.1f)
            {
                Debug.Log("Reached entrance");
                // Stop any further movement or trigger another behavior
            }
        }
    }
}
