using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

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
    private bool isWalkingIn = false; // Add a flag to track if tween is active 
    private bool isWalkingOut = false;
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
            PlayIdleAnimation(transform); // Play idle animation while the NPC is talking
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
        if (EntranceTransform != null && !isWalkingIn)
        {
            isWalkingIn = true; // Set the flag to prevent re-triggering

            transform.DOMove(EntranceTransform.position, 2f)
                .SetEase(Ease.OutQuad) // Adjust ease for smooth entry
                .OnComplete(() =>
                {
                    DOTween.Kill(transform); // Stop tweens safely
                    Debug.Log("Reached entrance");
                    hasArrived = true; // Update arrival status
                    StartCoroutine(ShowDialogueChoices()); // Start dialogue sequence 
                    transform.rotation = Quaternion.identity;
                });

            PlayWalkAnimation(transform);

        }
    } 
    
    protected virtual void WalkOutScene()
    {
        if (SpawnTransform != null && !isWalkingOut)
        {
            isWalkingOut = true; // Set the flag to prevent re-triggering

            // Horizontal movement with bounce
            transform.DOMoveX(SpawnTransform.position.x, 2f)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    Debug.Log("NPC exited the scene");

                    DOTween.Kill(transform); // Stop tweens safely
                    if (currentTextBubble != null)
                    {
                        Destroy(currentTextBubble);
                    }
                    Destroy(gameObject); // Destroy the NPC
                });
            // Add a bouncing effect while walking out
            PlayWalkAnimation(transform);
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
    protected IEnumerator WaitForTextAndExit()
    {
        if (Dialogue.Count > 1)
        {
            // Wait until the text finishes typing
            yield return new WaitForSeconds(Dialogue[1].Length * 0.05f); // Adjust timing based on type speed
        }
        else
        {
            Debug.LogWarning("Dialogue doesn't have enough entries for proper exit timing.");
            yield return new WaitForSeconds(2f); // Provide a fallback delay if Dialogue is shorter
        }

        ExitScene(); // Exit after the dialogue is fully displayed
    }
    public void PlayWalkAnimation(Transform npcTransform)
    {
        // Bouncing animation
        npcTransform.DOLocalMoveY(npcTransform.position.y + 0.2f, 0.15f)
      .SetEase(Ease.OutQuad)
      .SetLoops(-1, LoopType.Yoyo);

        // Tilting sway animation (rotate on Z-axis back and forth)
        npcTransform.DOLocalRotate(new Vector3(0, 0, 5f), 0.3f) // Tilt right
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo) // Back and forth sway
            .SetRelative(true); // Relative to current rotation
    }
    public void PlayIdleAnimation(Transform npcTransform)
    {
        // Idle Stretch Animation (slow stretch up and down on Y-axis)
        npcTransform.DOScaleY(0.2f, 3f) // Stretch up
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo) // Yoyo to stretch back and forth
            .SetRelative(true); // Make sure it stretches relative to its original scale
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
