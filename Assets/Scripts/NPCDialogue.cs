using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialog : MonoBehaviour
{
    // Reference to the text UI element
    [SerializeField] private TextMeshProUGUI dialogText;

    // Reference to the PlayerAnswerDetector script
    [SerializeField] private Player playerAnswerDetector;

    [TextArea(2, 5)]
    public string[] dialogLines =
    {
        "Hello! How are you today?",
        "I’ve been looking for someone to sell me some scrap.",
        "It’s hard to find good traders these days.",
    };

    [TextArea(2, 5)]
    public string dialog_1 = "Hello! How are you today";
    [TextArea(2, 5)]
    public string dialog_2 = "I've been looking for someone to sell me some scrap";
    [TextArea(2, 5)]
    public string dialog_3 = "It's hard to find good traders these days";
    [TextArea(2, 5)]
    public string Question = "Will you sell me some scrap for 5 coins? (Press Y or N)";
    [TextArea(2, 5)]
    public string yesResponse = "Here you go, 5 coins!";
    [TextArea(2, 5)]
    public string noResponse = "Oh, I see. Maybe next time.";

    private int currentLineIndex = 0;
    private bool awaintingChoise = true;
    private bool playerInRange = true;

    // Initial dialog text
    [TextArea(2, 5)]
    public string initialDialog = "Will you sell me some scrap for 5 coins? (Press Y or N)";

    // Flag to prevent repeat dialog switching
    private bool hasUpdatedDialog = false;

    void Start()
    {
        // Set the initial dialog text
        if (dialogText != null)
        {
            dialogText.text = dialog_1;
            
        }
    }

    void Update()
    {
        if (playerInRange)
        {
            if(Input.GetKey(KeyCode.Space))
            {
                ProgressDialog();
            }

            if (awaintingChoise)
            {
                if (Input.GetKeyDown(KeyCode.Y))
                {
                    DisplayResponse(true);
                }
                else if (Input.GetKeyDown(KeyCode.N))
                {
                    DisplayResponse(false);
                }
            }
        }


        // Check if the player has answered and update dialog accordingly
        if (playerAnswerDetector != null && playerAnswerDetector.HasPlayerAnswered() && !hasUpdatedDialog)
        {
            // Update dialog text based on the player's answer
            if (playerAnswerDetector.GetPlayerAnswer())
            {
                dialogText.text = yesResponse; // Yes response
            }
            else
            {
                dialogText.text = noResponse; // No response
            }

            hasUpdatedDialog = true; // Ensure dialog updates only once
        }
    }

    private void ProgressDialog()
    {
        if(currentLineIndex < dialogLines.Length)
        {
            dialogText.text = dialogLines[currentLineIndex];
            currentLineIndex++;
        }
        else
        {
            Debug.Log(Question);
            awaintingChoise = true;
        }
    } 

    private void DisplayResponse(bool playerAnswer)
    {
        Debug.Log(playerAnswer ? yesResponse : noResponse);

        awaintingChoise = false;
        currentLineIndex = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Press space to talk");

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("NPC left talk zone");
        }
    }

    // Reset the dialog system for a new question (optional)
    public void ResetDialog()
    {
        if (dialogText != null)
        {
            dialogText.text = initialDialog;
        }
        hasUpdatedDialog = false;

        if (playerAnswerDetector != null)
        {
            playerAnswerDetector.ResetAnswer();
        }
    }
}
