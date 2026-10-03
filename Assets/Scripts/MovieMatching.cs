using UnityEngine;

public class MovieMatching : MonoBehaviour
{
    public HorseManager HorseManager;
    public TapeManager TapeManager;

    public void CheckMovieMaching()
    {
        if (HorseManager.horseJSONParser.horseList[HorseManager.currentHorseIndex].movie == TapeManager.tapeJSONParser.tapeList[TapeManager.currentTapeIndex].title)
        {
            Debug.Log("Correct Movie!");
        }
        else
        {
            Debug.Log("Incorrect Movie!");
        }
    }
}
