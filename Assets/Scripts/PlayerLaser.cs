using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    public bool laserReady;

    public ParticleSystem chargeFX;
    public ParticleSystem fireFX;
    public GameObject laserBeam;
    public LineRenderer laserbeamFX;
    public Animator luzinhaBorda;
    public LayerMask layerMask;
    DoCameraShake cameraShake;
    bool isHitting;
    public GameObject impactParticle;
    public GameObject receiver;
    public GameObject model;
    bool isFiring;

    private void Start()
    {
        cameraShake = GameObject.Find("Game Manager").GetComponent<DoCameraShake>();
        laserReady = true;
    }

    private void Update()
    {
        if (laserReady && (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("P.O.V")))
        {
            ChargeLaser();

        }
        if (isFiring)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000f, Color.green);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1000f, layerMask, QueryTriggerInteraction.Ignore))
            {
                isHitting = true;
                impactParticle.SetActive(true);
                impactParticle.transform.position = hit.point;
                Debug.Log(hit.collider.gameObject.name + " por " + gameObject.name);
                laserbeamFX.SetPosition(1, hit.point);
            }
            else
            {
                laserbeamFX.SetPosition(1, new Vector3(0, 0, 1000));
                impactParticle.SetActive(false);
            }
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
        luzinhaBorda.Play("luzinhaOff");
        isFiring = true;
        cameraShake.ShakeAmplitude = 2f;
        cameraShake.shakeElapsedTime = 4.5f;
        fireFX.Play();
        laserBeam.SetActive(true);
        Invoke("ResetLaser", 4.5f);



    }

    public void ResetLaser()
    {
        isFiring = false;
        impactParticle.SetActive(false);
        laserBeam.SetActive(false);
        Invoke("LaserCooldown", 20f);
    }

    public void LaserCooldown()
    {
        luzinhaBorda.Play("sparkOn");
        laserReady = true;
    }


}
