using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JacintaSoundManager : MonoBehaviour {

    public AudioClip popClip;
    public AudioClip bombedClip;
    public AudioClip fireClip;
    public AudioClip cutClip;
    public AudioClip netClip;
    public AudioClip podaClip;

    private AudioSource audioSource;

    void Start () {
        audioSource = GetComponent<AudioSource>();
    }

    public void playPopSound()
    {
        audioSource.clip = popClip;
        audioSource.Play();
    }

    public void playBombSound()
    {
        audioSource.clip = bombedClip;
        audioSource.Play();
    }

    public void playCutSound()
    {
        audioSource.clip = cutClip;
        audioSource.Play();
    }

    public void playFireSound()
    {
        audioSource.clip = fireClip;
        audioSource.Play();
    }

    public void playNetSound()
    {
        audioSource.clip = netClip;
        audioSource.Play();
    }

    public void playPodaSound()
    {
        audioSource.clip = podaClip;
        audioSource.Play();
    }
}
