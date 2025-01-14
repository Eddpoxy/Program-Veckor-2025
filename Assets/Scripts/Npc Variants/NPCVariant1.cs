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
                "You said 'No' last time... I won't ask again. I'm taking 10 food now."
            };
        }
        else
        {
            Dialogue = new List<string>
            {
                "Hi! May I borrow some food? I promise I will make it worth it!",
                "Thank you so much for your kindness!",
                "How dare you!"
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
            ExitScene();
            yield break;
        }

        yield return base.ShowDialogueChoices();
    }

    public override void YesReply()
    {
        GameManager.Instance.RecordChoice(npcID, "Yes");
        GameManager.Instance.StartCoroutine(GameManager.Instance.RemoveFood(10));
        ExitScene();
    }

    public override void NoReply()
    {
        GameManager.Instance.RecordChoice(npcID, "No");
        ExitScene();
    }
}
