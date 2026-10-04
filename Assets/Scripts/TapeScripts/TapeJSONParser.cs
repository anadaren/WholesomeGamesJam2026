using System;
using System.Collections.Generic;
using UnityEngine;

public class TapeJSONParser : MonoBehaviour
{
    public TextAsset jsonFile;

    public List<Tape> tapeList;


    [System.Serializable]
    public class Tapes
    {
        public Tape[] tapes;
    }

    public class TapeParseException : Exception
    {
        public TapeParseException(string message) : base(message) { }
    }


    void Awake()    // Parses the JSON file and populates the tapeList with Tape objects
    {
        Tapes tapesInJson = JsonUtility.FromJson<Tapes>(jsonFile.text);
        if (tapesInJson == null || tapesInJson.tapes == null)
        {
            throw new TapeParseException($"Could not parse tapes JSON: {jsonFile}");
        }

        foreach (Tape tape in tapesInJson.tapes)
        {
            //Debug.Log("Tape index: " + tape.index + " Tape title: " + tape.title + " Tape genre: " + tape.genre + " Tape description: " + tape.description);
            tapeList.Add(tape);
        }

        // Sort alphabetically by title
        tapeList.Sort((a, b) => a.title.CompareTo(b.title));
    }

    public int SearchForTape(string tapeToFind) // Not sure if we'll need this, but it could be useful??
    {
        Tape result = tapeList.Find(x => x.title == tapeToFind);
        return tapeList.IndexOf(result);
    }
}