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

    public Sprite[] horseImages; // Array to hold the sprites for each horse


    public List<Horse> horseList; // List to hold the horses; will be shuffled from their original indexes

    public Horse currentHorse; // Current horse that's on screen
    public int currentHorseIndex; // Where in the list of horses we are on the current playthrough
    public Image currentHorseImage; // Image component of currently displayed horse


    public TextMeshProUGUI horseNameText; // TEMPORARY; DELETE LATER


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

        // Testing
        horseNameText.text = "TEST: " + horseList[0].name; // Test text delete later
        //Debug.Log("Randomized horses: " + string.Join(", ", horseList.ConvertAll(h => h.name).ToArray()));
    }


    public void NewHorse() // Goes to next horse
    {
        StartCoroutine(NewHorseCoroutine());
    }

    private IEnumerator NewHorseCoroutine()
    {
        yield return new WaitForSeconds(1f);
        // TODO: old horse fade out
        currentHorseImage.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        currentHorseImage.gameObject.SetActive(true);
        // TODO: new horse fade in

        currentHorse = horseList[currentHorseIndex];
        horseNameText.text = "TEST: " + currentHorse.name; // Update test text; delete later

        yield return new WaitForSeconds(1f);
        DialogueManager.Instance.BeginDialogue();
    }

}