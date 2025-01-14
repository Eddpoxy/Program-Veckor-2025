using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCS : MonoBehaviour
{
    protected Player player;
    public List<string> Dialogue;
    public Transform EntranceTransform;
    public Transform SpawnTransform;
    private float moveSpeed = 2f;
    private bool hasArrived = false;
    private bool isChoosing = false;
    private bool isExiting = false;
    public int npcID;
    public GameObject textBubblePrefab;
    protected GameObject currentTextBubble;

    public AudioClip TalkSound; // AudioClip for typing sounds
    private AudioSource audioSource; // AudioSource for playing sounds

    private void Awake()
    {
        // Add an AudioSource component to the NPC
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        textBubblePrefab = Resources.Load<GameObject>("TextBubble");
        player = FindAnyObjectByType<Player>();

        GameObject entranceObject = GameObject.Find("EntrancePosition");
        if (entranceObject != null)
        {
            EntranceTransform = entranceObject.transform;
        }

        GameObject SpawnObject = GameObject.Find("SpawnPosition");
        if (SpawnObject != null)
        {
            SpawnTransform = SpawnObject.transform;
        }

        string previousChoice = GameManager.Instance.GetChoice(npcID);
        if (previousChoice != null)
        {
            Debug.Log($"NPC {npcID} remembers your choice: {previousChoice}");
        }
    }

    protected void ShowTextBubble(string message)
    {
        if (textBubblePrefab == null)
        {
            Debug.LogWarning("TextBubble prefab is not assigned!");
            return;
        }

        GameObject textBubble = Instantiate(textBubblePrefab, transform.position + new Vector3(0, 2, 0), Quaternion.identity);

        TextMeshProUGUI textComponent = textBubble.GetComponentInChildren<TextMeshProUGUI>();
        if (textComponent != null)
        {
            StartCoroutine(TypeTextWithSound(message, textComponent));
        }
        else
        {
            Debug.LogWarning("Text component not found in TextBubble prefab!");
        }

        currentTextBubble = textBubble;
    }

    private IEnumerator TypeTextWithSound(string message, TextMeshProUGUI textComponent)
    {
        textComponent.text = ""; // Clear the text initially

        foreach (char letter in message.ToCharArray())
        {
            textComponent.text += letter; // Add one letter at a time
            PlayTalkSound(); // Play sound for each letter typed
            yield return new WaitForSeconds(0.05f); // Adjust typing speed here
        }
    }

    private void PlayTalkSound()
    {
        if (TalkSound == null || audioSource == null)
        {
            return;
        }

        // Slightly vary the pitch
        audioSource.pitch = Random.Range(0.9f, 1.1f); // Adjust range as needed
        audioSource.PlayOneShot(TalkSound); // Play the talk sound
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

    protected virtual void WalkIntoScene()
    {
        if (EntranceTransform != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, EntranceTransform.position, moveSpeed * Time.deltaTime);

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

            if (Vector3.Distance(transform.position, SpawnTransform.position) < 0.1f)
            {
                Debug.Log("NPC exited the scene");
                if (currentTextBubble != null)
                {
                    Destroy(currentTextBubble);
                }
                Destroy(gameObject);
            }
        }
    }

    protected virtual IEnumerator ShowDialogueChoices()
    {
        ShowTextBubble(Dialogue[0]);

        isChoosing = true;
        while (isChoosing)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                if (currentTextBubble != null)
                {
                    Destroy(currentTextBubble);
                }

                ShowTextBubble(Dialogue[1]);
                YesReply();
                isChoosing = false;
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                if (currentTextBubble != null)
                {
                    Destroy(currentTextBubble);
                }

                ShowTextBubble(Dialogue[2]);
                NoReply();
                isChoosing = false;
            }

            yield return null;
        }
    }

    public void ExitScene()
    {
        isExiting = true;
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
