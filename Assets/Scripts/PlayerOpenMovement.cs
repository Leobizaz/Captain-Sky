﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerOpenMovement : MonoBehaviour
{
    Vector3 vectorinput;
    public GameObject cameraHolder;
    public GameObject playerMesh;
    public float forwardSpeed = 10;
    public float rollSpeed = 2;
    public float yawSpeed = 1;
    bool manobra;
    float storedSpeed;
    float manobra_x;
    float manobra_y;
    float manobra_z;
    float rotation;
    public Animator anim;
    public ParticleSystem shoot;
    public ParticleSystem shoot2;
    public AudioSource audioSource;
    bool boost;
    public CameraFollow CMCamera1;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Boost"))
            boost = true;
        if (Input.GetButtonUp("Boost"))
            boost = false;
        if (Input.GetButtonDown("Back"))
            cameraHolder.transform.Rotate(0,180,0, Space.Self);
        if (Input.GetButtonUp("Back"))
            cameraHolder.transform.Rotate(0, 0, 0, Space.Self);
        if (Input.GetButtonUp("HorizontalDireito"))
            vectorinput.x = 0;

        anim.SetFloat("xInput", x);
        anim.SetFloat("yInput", y);

        Movement();
        if (Input.GetKeyDown(KeyCode.F) && !manobra)
        {
            ManobraVoltar();
        }
        float tiro = Input.GetAxis("Shoot");

        if (tiro == 0 || Input.GetButtonDown("Shoot"))
        {
            audioSource.Stop();
            shoot2.Play();
            shoot.Play();
        }
        else
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }

        if(boost == true)
        {
            SetSpeed(forwardSpeed + 1);
            //float currentSpeed = forwardSpeed;
            //DOVirtual.Float(currentSpeed, 160, 1.5f, SetSpeed).SetEase(Ease.InOutQuad);
            //Boost(boost);

        }
        /*
        if (boost == false)
        {
            float currentSpeed = forwardSpeed;
            SetCameraZoom(0f, .4f);
            DOVirtual.Float(currentSpeed, 90, 4f, SetSpeed).SetEase(Ease.InOutQuad);
        }
        */

    }

    public void SetSpeed(float x)
    {
        SetCameraZoom(-(forwardSpeed / 12), 1.5f);
        forwardSpeed = x;
        forwardSpeed = Mathf.Clamp(forwardSpeed, 30, 160);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Boundary" && !manobra)
        {
            ManobraVoltar();
        }
    }

    void Movement()
    {

        if (!manobra)
        {
            float currentVelocity = 0;
            //vectorinput = new Vector3(Input.GetAxis("HorizontalDireito"), Input.GetAxis("Vertical") + Input.GetAxis("Vertical"), 0);
            vectorinput.x = Mathf.SmoothDamp(vectorinput.x, Input.GetAxis("HorizontalDireito"), ref currentVelocity, 0.03f);
            vectorinput.y = Mathf.SmoothDamp(vectorinput.y, Input.GetAxis("Vertical") + Input.GetAxis("Vertical"), ref currentVelocity, 0.03f);
            rotation = Mathf.SmoothDamp(rotation, Input.GetAxis("Horizontal"), ref currentVelocity, 0.03f);
        }
        else
            vectorinput = new Vector3(manobra_x, manobra_y, manobra_z);

        if (Input.GetAxis("Horizontal") == 0 || Input.GetAxis("HorizontalDireito") == 0 || Input.GetAxis("Vertical") == 0)
        {
            CMCamera1.offset = new Vector3(0, 5, -12.68f);
            //anim.SetInteger("State", 0);
        }
/*
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("HorizontalDireito") > 0)
            anim.SetInteger("State", 1);
        if (Input.GetAxis("Horizontal") < 0 || Input.GetAxis("HorizontalDireito") < 0)
            anim.SetInteger("State", 2);
            */
        if (Input.GetAxis("Vertical") > 0)
        {
            SetSpeed(forwardSpeed + 1);
            //para baixo
            //anim.SetInteger("State", 4);
            cameraHolder.transform.DOLocalMoveY(-10, 4);
            CMCamera1.offset = new Vector3(0, 5, -12.68f);
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            SetSpeed(forwardSpeed - 1);
            //para cima
            //anim.SetInteger("State", 3);
            cameraHolder.transform.DOLocalMoveY(10, 4);
            CMCamera1.offset = new Vector3(0, 7, -12.68f);
        }



        //if (!manobra)
        // {
        HorizontalLean(playerMesh.transform, rotation, 40, .1f);
        HorizontalLean(playerMesh.transform, vectorinput.x, 40, .1f);
            VerticalLean(playerMesh.transform, -vectorinput.y, 15, .1f);
       // }



        transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed);
        Quaternion rot = Quaternion.Euler(new Vector3(0, rotation, 0));
        Quaternion yawRot = Quaternion.Euler(new Vector3(vectorinput.y * yawSpeed, 0, 0));
        Quaternion rollRot = Quaternion.Euler(new Vector3(0, 0, -vectorinput.x * rollSpeed));
        
        transform.localRotation = transform.localRotation * rot * yawRot * rollRot;
    }

    void HorizontalLean(Transform target, float axis, float leanLimit, float lerpTime)
    {
        Vector3 targetEulerAngels = target.localEulerAngles;
        target.localEulerAngles = new Vector3(targetEulerAngels.x, targetEulerAngels.y, Mathf.LerpAngle(targetEulerAngels.z, -axis * leanLimit, lerpTime));
    }

    void VerticalLean(Transform target, float axis, float leanLimit, float lerpTime)
    {
        Vector3 targetEulerAngels = target.localEulerAngles;
        target.localEulerAngles = new Vector3(Mathf.LerpAngle(targetEulerAngels.x, -axis * leanLimit, lerpTime), targetEulerAngels.y, targetEulerAngels.z);
    }

    void ManobraVoltar()
    {
        SetCameraZoom(-30, .4f);
        manobra = true;
        storedSpeed = yawSpeed;
        yawSpeed = yawSpeed * 3;
        manobra_y = -1;
        if (!IsInvoking("EndManobra"))
            Invoke("EndManobra", 1f);
    }

    void EndManobra()
    {
        SetCameraZoom(-10, .4f);
        playerMesh.transform.DOLocalRotate(new Vector3(0, 0, 360), 0.4f, RotateMode.LocalAxisAdd);
        transform.DOLocalRotate(new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0), 0.2f, RotateMode.Fast).SetEase(Ease.OutSine);

        yawSpeed = storedSpeed;
        manobra = false;
    }

    void SetCameraZoom(float zoom, float duration)
    {
        cameraHolder.transform.DOLocalMove(new Vector3(0, 0, zoom), duration);
    }
    void Boost(bool state)
    {
        float zoom = state ? -7 : 0;
        //DOVirtual.Float(dolly.m_Speed, speed, .15f, SetSpeed);
        SetCameraZoom(zoom, .4f);
    }
}
