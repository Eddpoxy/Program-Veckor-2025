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
                "Im back and i need food, but I´m not asking. (10 food)"
            };
        }
        else if (previousChoice == "Yes")
        {
            Dialogue = new List<string>
            {
                "Im back and Thank again for the food, i have earned som food and want to pay you back. (50 food)"
            };
        }
        else
        {
            Dialogue = new List<string>
            {
                "Hey im trying to make it big in life but i need some food, can you help me, i will make it worth your while   (20)",
                "Thank you, i will be back",
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
            GameManager.Instance.StartCoroutine(GameManager.Instance.RemoveFood(10));
            yield return new WaitForSeconds(2);
            StartCoroutine(WaitForTextAndExit());
            yield break;
        }
        if (previousChoice == "Yes")
        {
            ShowTextBubble(Dialogue[0]);
            Debug.Log(Dialogue[0]);
            GameManager.Instance.StartCoroutine(GameManager.Instance.AddFood(50));
            yield return new WaitForSeconds(2);
            StartCoroutine(WaitForTextAndExit());
            yield break;
        }
        yield return base.ShowDialogueChoices();
    }

   


    public override void YesReply()
    {
        GameManager.Instance.RecordChoice(npcID, "Yes");
        GameManager.Instance.StartCoroutine(GameManager.Instance.RemoveFood(20));
        StartCoroutine(WaitForTextAndExit());
    }

    public override void NoReply()
    {
        GameManager.Instance.RecordChoice(npcID, "No");
        StartCoroutine(WaitForTextAndExit());
    }

}
