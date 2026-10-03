using UnityEngine;

public class MovieMatching : MonoBehaviour
{
    public HorseManager HorseManager;
    public TapeManager TapeManager;

    public int horsesMatched = 0;
    public int correctMatches = 0;

    void Start()
    {
        HorseManager = HorseManager.Instance;
        TapeManager = TapeManager.Instance;
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
            HorseManager.currentHorseIndex++;
            /*if (currentHorseIndex >= horseList.Count) // If we want to see each of the horses more than once in a day, uncomment this block
            {
                currentHorseIndex = 0; // Loop back to the first horse
            }*/
            HorseManager.NewHorse();
        }
    }

    public void EndGame()
    {
        if (correctMatches == HorseManager.horseList.Count)
        {
            Debug.Log("Congratulations! You matched all the movies correctly! Best ending.");
        }
        else if (correctMatches > 3)
        {
            Debug.Log("Good job! You got " + correctMatches + " correct matches. Okay ending.");
        }
        else
        {
            Debug.Log("Game Over! You got " + correctMatches + " correct matches. Bad ending.");
        }
    }
}
