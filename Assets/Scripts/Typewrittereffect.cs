using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Typewrittereffect : MonoBehaviour
{
    [SerializeField] private float TypewritterSpeed = 50f;

    public Coroutine Run(string textToType, TMP_Text textLabel)
    {
        return StartCoroutine(routine: TypeText(textToType, textLabel));
    }

    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        textLabel.text = string.Empty;
       // yield return new WaitForSeconds(2);
        float t = 0;
        int charIndex = 0;

        while (charIndex < textToType.Length)
        {
            t += Time.deltaTime * TypewritterSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(value: charIndex, min: 0, max: textToType.Length);

            textLabel.text= textToType.Substring(startIndex: 0, length: charIndex);

            yield return null;
        }
        textLabel.text = textToType;
    }
}
