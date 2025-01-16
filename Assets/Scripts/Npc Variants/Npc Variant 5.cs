using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCVariant5 : NPCS
{

    protected override void Start()
    {
        base.Start();

        string previousChoice = GameManager.Instance.GetChoice(npcID);
        if (previousChoice == "No")
        {
            Dialogue = new List<string>
            {
                "I'm so broke right now so im just gonna steal from you now no matter what!!"
            };
        }
        else if (previousChoice == "Yes")
        {
            Dialogue = new List<string>
            {
                "Hey what's up bub, Your little investement made it big time and now I own the biggest drug cartel in TOWN!!!! This wasn't possible without your help so have some for youself"
            };
        }
        else
        {
            Dialogue = new List<string>
            {
                "Hey I'm trying to make it big in life but I need some food, can you help me? I will make it worth your while",
                "Thank you, i will be back soon!!!",
                "ok..."
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
            GameManager.Instance.StartCoroutine(GameManager.Instance.RemoveFood(8));
            yield return new WaitForSeconds(2);
            StartCoroutine(WaitForTextAndExit());
            yield break;
        }
        if (previousChoice == "Yes")
        {
            ShowTextBubble(Dialogue[0]);
            Debug.Log(Dialogue[0]);
            GameManager.Instance.StartCoroutine(GameManager.Instance.AddFood(20));
            yield return new WaitForSeconds(2);
            StartCoroutine(WaitForTextAndExit());
            yield break;
        }
        yield return base.ShowDialogueChoices();
    }

   


    public override void YesReply()
    {
        GameManager.Instance.RecordChoice(npcID, "Yes");
        GameManager.Instance.StartCoroutine(GameManager.Instance.RemoveFood(10));
        StartCoroutine(WaitForTextAndExit());
    }

    public override void NoReply()
    {
        GameManager.Instance.RecordChoice(npcID, "No");
        StartCoroutine(WaitForTextAndExit());
    }

}
