﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Rendering.PostProcessing;

public class IntroAto1 : MonoBehaviour
{
    public GameObject gameplay;
    public GameObject canvas;
    public GameObject timeline;
    public GameObject musica;
    public GameObject maincamera;

    public GameObject fadeBlack;

    public VideoPlayer videoPlayer;

    void Start()
    {
        maincamera.GetComponent<PostProcessLayer>().enabled = false;
        gameplay.SetActive(false);
        canvas.SetActive(false);
        timeline.SetActive(false);
        musica.SetActive(false);
    }

    void Update()
    {
        videoPlayer.loopPointReached += EndReached;
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        fadeBlack.SetActive(true);
        Debug.Log("Cabo video");
        vp.playbackSpeed = vp.playbackSpeed / 10.0F;
        maincamera.GetComponent<PostProcessLayer>().enabled = true;
        //gameplay.SetActive(true);
        canvas.SetActive(true);
        //timeline.SetActive(true);
        //musica.SetActive(true);
    }
}
