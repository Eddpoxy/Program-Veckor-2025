using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCVariant1 : NPCS
{


    protected override void Start()
    {
        base.Start();

        string previousChoice = GameManager.Instance.GetChoice(npcID);
        if (previousChoice == "No")
        {
            Dialogue = new List<string>
            {
                "Beep, boop, I'm a goverment drone. All you food belongs to us"
            };
        }
        else if (previousChoice == "Yes")
        {
            Dialogue = new List<string>
            {
                "Hi again my fellow enlightened induvidual! If anything happens I can always trust you my friend"
            };
        }
        else
        {
            Dialogue = new List<string>
            {
                "I know stuff and theres something you don't know about. All of my kind are GOVERMENT DRONES!!! They know EVERYTHING and SPY on us. We are in DANGER!!! I need some emergency food NOOWW!",
                "YES! Finally someone who believes in me!! Trust me pal I got you when they gonna invade us",
                "ONE DAY MY FRIEND, OOONNNNE DAY!!! YOU BETTER BE PREPEARED!!"
            };
        }
    }

    protected override IEnumerator ShowDialogueChoices()
    {
        string previousChoice = GameManager.Instance.GetChoice(npcID);

        if (previousChoice == "No")
        {
            ShowTextBubble(Dialogue[0]);
            Debug.Log(Dialogue[0]);
            GameManager.Instance.StartCoroutine(GameManager.Instance.RemoveFood(15));
            yield return new WaitForSeconds(2);
            StartCoroutine(WaitForTextAndExit());
            yield break;
        }
        if (previousChoice == "Yes")
        {
            ShowTextBubble(Dialogue[0]);
            Debug.Log(Dialogue[0]);
            GameManager.Instance.StartCoroutine(GameManager.Instance.AddFood(3));
            yield return new WaitForSeconds(2);
            StartCoroutine(WaitForTextAndExit());
            yield break;
        }
        yield return base.ShowDialogueChoices();
    }

    public override void YesReply()
    { 
   
        GameManager.Instance.RecordChoice(npcID, "Yes"); 
        GameManager.Instance.StartCoroutine(GameManager.Instance.RemoveFood(5));
        StartCoroutine(WaitForTextAndExit());
    }

    public override void NoReply()
    {
        GameManager.Instance.RecordChoice(npcID, "No");
        StartCoroutine(WaitForTextAndExit());
    }
}
