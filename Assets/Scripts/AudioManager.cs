using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource sfx;

    [SerializeField] private AudioClip[] horseMusicTracks;
    [SerializeField] private AudioClip[] sfxClips;
    /*
    0 - Button click sound 
    1 - Door Beep
    2 - Checkout sound
    3 - Inventory woosh
    */

    [SerializeField] private AudioClip defaultMusic;

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

    public void SwitchMusic(int clipIndex)
    {
        if (horseMusicTracks[clipIndex] == null) { music.clip = defaultMusic; }

        music.clip = horseMusicTracks[clipIndex];
        music.Play();
    }

    public void PlaySFX(int sfxIndex)
    {
        if (sfxClips[sfxIndex] == null) return;

        sfx.clip = sfxClips[sfxIndex];
    }

}