using UnityEngine;
using System.Collections;

public class MenuAnimations : MonoBehaviour
{
    [Header("Tape Menu")]
    [SerializeField] Animator tapeMenuAnimator;
    [SerializeField] AnimationClip[] tapeMenuAnimations;
    [SerializeField] GameObject tapeMenuObj;

    [Header("Movie Menu")]
    [SerializeField] Animator movieMenuAnimator;
    [SerializeField] AnimationClip[] movieMenuAnimations;
    [SerializeField] GameObject movieMenuObj;

    [Header("Horse")]
    [SerializeField] Animator horseAnimator;
    [SerializeField] AnimationClip[] horseAnimations;
    [SerializeField] GameObject horseObj;


    public void TapeMenuX()
    {
        if (tapeMenuAnimator == null) return;
        tapeMenuAnimator.Play(tapeMenuAnimations[1].name);
        StartCoroutine(WaitForAnimation(tapeMenuAnimations[1].length, tapeMenuObj));
    }

    public void MovieMenuX()
    {
        if (tapeMenuAnimator == null) return;
        movieMenuAnimator.Play(movieMenuAnimations[1].name);
        StartCoroutine(WaitForAnimation(movieMenuAnimations[1].length, movieMenuObj));
    }

    public void HorseFadeIn()
    {
        if (horseAnimator == null) return;
        horseAnimator.Play(horseAnimations[0].name);
    }

    public void HorseFadeOut()
    {
        if (horseAnimator == null) return;
        horseAnimator.Play(horseAnimations[1].name);
        StartCoroutine(WaitForAnimation(horseAnimations[1].length, horseObj));
    }

    private IEnumerator WaitForAnimation(float animLength, GameObject objectToDisable)
    {
        yield return new WaitForSeconds(animLength);
        objectToDisable.SetActive(false);
    }

}
