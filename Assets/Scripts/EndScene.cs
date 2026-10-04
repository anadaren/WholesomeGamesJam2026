using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip fadeOut;
    [SerializeField] private AnimationClip fadeIn;
    [SerializeField] private GameObject fadePanel;
    [SerializeField] private string goToScene = "Title";

    void Start()
    {
        FadeIn();
    }

    public void FadeIn()
    {
        if (animator == null) return;
        StartCoroutine(FadeInCoroutine());
    }

    public void FadeOut()
    {
        if (animator == null) return;
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        animator.Play(fadeIn.name);
        yield return new WaitForSeconds(1f);
        fadePanel.SetActive(false);
    }

    private IEnumerator FadeOutCoroutine()
    {
        fadePanel.SetActive(true);
        animator.Play(fadeOut.name);
        yield return new WaitForSeconds(1f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(goToScene);

    }
}
