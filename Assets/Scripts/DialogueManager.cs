using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;

    [SerializeField] private float textSpeed = 50f;

    [SerializeField] private Sprite[] characterImages;

    [SerializeField] private Horse currentHorse;



    public Sprite currentImage;

    [Header("Typewriter Text Effect")]
    public TypewriterEffect typewriterEffect;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }

    public void BeginConversation()
    {
        currentHorse = HorseManager.Instance.currentHorse;
        dialoguePanel.SetActive(true);
        nameText.text = currentHorse.name;

        RunText(currentHorse.dialogue[0]);

    }


    public void EndConversation()
    {

    }




    /* Typewriter Effect */

    public void RunText(string textToType, TMP_Text textBox = null)
    {
        if (textBox == null)
        {
            textBox = dialogueText;
        }
        typewriterEffect.RunText(textToType, textBox, textSpeed);
    }
}