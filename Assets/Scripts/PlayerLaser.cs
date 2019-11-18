using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    public bool laserReady;

    public ParticleSystem chargeFX;
    public ParticleSystem fireFX;
    public GameObject laserBeam;
    public LayerMask layerMask;
    DoCameraShake cameraShake;
    bool isHitting;
    public GameObject impactParticle;
    public GameObject receiver;
    public GameObject model;

    private void Start()
    {
        cameraShake = GameObject.Find("Game Manager").GetComponent<DoCameraShake>();
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
        cameraShake.ShakeAmplitude = 2f;
        cameraShake.shakeElapsedTime = 4.5f;
        fireFX.Play();
        laserBeam.SetActive(true);
        Invoke("ResetLaser", 4.5f);

        RaycastHit hit;
        if (Physics.Raycast(model.transform.position, this.transform.forward, out hit, 1000f, layerMask))
        {
            isHitting = true;
            impactParticle.SetActive(true);
            impactParticle.transform.position = hit.point;
            receiver.transform.LookAt(gameObject.transform);
            receiver.transform.localPosition = hit.point;
            Debug.Log(hit.collider.gameObject.name + " por " + gameObject.name);
        }
        else
        {
            impactParticle.SetActive(false);
        }



    }

    public void ResetLaser()
    {
        impactParticle.SetActive(false);
        laserBeam.SetActive(false);
        laserReady = true;
    }


}
