using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour
{
    public int musicNumber;
    public float time;
    bool once;
    MusicController controller;

    private void Start()
    {
        controller = GameObject.Find("Music").GetComponent<MusicController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !once)
        {
            once = true;
            ChangeMusica();
        }
    }

    public void ChangeMusica()
    {
        controller.ChangeMusic(musicNumber, time);
    }
}
