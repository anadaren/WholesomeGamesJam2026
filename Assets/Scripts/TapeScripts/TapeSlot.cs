using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TapeSlot : MonoBehaviour
{
    public TextMeshProUGUI titleText;

    private int tapeIndex;

    public void Setup(int index, Tape tape)
    {
        tapeIndex = index;
        titleText.text = tape.title;
    }

    public void Select()
    {
        TapeManager.Instance.SelectTape(tapeIndex);
    }
}