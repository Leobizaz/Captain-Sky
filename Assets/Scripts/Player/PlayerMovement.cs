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
    public GameObject radarIMG;
    public GameObject oculosHUD;
    bool briefCancel;
    public bool canFirstPerson;
    public GameObject cockpit;
    public Animator anim;

   // public GameObject cursor;
    private bool esquerda = false;
    private bool direita = false;
    public bool podeAtirar;
    public ParticleSystem shoot;
    public ParticleSystem shoot2;
    public CinemachineVirtualCamera thirdPersonCamera;
    public GameObject firstPersonCamera;
    public Camera boundaryCamera;
    public CinemachineBrain cinemachineBrain;
    public bool playerActive;
    public static bool firstPerson;
    AudioSource audioSource;
    Sequence mySequence;
    public GameObject modelooo;
    public SpriteRenderer mira;

    // Start is called before the first frame update
    void Start()
    {
        podeAtirar = true;
        mySequence = DOTween.Sequence();
        firstPerson = false;
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
        if (!Pause.paused && !Pause.victory)
        {
            float x;
            float y;
            float x2;
            float y2;
            y2 = Input.GetAxis("VerticalDireito");
            x2 = Input.GetAxis("HorizontalDireito");

            if (PlayerHealth.dead == true)
                playerActive = false;

            if (playerActive && Input.GetButtonDown("P.O.V") && canFirstPerson)
            {
                firstPerson = !firstPerson;

                if (firstPerson)
                {
                    aimTarget.transform.localPosition = new Vector3(0f, 0, 8f);
                    oculosHUD.SetActive(true);
                    modelooo.SetActive(false);
                    cockpit.SetActive(true);
                    firstPersonCamera.SetActive(true);
                    radarIMG.SetActive(false);
                    mira.enabled = false;
                    
                   
                }
                else
                {
                    mira.enabled = true;
                    cockpit.SetActive(false);
                    modelooo.SetActive(true);
                    oculosHUD.SetActive(false);
                    radarIMG.SetActive(true);
                    firstPersonCamera.SetActive(false);
                }
            }


            if (playerActive && !PlayerHealth.dead)
            {
                if (Pause.controleInvertido)
                {
                    x = Input.GetAxis("Horizontal");
                    y = -Input.GetAxis("Vertical");
                }
                else
                {
                    x = Input.GetAxis("Horizontal");
                    y = Input.GetAxis("Vertical");
                }
            }
            else
            {
                x = 0;
                y = 0;
            }

            if (x == 0 && y == 0 && !firstPerson)
            {
                Invoke("ResetMira", 0.5f);
                anim.SetInteger("State", 0);
            }
            else
            {
                CancelInvoke("ResetMira");
                mySequence.Kill();
            }
            if (x > 0)
                anim.SetInteger("State", 1);
            if (x < 0)
                anim.SetInteger("State", 2);
            if (Pause.controleInvertido == false)
            {
                if (y > 0)
                    anim.SetInteger("State", 3);
                if (y < 0)
                    anim.SetInteger("State", 4);
            }
            if (Pause.controleInvertido == true)
            {
                if (y > 0)
                    anim.SetInteger("State", 3);
                if (y < 0)
                    anim.SetInteger("State", 4);
            }


            MovimentoLocal(x, y, horizontalSpeed);
            
            if (!firstPerson)
            RotationLook(x, y);
            else
            RotationLookFPS(x2, y2);

            //Inclinada(modelo.transform, x, 25, Time.deltaTime * 3f);
            //InclinadaPraCima(modelo.transform, y, 25, Time.deltaTime * 2f);

            if (firstPerson)
            {
                Inclinada(firstPersonCamera.transform, x, 25, 0.07f);
                InclinadaPraCima(firstPersonCamera.transform, y, 10, 0.07f);
                Inclinada(cockpit.transform, x, 15, 0.07f);
                //InclinadaPraCima(cockpit.transform, y, 25, 0.07f);
            }

            Inclinada(modelo.transform, x, 25, 0.1f);
            InclinadaPraCima(modelo.transform, y, 25, 0.1f);

            Vector3 Direction = new Vector3(x, y, 1f);

            //if(!isBoosting)
            //transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.LookRotation(Direction), Mathf.Deg2Rad * 50f * sensibilidade);

            float tiro = Input.GetAxis("Shoot");

            if (Input.GetButtonDown("P.O.V") && !isBoosting)
            {
                //Boost(true);
            }

            if (Input.GetButtonUp("P.O.V") && !isBoosting)
            {
                //Boost(false);
            }
            if (playerActive)
                ClampPosition();

            if (podeAtirar)
            {
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
            }
            else
            {
                shoot.Stop();
                shoot2.Stop();
            }

            if (Input.GetAxisRaw("Break") != 0)
            {
                //pau no cu do break
            }

            if (Input.GetButtonDown("Right") || Input.GetButtonDown("Right2"))
            {
                direita = true;
                if (!pressedOnce)
                {
                    pressedOnce = true;
                    time = Time.time;
                }

                else
                {
                    BarrelRoll(-1);
                }
            }

            if (Input.GetButtonUp("Right") || Input.GetButtonUp("Right2"))
                direita = false;

            ///////////////////////////////////////

            if (Input.GetButtonDown("Left") || Input.GetButtonDown("Left2") || Input.GetKeyDown(KeyCode.B))
            {
                esquerda = true;
                if (!pressedOnce)
                {
                    pressedOnce = true;
                    time = Time.time;
                }

                else
                {
                    BarrelRoll(1);
                }
            }
            
            if (Input.GetButtonUp("Left") || Input.GetButtonUp("Left2"))
                esquerda = false;

            if (pressedOnce)
            {
                if (Time.time - time > timerLength)
                {
                    pressedOnce = false;
                }

                time += Time.deltaTime * 0f;
            }

        }
    }

    void MovimentoLocal(float x, float y, float speed)
    {
        transform.localPosition += new Vector3(x, y, 0) * speed * Time.deltaTime;
    }

    void ClampPosition()
    {
        Vector3 pos = boundaryCamera.WorldToViewportPoint(transform.position);

        //Boundary em relação a camera

        pos.x = Mathf.Clamp(pos.x, 0.1f, 0.9f);
        pos.y = Mathf.Clamp(pos.y, 0.1f, 0.9f);

        transform.position = boundaryCamera.ViewportToWorldPoint(pos);
    }

    void RotationLook(float x, float y)
    {
        aimTarget.transform.localPosition += new Vector3(x, 0, 8f) * Time.deltaTime;
        aimTarget.transform.localPosition += new Vector3(0, y, 8f) * Time.deltaTime;
        aimTarget.transform.localPosition = new Vector3(Mathf.Clamp(aimTarget.transform.localPosition.x, -10f, 10f), Mathf.Clamp(aimTarget.transform.localPosition.y, -6f, 6f),8f);
    }
    void RotationLookFPS(float x, float y)
    {
        aimTarget.transform.localPosition +=( new Vector3(x, 0, 8f) * Time.deltaTime )* 6;
        aimTarget.transform.localPosition +=( new Vector3(0, y, 8f) * Time.deltaTime) * 6;
        aimTarget.transform.localPosition = new Vector3(Mathf.Clamp(aimTarget.transform.localPosition.x, -4.2f, 4.2f), Mathf.Clamp(aimTarget.transform.localPosition.y, -1f, 3f), 8f);
    }

    void ResetMira()
    {
        if(!mySequence.IsPlaying())
            mySequence.Append(aimTarget.DOLocalMove(new Vector3(0, 0, 8f), 0.7f));
    }
    
    void Inclinada(Transform target, float eixo, float limite, float lerpTime)
    {
        /* old
        Vector3 targetEulerAngles = target.localEulerAngles;
        if(esquerda == false && direita == false)
        target.localEulerAngles = new Vector3(targetEulerAngles.x, Mathf.LerpAngle(targetEulerAngles.y, 1f, .1f), Mathf.LerpAngle(targetEulerAngles.z, -eixo * limite, lerpTime));
        //target.localEulerAngles = new Vector3(targetEulerAngles.x, Mathf.LerpAngle(targetEulerAngles.y, +eixo * limite, lerpTime), Mathf.LerpAngle(targetEulerAngles.z, -eixo * limite, lerpTime));
        else if (esquerda == true)
            target.localEulerAngles = new Vector3(targetEulerAngles.x, Mathf.LerpAngle(targetEulerAngles.y, +eixo * limite, lerpTime), Mathf.LerpAngle(targetEulerAngles.z + 10f, -0.01f * limite, lerpTime));
        else if (direita == true)
            target.localEulerAngles = new Vector3(targetEulerAngles.x, Mathf.LerpAngle(targetEulerAngles.y, +eixo * limite, lerpTime), Mathf.LerpAngle(targetEulerAngles.z - 10f, -0.01f * limite, lerpTime));
            */

        Vector3 targetEulerAngles = target.localEulerAngles;
        if (esquerda == false && direita == false)
            target.localEulerAngles = new Vector3(targetEulerAngles.x, targetEulerAngles.y, Mathf.LerpAngle(targetEulerAngles.z, -eixo * limite, lerpTime));
        //target.localEulerAngles = new Vector3(targetEulerAngles.x, Mathf.LerpAngle(targetEulerAngles.y, +eixo * limite, lerpTime), Mathf.LerpAngle(targetEulerAngles.z, -eixo * limite, lerpTime));
        else if (esquerda == true)
            target.localEulerAngles = new Vector3(targetEulerAngles.x, targetEulerAngles.y, Mathf.LerpAngle(targetEulerAngles.z + 10f, -0.01f * limite, lerpTime));
        else if (direita == true)
            target.localEulerAngles = new Vector3(targetEulerAngles.x, targetEulerAngles.y, Mathf.LerpAngle(targetEulerAngles.z - 10f, -0.01f * limite, lerpTime));
        

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
            float speed = other.GetComponent<Boostzone>().Boost_speed;
            isBoosting = true;
            Debug.Log("Boost ON");
            //Boost(false);
            //SetSpeed(progressionSpeed + 75f);
            float currentSpeed = dolly.m_Speed;
            float newSpeed = progressionSpeed + speed;
            DOVirtual.Float(currentSpeed, newSpeed, 4f, SetSpeed).SetEase(Ease.InOutQuad);
            briefCancel = true;
            Invoke("ResetBriefCancel", 2f);
        }
        if(other.tag == "Setspeed")
        {
            float newspeed = other.GetComponent<Boostzone>().Boost_speed;
            float currentspeed = dolly.m_Speed;
            DOVirtual.Float(currentspeed, newspeed, 4f, SetSpeed).SetEase(Ease.InOutQuad);
        }

    }
    
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Boost" && !briefCancel)
        {
            isBoosting = false;
            Debug.Log("Boost OFF");
            //Boost(false);
            //SetSpeed(progressionSpeed);
            float currentSpeed = dolly.m_Speed;
            DOVirtual.Float(currentSpeed, progressionSpeed, 2f, SetSpeed);

        }
        else
        {
            //Debug.Log("Negado");
        }
    }
    

    void ResetBriefCancel()
    {
        Debug.Log("Oh fuck it resetted");
        briefCancel = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(aimTarget.position, 0.5f);
        Gizmos.DrawSphere(aimTarget.position, 0.15f);
    }

    void BarrelRoll(float lado)
    { 
        if(lado == -1 && transform.localPosition.x < 20)
        {
            modelo.transform.DOLocalRotate(new Vector3(0,0,360f * lado),0.6f, RotateMode.LocalAxisAdd );
            transform.DOLocalMove(new Vector3(transform.localPosition.x + (-lado * 10), transform.localPosition.y, 0), 0.7f);//.SetEase(Ease.OutSine);
            aimTarget.transform.localPosition = new Vector3(0, 0, 9f);
        }
        if(lado == 1 && transform.localPosition.x > -20)
        {
            modelo.transform.DOLocalRotate(new Vector3(0,0,360f * lado),0.6f, RotateMode.LocalAxisAdd );
            transform.DOLocalMove(new Vector3(transform.localPosition.x + (-lado * 10), transform.localPosition.y, 0), 0.7f);//.SetEase(Ease.OutSine);
            aimTarget.transform.localPosition = new Vector3(0, 0, 9f);
        }
    }

}
