using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Rendering.PostProcessing;

public class FadeCreditos : MonoBehaviour
{
    public GameObject victoryScreen;
    Camera mainCamera;
    public VideoPlayer videoPlayer;
    public Image FillCircle;
    private float speed = 0.7f;
    bool fill = false;
    public GameObject textin;
    public GameObject videoCanvas;
    public GameObject mask;
    Image imagem;
    public GameObject hud;
    public PlayerMovement player;

    private void Start()
    {
        imagem = GetComponent<Image>();
        mainCamera = Camera.main;
        Invoke("CreditsVideo", 9f);
    }

    public void CreditsVideo()
    {
        hud.SetActive(false);
        player.playerActive = false;
        imagem.enabled = false;
        mask.SetActive(false);
        videoCanvas.SetActive(true);
        GameObject.Find("Music").GetComponent<MusicController>().ChangeMusic(4, 3);
        textin.SetActive(true);
        videoPlayer.gameObject.SetActive(true);
        mainCamera.GetComponent<PostProcessLayer>().enabled = false;

    }

    void Update()
    {
        if ((Input.GetButtonDown("Break") || Input.GetKeyDown(KeyCode.Space)) && !IsInvoking("SkipIntro") && FillCircle != null)
        {
            fill = true;
            Invoke("SkipIntro", 3f);
        }
        if (Input.GetButtonUp("Break") || Input.GetKeyUp(KeyCode.Space) && FillCircle != null)
        {
            fill = false;
            FillCircle.GetComponent<Image>().fillAmount = 0;
            CancelInvoke("SkipIntro");
        }

        if (fill == true && FillCircle != null)
        {
            FillCircle.GetComponent<Image>().fillAmount += 0.5f * speed * Time.deltaTime;
        }
        videoPlayer.loopPointReached += EndReached;
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        victoryScreen.SetActive(true);
        textin.SetActive(false);
        Debug.Log("Cabo video");
        vp.playbackSpeed = vp.playbackSpeed / 10.0F;
        mainCamera.GetComponent<PostProcessLayer>().enabled = true;
    }

    void SkipIntro()
    {
        textin.SetActive(false);
        victoryScreen.SetActive(true);
        videoPlayer.transform.parent.gameObject.SetActive(false);
        Debug.Log("Pulou video");
        mainCamera.GetComponent<PostProcessLayer>().enabled = true;
        fill = false;
    }

}
