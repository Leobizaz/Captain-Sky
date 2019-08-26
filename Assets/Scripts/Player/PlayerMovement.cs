using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering.PostProcessing;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
     private bool pressedOnce;
     private float time;

     public float sensibilidade;
     private float timerLength;
    private bool isBoosting;
    public float horizontalSpeed = 7;
    public float progressionSpeed = 6;
    public float lookSpeed = 5f;
    public GameObject modelo;
    public Transform aimTarget;
    public CinemachineDollyCart dolly;
    public Transform cameraParent;
   // public GameObject cursor;
    private bool esquerda = false;
    private bool direita = false;
    public ParticleSystem shoot;
    public ParticleSystem shoot2;
    public bool playerActive;
    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //playerActive = false;
        SetSpeed(progressionSpeed);
       // shoot2.Stop();
       // shoot.Stop();
         pressedOnce = false;
         time = 0;
         timerLength = 0.3f;
        Invoke("EnablePlayer", 7f);
    }

    void EnablePlayer()
    {
        playerActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        float x;
        float y;


        if (playerActive)
        {
            x = Input.GetAxis("Horizontal");
            y = -Input.GetAxis("Vertical");
        }
        else
        {
            x = 0;
            y = 0;
        }
       // Debug.Log(Input.GetAxis("Fire2Axis"));
        MovimentoLocal(x, y, horizontalSpeed);
            //RotationLook(x, y, 1, lookSpeed);
        Inclinada(modelo.transform, x, 45, 0.1f);
        InclinadaPraCima(modelo.transform, y, 45, 0.1f);

        Vector3 Direction = new Vector3(x, y, 1f)* sensibilidade;

         transform.rotation = Quaternion.RotateTowards (transform.rotation,Quaternion.LookRotation (Direction),Mathf.Deg2Rad * 40f);

        float tiro = Input.GetAxis("Fire2Axis");

        if (Input.GetButtonDown("Fire3") && !isBoosting)
            Boost(true);

        if (Input.GetButtonUp("Fire3") && !isBoosting)
            Boost(false);

        if (tiro == 0 || Input.GetButton("Fire1"))
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
        /*
        if (Input.GetAxisRaw("Fire1Axis") != 0)
            Break(true);

        if (Input.GetAxisRaw("Fire1Axis") == 0)
            Break(false);
        */
        if (Input.GetButtonDown("Left") )
        {
            esquerda = true;
            
             if(!pressedOnce)
             {
             pressedOnce = true;
             time = Time.time;
             }
 
             else
             {
                BarrelRoll(1);
                Debug.Log("Roll");
             }
        }

        if (Input.GetButtonUp("Left"))
            esquerda = false;

        if (Input.GetButtonDown("Right"))
        {
             direita = true;
             if(!pressedOnce)
             {
             pressedOnce = true;
             time = Time.time;
             }
 
             else
             {
                BarrelRoll(-1);
                Debug.Log("Roll");
             }
        }

        if(pressedOnce)
         {
             if(Time.time - time > timerLength)
             {
                 pressedOnce = false;
             }
 
             time += Time.deltaTime *0f;
         }
         
        if (Input.GetButtonUp("Right"))
        {
           // pressedOnce = true;
            direita = false;
        }

        

    }

    void MovimentoLocal(float x, float y, float speed)
    {
        transform.localPosition += new Vector3(x, y, 0) * speed * Time.deltaTime;
        if(playerActive)
            ClampPosition();
    }

    void ClampPosition()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        //Boundary em relação a camera

        pos.x = Mathf.Clamp(pos.x, 0.1f, 0.9f);
        pos.y = Mathf.Clamp(pos.y, 0.1f, 0.9f);

        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void RotationLook(float x, float y, float z, float speed)
    {
        aimTarget.parent.position = Vector3.zero;
        aimTarget.localPosition = new Vector3(x, y, z);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(aimTarget.position), Mathf.Deg2Rad * speed * Time.deltaTime);
    }

    void Inclinada(Transform target, float eixo, float limite, float lerpTime)
    {
        Vector3 targetEulerAngles = target.localEulerAngles;
        if(esquerda == false && direita == false)
        target.localEulerAngles = new Vector3(targetEulerAngles.x, Mathf.LerpAngle(targetEulerAngles.y, 1f, .1f), Mathf.LerpAngle(targetEulerAngles.z, -eixo * limite, lerpTime));
        //target.localEulerAngles = new Vector3(targetEulerAngles.x, Mathf.LerpAngle(targetEulerAngles.y, +eixo * limite, lerpTime), Mathf.LerpAngle(targetEulerAngles.z, -eixo * limite, lerpTime));
        else if (esquerda == true)
            target.localEulerAngles = new Vector3(targetEulerAngles.x, Mathf.LerpAngle(targetEulerAngles.y, +eixo * limite, lerpTime), Mathf.LerpAngle(targetEulerAngles.z + 10f, -0.01f * limite, lerpTime));
        else if (direita == true)
            target.localEulerAngles = new Vector3(targetEulerAngles.x, Mathf.LerpAngle(targetEulerAngles.y, +eixo * limite, lerpTime), Mathf.LerpAngle(targetEulerAngles.z - 10f, -0.01f * limite, lerpTime));
    }

    void InclinadaPraCima(Transform target, float eixo, float limite, float lerpTime)
    {
        Vector3 targetEulerAngles = target.localEulerAngles;
        if (esquerda == false && direita == false)
            target.localEulerAngles = new Vector3(Mathf.LerpAngle(targetEulerAngles.x, -eixo * limite, lerpTime), targetEulerAngles.y, target.eulerAngles.z);
        else if (esquerda == true)
            target.localEulerAngles = new Vector3(Mathf.LerpAngle(targetEulerAngles.x, -eixo * limite, lerpTime), targetEulerAngles.y, target.eulerAngles.z);
        else if (direita == true)
            target.localEulerAngles = new Vector3(Mathf.LerpAngle(targetEulerAngles.x, -eixo * limite, lerpTime), targetEulerAngles.y, target.eulerAngles.z);
    }

    void SetSpeed(float x)
    {
        dolly.m_Speed = x;
    }

    void SetCameraZoom(float zoom, float duration)
    {
        cameraParent.DOLocalMove(new Vector3(0, 0, zoom), duration);
    }

    void Break(bool state)
    {   
        float speed = state ? progressionSpeed / 3 : progressionSpeed;
        float zoom = state ? 3 : 0;

        SetSpeed(speed);
        SetCameraZoom(zoom, .4f);
    }

    void Boost(bool state)
    {
        float speed = state ? progressionSpeed * 1.5f : progressionSpeed;
        float zoom = state ? -7 : 0;
        SetSpeed(speed);
        //DOVirtual.Float(dolly.m_Speed, speed, .15f, SetSpeed);
        SetCameraZoom(zoom, .4f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Boost")
        {
            isBoosting = true;
            Debug.Log("Boost ON");
            Boost(false);
            //SetSpeed(progressionSpeed + 75f);
            float currentSpeed = dolly.m_Speed;
            DOVirtual.Float(currentSpeed, progressionSpeed +75f, 2f, SetSpeed);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Boost")
        {
            isBoosting = false;
            Debug.Log("Boost OFF");
            //Boost(false);
            //SetSpeed(progressionSpeed);
            float currentSpeed = dolly.m_Speed;
            DOVirtual.Float(currentSpeed, progressionSpeed, 2f, SetSpeed);

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(aimTarget.position, 0.5f);
        Gizmos.DrawSphere(aimTarget.position, 0.15f);
    }

    void BarrelRoll(float lado)
    { 
        modelo.transform.DOLocalRotate(new Vector3(0,0,360f * lado),0.6f, RotateMode.LocalAxisAdd );
        transform.DOLocalMove(new Vector3(-lado * 6, 0, 0), 0.7f).SetEase(Ease.OutSine);
    }

}
