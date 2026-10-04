using UnityEngine;
using System.Collections;
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
            StartCoroutine(EndGame());
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

    public IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1f);
        HorseManager.menuAnimations.HorseFadeOut(); // Fade out last horse
        AudioManager.Instance.DefaultMusic(); // Switches music to default theme

        yield return new WaitForSeconds(1f);

        if (correctMatches == HorseManager.horseList.Count)
        {
            endingTxt.text = correctMatches + "/7 movies matched correctly.\n\nCongratulations! You matched all the movies correctly!\n\nAll customers loved your attitude and recommendations. You get a promotion!";
        }
        else if (correctMatches > 3)
        {
            endingTxt.text = correctMatches + "/7 movies matched correctly.\n\nGood job!\n\nCustomers generally like you. You'll come in to work tomorrow just like any day.";
        }
        else
        {
            endingTxt.text = correctMatches + "/7 movies matched correctly.\n\nBetter luck next time!\n\nYou got a bunch of complaints from customers, saying you didn't pay attention to anything they said. You get fired!";
        }
        endPanel.SetActive(true);
    }
}
