using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TapeManager : MonoBehaviour
{
    public static TapeManager Instance { get; private set; }
    public TapeJSONParser tapeJSONParser;
    public GameObject[] tapeButtons;
    public GameObject movieSlotPrefab;
    public Transform tapeContent;

    public Image[] tapeImages; // Array to hold the images for each tape
    public int currentTapeIndex = 0; // Reference to the tape thats info is currently being displayed

    [Header("Movie Elements")]
    public TextMeshProUGUI movieTitleText;
    public TextMeshProUGUI movieGenreText;
    public TextMeshProUGUI movieDescriptionText;
    public Image movieImage;

    public Tape selectedTape; // Tape selected to recommend

    public GameObject moviePanel;


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

    public void UpdateTapeList() // Gets called by VHS Tapes button on screen
    {
        if (tapeJSONParser == null)
        {
            Debug.Log("Cannot find TapeJSONParser. Please assign it in the inspector.");
            return;
        }

        /*for (int i = 0; i < tapeButtons.Length; i++)
        {
            TextMeshProUGUI titleText = tapeButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            titleText.text = tapeJSONParser.tapeList[i].title;
        }*/

        // Delete existing buttons
        foreach (Transform child in tapeContent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < tapeJSONParser.tapeList.Count; i++)
        {
            GameObject newSlotObject = Instantiate(movieSlotPrefab, tapeContent);

            TapeSlot newSlot = newSlotObject.GetComponent<TapeSlot>();

            newSlot.Setup(i, tapeJSONParser.tapeList[i]);
        }

    }

    public void SelectTape(int index)
    {
        if (index < 0 || index >= tapeJSONParser.tapeList.Count)
        {
            Debug.Log("Invalid tape index selected.");
            return;
        }

        selectedTape = tapeJSONParser.tapeList[index];
        currentTapeIndex = index;

        movieTitleText.text = selectedTape.title;
        movieGenreText.text = "Genre: " + selectedTape.genre;
        movieDescriptionText.text = selectedTape.description;
        //movieImage.sprite = tapeImages[index].sprite;

        moviePanel.SetActive(true);
    }
}
