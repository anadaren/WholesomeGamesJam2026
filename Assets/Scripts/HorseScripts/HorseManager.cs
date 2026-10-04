using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HorseManager : MonoBehaviour
{
    public static HorseManager Instance { get; private set; }
    public HorseJSONParser horseJSONParser;
    public MenuAnimations menuAnimations;

    public Sprite[] horseImages; // Array to hold the sprites for each horse


    public List<Horse> horseList; // List to hold the horses; will be shuffled from their original indexes

    public Horse currentHorse; // Current horse that's on screen
    public int currentHorseIndex; // Where in the list of horses we are on the CURRENT PLAYTHROUGH
    // To get the actual index of the horse, use currentHorse.index
    public Image currentHorseImage; // Image component of currently displayed horse



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

    void Start()
    {
        currentHorseImage.gameObject.SetActive(false);
        RandomizeHorses();
    }

    // Puts horses in a random order once at the beginning of the game
    // If we want to add multiple days, call function again
    void RandomizeHorses()
    {
        horseList = horseJSONParser.horseList;

        // "Knuth shuffle algorithm :: courtesy of Wikipedia :)" <-courtesy of a unity forum thread :)
        for (int t = 0; t < horseList.Count; t++)
        {
            Horse tmp = horseList[t];
            int r = UnityEngine.Random.Range(t, horseList.Count);
            horseList[t] = horseList[r];
            horseList[r] = tmp;
        }

        currentHorse = horseList[0];
        currentHorseImage.sprite = horseImages[currentHorse.index];
    }


    public void NewHorse() // Goes to next horse
    {
        StartCoroutine(NewHorseCoroutine());
    }

    private IEnumerator NewHorseCoroutine()
    {
        yield return new WaitForSeconds(1f);
        menuAnimations.HorseFadeOut(); // Old horse fades out

        yield return new WaitForSeconds(1f);
        AudioManager.Instance.PlaySFX(1); // Plays door beep

        yield return new WaitForSeconds(1f);
        currentHorse = horseList[currentHorseIndex];
        AudioManager.Instance.SwitchMusic(currentHorse.index); // Switches music to horses theme
        currentHorseImage.sprite = horseImages[currentHorse.index]; // Updates horse sprite

        currentHorseImage.gameObject.SetActive(true); // New horse fades in

        yield return new WaitForSeconds(1f);
        DialogueManager.Instance.BeginDialogue();
    }

}