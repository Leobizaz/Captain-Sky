using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    public bool laserReady;

    public ParticleSystem chargeFX;
    public ParticleSystem fireFX;
    public GameObject laserBeam;

    private void Start()
    {
        laserReady = true;
    }

    private void Update()
    {
        if(laserReady && Input.GetKeyDown(KeyCode.Q))
        {
            ChargeLaser();

        }
    }

    public void ChargeLaser()
    {
        chargeFX.Play();
        laserReady = false;
        Invoke("FireLaser", 1.3f);
    }

    public void FireLaser()
    {
        fireFX.Play();
        laserBeam.SetActive(true);
        Invoke("ResetLaser", 4.5f);
    }

    public void ResetLaser()
    {
        laserBeam.SetActive(false);
        laserReady = true;
    }


}
