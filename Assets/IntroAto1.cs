using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class IntroAto1 : MonoBehaviour
{
    public GameObject gameplay;
    public GameObject canvas;
    public GameObject timeline;
    public GameObject maincamera;

    void Start()
    {
        maincamera.GetComponent<PostProcessLayer>().enabled = false;
        gameplay.SetActive(false);
        canvas.SetActive(false);
        timeline.SetActive(false);
    }

    void Update()
    {
        
    }
}
