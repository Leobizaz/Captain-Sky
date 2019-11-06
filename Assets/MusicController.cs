using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MusicController : MonoBehaviour
{
    public AudioClip[] musics;
    AudioSource audio;
    public float time;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void ChangeMusic(int index, float time)
    {
        audio.DOFade(0, time);
        ChangeIt(index, time);
    }

    public void ChangeIt(int index, float time)
    {
        audio.clip = musics[index];
        audio.Play();
        audio.DOFade(0.108f, time);
    }



}
