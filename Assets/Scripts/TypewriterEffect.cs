using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    public bool IsTyping { get; private set; }
    private string fullText;
    private TMP_Text currentTextBox;

    public void RunText(string textToType, TMP_Text textLabel, float textSpeed)
    {
        StopAllCoroutines();

        fullText = textToType;
        currentTextBox = textLabel;

        StartCoroutine(TypeText(textToType, textLabel, textSpeed));
    }

    private IEnumerator TypeText(string textToType, TMP_Text textLabel, float textSpeed)
    {
        textLabel.text = string.Empty;
        IsTyping = true;

        yield return new WaitForSeconds(.1f);

        float t = 0;
        int charIndex = 0;

        while (charIndex < textToType.Length)
        {
            t += Time.deltaTime * textSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            textLabel.text = textToType.Substring(0, charIndex);

            yield return null;
        }
        textLabel.text = textToType;
        IsTyping = false;
    }

    public void SkipToEnd()
    {
        if (!IsTyping) return;

        StopAllCoroutines();
        currentTextBox.text = fullText;
        IsTyping = false;
    }
}
