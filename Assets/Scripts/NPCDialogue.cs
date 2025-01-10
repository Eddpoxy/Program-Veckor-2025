using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialog : MonoBehaviour
{
    // Reference to the text UI element
    [SerializeField] private TMP_Text dialogText;

    // Reference to the PlayerAnswerDetector script
    [SerializeField] private Player playerAnswerDetector;

    // Dialog lines for Yes and No responses
    [TextArea(2, 5)]
    public string yesResponse = "Thank you for agreeing to help!";
    [TextArea(2, 5)]
    public string noResponse = "Oh, I see. Maybe next time.";

    // Initial dialog text
    [TextArea(2, 5)]
    public string initialDialog = "Will you help me on my quest? (Press Y or N)";

    // Flag to prevent repeat dialog switching
    private bool hasUpdatedDialog = false;

    void Start()
    {
        // Set the initial dialog text
        if (dialogText != null)
        {
            dialogText.text = initialDialog;
        }
    }

    void Update()
    {
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
