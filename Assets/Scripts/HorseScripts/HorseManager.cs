using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HorseManager : MonoBehaviour
{
    public HorseJSONParser horseJSONParser;

    public Image[] horseImages; // Array to hold the images for each horse
    public int currentHorseIndex = 0; // Reference to the horse whose is currently in front of the player

    public TextMeshProUGUI horseNameText;

    void Start()
    {
        horseNameText.text = "TEST: " + horseJSONParser.horseList[currentHorseIndex].name;
    }

}