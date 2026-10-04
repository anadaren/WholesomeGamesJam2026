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

    public bool duringCheckout = false; // Whether the dialogue being displayed currently is part of the intro or the checkout

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
        duringCheckout = false;
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

        if (duringCheckout)
        {
            MovieMatching.Instance.EndInteraction();
        }
    }

    public void BeginCheckoutDialogue() // Second phase of dialogue, after movie recommendation
    {
        dialogueIsPlaying = true;
        duringCheckout = true;
        currentHorse = HorseManager.Instance.currentHorse;
        dialoguePanel.SetActive(true);
        nameText.text = currentHorse.name;

        RunText(currentHorse.checkout[currSentence]);
        currSentence++;
    }

    public void ContinueCheckoutDialogue()
    {
        RunText(currentHorse.checkout[currSentence]);
        currSentence++;
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
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) { HandleAdvance(); }
    }

    private void HandleAdvance()
    {
        if (!dialogueIsPlaying) return;

        if (typewriterEffect.IsTyping)  // If text is still being typed out, press space to skip to the end of the text
        {
            typewriterEffect.SkipToEnd();
            return;
        }

        if (!duringCheckout)
        {
            if (currSentence < currentHorse.dialogue.Count) // Intro Dialogue
            {
                ContinueDialogue();
            }
            else if (currSentence >= currentHorse.dialogue.Count)
            {
                EndDialogue();
            }
        }
        else
        {
            if (currSentence < currentHorse.checkout.Count) // Checkout Dialogue
            {
                ContinueCheckoutDialogue();
            }
            else if (currSentence >= currentHorse.checkout.Count)
            {
                EndDialogue();
            }
        }

        //Debug.Log("Current sentence: " + currSentence + " / " + currentConvo.Count);
    }
}