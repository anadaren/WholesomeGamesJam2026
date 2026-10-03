using System;
using System.Collections.Generic;
using UnityEngine;

public class HorseJSONParser : MonoBehaviour
{
    public TextAsset jsonFile;

    public List<Horse> horseList;


    [System.Serializable]
    public class Horses
    {
        public Horse[] horses;
    }

    public class HorseParseException : Exception
    {
        public HorseParseException(string message) : base(message) { }
    }


    void Awake()    // Parses the JSON file and populates the horseList with Horse objects
    {
        Horses horsesInJson = JsonUtility.FromJson<Horses>(jsonFile.text);
        if (horsesInJson == null || horsesInJson.horses == null)
        {
            throw new HorseParseException($"Could not parse horses JSON: {jsonFile}");
        }

        foreach (Horse horse in horsesInJson.horses)
        {
            //Debug.Log("Horse index: " + horse.index + " Horse name: " + horse.name + " Horse type: " + horse.type + " Horse movie: " + horse.movie);
            horseList.Add(horse);
        }
    }

    public int SearchForHorse(string horseToFind) // Not sure if we'll need this, but it could be useful??
    {
        Horse result = horseList.Find(x => x.name == horseToFind);
        return horseList.IndexOf(result);
    }
}