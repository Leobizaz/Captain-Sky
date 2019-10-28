using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public GameObject dialogoKinect;
    public GameObject dialogoKinect2;
    public GameObject audiokinect;
    public VideoPlayer videoPlayer;
    public Image FillCircle;
    private float speed = 0.4f;
    bool fill = false;

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


        if ((Input.GetButtonDown("Break") || Input.GetKeyDown(KeyCode.Space)) && !IsInvoking("SkipIntro") && FillCircle != null)
        {
            fill = true;
            Invoke("SkipIntro", 5f);
        }
        if (Input.GetButtonUp("Break") || Input.GetKeyUp(KeyCode.Space) && FillCircle != null)
        {
            FillCircle.GetComponent<Image>().fillAmount = 0;
            CancelInvoke("SkipIntro");
        }

        if ( fill == true && FillCircle != null)
        {
            FillCircle.GetComponent<Image>().fillAmount += 0.5f * speed * Time.deltaTime;
        }
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
        Invoke("PlayCantoria", 2f);
        //timeline.SetActive(true);
        //musica.SetActive(true);
    }

    void SkipIntro()
    {
        videoPlayer.transform.parent.gameObject.SetActive(false);
        Debug.Log("Pulou video");
        maincamera.GetComponent<PostProcessLayer>().enabled = true;
        canvas.SetActive(true);
        Invoke("PlayCantoria", 2f);
        fill = false;
    }

    void PlayCantoria()
    {
        //musica.SetActive(true);
        musica.GetComponent<AudioSource>().volume = 0.038f;
        audiokinect.SetActive(true);
        dialogoKinect.SetActive(true);
        Invoke("PlayCantoria2", 6.2f);
    }

    void PlayCantoria2()
    {
        dialogoKinect2.SetActive(true);
    }
}
