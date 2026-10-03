using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TapeManager : MonoBehaviour
{
    public TapeJSONParser tapeJSONParser;
    public GameObject[] tapeButtons;

    public Image[] tapeImages; // Array to hold the images for each tape
    public int currentTapeIndex = 0; // Reference to the tape thats info is currently being displayed

    [Header("Movie Elements")]
    public TextMeshProUGUI movieTitleText;
    public TextMeshProUGUI movieGenreText;
    public TextMeshProUGUI movieDescriptionText;
    public Image movieImage;

    public void UpdateTapeList()
    {
        if (tapeJSONParser == null)
        {
            Debug.Log("Cannot find TapeJSONParser. Please assign it in the inspector.");
            return;
        }

        for (int i = 0; i < tapeButtons.Length; i++)
        {
            TextMeshProUGUI titleText = tapeButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            titleText.text = tapeJSONParser.tapeList[i].title;
        }

    }

    public void SelectTape(int index)
    {
        if (index < 0 || index >= tapeJSONParser.tapeList.Count)
        {
            Debug.Log("Invalid tape index selected.");
            return;
        }

        Tape selectedTape = tapeJSONParser.tapeList[index];
        currentTapeIndex = index;

        movieTitleText.text = selectedTape.title;
        movieGenreText.text = "Genre: " + selectedTape.genre;
        movieDescriptionText.text = selectedTape.description;
        //movieImage.sprite = tapeImages[index].sprite;
    }
}
