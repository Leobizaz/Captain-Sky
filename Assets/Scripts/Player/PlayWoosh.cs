using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWoosh : MonoBehaviour
{
    AudioSource audio;
    bool once;
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player" && !once)
        {
            once = true;
            audio.Play();
        }
    }
}
