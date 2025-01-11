using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCS : MonoBehaviour
{
    protected Player player;
    public List<string> Dialogue; // Dialogue list can be modified by variants
    public Transform EntranceTransform; // The entrance position NPC will walk to 
    public Transform SpawnTransform; // The exit position NPC will walk to
    private float moveSpeed = 2f; // NPC movement speed
    private bool hasArrived = false;
    private bool isChoosing = false; // Flag to track if player is choosing dialogue 
    private bool isExiting = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = FindAnyObjectByType<Player>();
        // Ensure that EntrancePosition exists in the scene
        GameObject entranceObject = GameObject.Find("EntrancePosition");
        if (entranceObject != null)
        {
            EntranceTransform = entranceObject.transform;
        } 
        
        GameObject SpawnObject = GameObject.Find("SpawnPosition");
        if (entranceObject != null)
        {
            SpawnTransform = SpawnObject.transform;
        }
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isExiting)
        {
            WalkOutScene(); // Continuously move NPC toward the SpawnTransform
            return;         // Skip the rest of the Update logic while exiting
        }

        if (EntranceTransform != null && !hasArrived)
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
                hasArrived = true;  // Set the flag to true once the NPC has arrived
                Debug.Log("Reached entrance");
                StartCoroutine(ShowDialogueChoices()); // Show dialogue choices after arrival
            }
        }
    } 
    
    protected virtual void WalkOutScene()
    {
        if (SpawnTransform != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, SpawnTransform.position, moveSpeed * Time.deltaTime);

            // Stop movement and destroy NPC when it reaches SpawnTransform
            if (Vector3.Distance(transform.position, SpawnTransform.position) < 0.1f)
            {
                Debug.Log("NPC exited the scene");
                Destroy(gameObject);
            }
        }
    } 
    

    // Coroutine to display the first dialogue and wait for the player's choice
    private IEnumerator ShowDialogueChoices()
    {
        // Display the first dialogue line
        Debug.Log(Dialogue[0]);

        // Wait for the player to press either "Y" or "N"
        isChoosing = true;  // Set the flag to indicate the player is choosing
        while (isChoosing)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                Debug.Log(Dialogue[1]);  // Show the second dialogue choice if Y is pressed 
                YesReply();             // Call the YesReply method of the current NPC
                isChoosing = false;     // Stop the loop after a choice is made
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                Debug.Log(Dialogue[2]);  // Show the third dialogue choice if N is pressed 
                NoReply();              // Call the NoReply method of the current NPC
                isChoosing = false;     // Stop the loop after a choice is made
            }

            yield return null;  // Wait for the next frame
        }
    }
    public void ExitScene()
    {
        isExiting = true; // Set the flag to begin walking out of the scene
    }
    public virtual void YesReply()
    {
        Debug.Log("Default YesReply behavior in NPCS. Override this in specific variants.");
    }

    public virtual void NoReply()
    {
        Debug.Log("Default NoReply behavior in NPCS. Override this in specific variants.");
    }

}
