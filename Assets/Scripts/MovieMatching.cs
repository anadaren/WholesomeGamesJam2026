using UnityEngine;
using TMPro;

public class MovieMatching : MonoBehaviour
{
    public static MovieMatching Instance { get; private set; }
    public HorseManager HorseManager;
    public TapeManager TapeManager;

    public int horsesMatched = 0;
    public int correctMatches = 0;

    public GameObject endPanel;
    public TextMeshProUGUI endingTxt;

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
        HorseManager = HorseManager.Instance;
        TapeManager = TapeManager.Instance;
        endPanel.SetActive(false);
    }

    public void CheckMovieMaching()
    {
        if (HorseManager.horseList[HorseManager.currentHorseIndex].movie == TapeManager.selectedTape.title)
        {
            Debug.Log("Correct Movie!");
            correctMatches++;
            // TODO: Add more dialogue for horses positive reaction
        }
        else
        {
            Debug.Log("Incorrect Movie!");
            // TODO: Add more dialogue for horses negative reaction
        }
        horsesMatched++;
        if (horsesMatched >= 8)
        {
            EndGame();
        }
        else
        {
            DialogueManager.Instance.BeginCheckoutDialogue();
        }
    }

    public void EndInteraction() // Ending the current interaction
    {
        HorseManager.currentHorseIndex++;
        /*if (currentHorseIndex >= horseList.Count) // If we want to see each of the horses more than once in a day, uncomment this block
        {
            currentHorseIndex = 0; // Loop back to the first horse
        }*/
        HorseManager.NewHorse();
    }

    public void EndGame()
    {
        if (correctMatches == HorseManager.horseList.Count)
        {
            endingTxt.text = "Congratulations! You matched all the movies correctly! Best ending.";
        }
        else if (correctMatches > 3)
        {
            endingTxt.text = "Good job! You got " + correctMatches + " correct matches. Okay ending.";
        }
        else
        {
            endingTxt.text = "Game Over! You got " + correctMatches + " correct matches. Bad ending.";
        }
        endPanel.SetActive(true);
    }
}
