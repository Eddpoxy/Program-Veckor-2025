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
                "Im back and i need food, but I´m not asking.   (13 food)"
            };
        }
        else
        {
            Dialogue = new List<string>
            {
                "Hey im tring to make it big in life but i need som food, can yoiu help me, i will make it worth ypur while   (20)",
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
            GameManager.Instance.StartCoroutine(GameManager.Instance.RemoveFood(13));
            yield return new WaitForSeconds(2);
            ExitScene();
            yield break;
        }

        yield return base.ShowDialogueChoices();
    }

    public override void YesReply()
    {
        GameManager.Instance.RecordChoice(npcID, "Yes");
        GameManager.Instance.StartCoroutine(GameManager.Instance.RemoveFood(20));
        ExitScene();
    }

    public override void NoReply()
    {
        GameManager.Instance.RecordChoice(npcID, "No");
        ExitScene();
    }

}
