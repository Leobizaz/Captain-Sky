﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerExplosion : MonoBehaviour
{
    bool once;
    public float shakeAmmount;
    public float shakeDuration;
    public ParticleSystem explosionFX;
    public DoCameraShake shakeScript;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player" && !once)
        {
            once = true;
            explosionFX.Play();
            explosionFX.gameObject.GetComponent<AudioSource>().Play();
            shakeScript.ShakeAmplitude = shakeAmmount;
            shakeScript.shakeElapsedTime = shakeDuration;
        }
    }

}
