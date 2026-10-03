using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HorseManager : MonoBehaviour
{
    public static HorseManager Instance { get; private set; }
    public HorseJSONParser horseJSONParser;

    public Sprite[] horseImages; // Array to hold the sprites for each horse

    public List<Horse> horseList; // List to hold the horses

    public Horse currentHorse;
    public int currentHorseIndex;


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

        // Testing
        horseNameText.text = "TEST: " + horseList[0].name; // Test text delete later
        Debug.Log("Randomized horses: " + string.Join(", ", horseList.ConvertAll(h => h.name).ToArray()));
    }


    public void NewHorse() // Goes to next horse
    {
        currentHorseIndex++;
        /*if (currentHorseIndex >= horseList.Count) // If we want to see each of the horses more than once in a day, uncomment this block
        {
            currentHorseIndex = 0; // Loop back to the first horse
        }*/

        currentHorse = horseList[currentHorseIndex];
        horseNameText.text = "TEST: " + currentHorse.name; // Update test text; delete later

        DialogueManager.Instance.BeginConversation();
    }

}