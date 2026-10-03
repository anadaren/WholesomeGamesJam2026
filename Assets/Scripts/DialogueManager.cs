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

    public bool dialogueIsPlaying;
    public int currSentence = 0;

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

    public void BeginDialogue()
    {
        dialogueIsPlaying = true;
        currentHorse = HorseManager.Instance.currentHorse;
        dialoguePanel.SetActive(true);
        nameText.text = currentHorse.name;

        RunText(currentHorse.dialogue[currSentence]);
        currSentence++;

    }

    public void ContinueDialogue()
    {
        RunText(currentHorse.dialogue[currSentence]);
        currSentence++;
    }


    public void EndDialogue()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        nameText.text = "";
        dialogueText.text = "";
        currSentence = 0;
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


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { HandleAdvance(); }
    }

    private void HandleAdvance()
    {
        if (!dialogueIsPlaying) return;

        if (typewriterEffect.IsTyping)  // If text is still being typed out, press space to skip to the end of the text
        {
            typewriterEffect.SkipToEnd();
            return;
        }

        if (currSentence < currentHorse.dialogue.Count)
        {
            ContinueDialogue();
        }
        else if (currSentence >= currentHorse.dialogue.Count)
        {
            EndDialogue();
        }
        //Debug.Log("Current sentence: " + currSentence + " / " + currentConvo.Count);
    }
}