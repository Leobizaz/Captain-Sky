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
        StartCoroutine(FadeAudioSource.StartFade(audio, time, 0f));
        StartCoroutine(ChangeIt(index, time));
    }

    public IEnumerator ChangeIt(int index, float time)
    {
        yield return new WaitForSeconds(time);
        audio.clip = musics[index];
        audio.Play();
        StartCoroutine(FadeAudioSource.StartFade(audio, time, 0.108f));
    }



}
