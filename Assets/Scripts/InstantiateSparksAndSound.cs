using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateSparksAndSound : MonoBehaviour
{
    
    public AudioSource audio;
    public ParticleSystem particle;
    DoCameraShake cameraShake;

    void Start()
    {
        cameraShake = GameObject.Find("Game Manager").GetComponent<DoCameraShake>();
    }
    public void Play()
    {
        cameraShake.shakeElapsedTime = 0.5f;
        audio.Play();
        particle.Play();
    }
}
