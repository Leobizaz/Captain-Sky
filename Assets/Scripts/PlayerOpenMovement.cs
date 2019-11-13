 using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PlayerOpenMovement : MonoBehaviour
{

    [Header("Controles")]
    public float forwardSpeed = 10;
    public float sensibilidade = 0.8f;
    public float rollSpeed = 2;
    public float yawSpeed = 1;
    [Space(5)]
    public LayerMask layerMask;
    public LayerMask layerMask2;

    [Header("Debug")]
    public float ignora_isso;
    public RaycastHit hit;
    [SerializeField] Vector3 vectorinput;
    [SerializeField] bool manobra;
    float storedSpeed;
    float manobra_x;
    float manobra_y;
    float manobra_z;
    float rotation;
    public static int ded = 1;
    [SerializeField] bool boost;
    public bool crashed;
    [Header("Referencias")]
    public GameObject cameraHolder;
    public PlayerCollision colisao;
    public GameObject playerMesh;
    public Animator anim;
    public ParticleSystem shoot;
    public ParticleSystem shoot2;
    public AudioSource audioSource;
    public CameraFollow CMCamera1;
    public Transform cameraParent;
    public GameObject modelooo;
    public SpriteRenderer mira;
    public GameObject radarIMG;
    public GameObject oculosHUD;
    public Transform aimTarget;
    public bool playerActive;
    public bool canFirstPerson;
    public GameObject cockpit;
    public GameObject firstPersonCamera;
    [SerializeField] public static bool firstPerson;
    private bool rotate;

    void Update()
    {

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float y2 = Input.GetAxis("VerticalDireito");
        float x2 = Input.GetAxisRaw("HorizontalDireito");


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





        float currentVelocity = 0;

        if (yawSpeed <= 0.15f)
        {
            yawSpeed = 0.15f;
        }

        if (yawSpeed >= 2)
        {
            yawSpeed = 2;
        }

        if(rollSpeed >= 5f)
        {
            rollSpeed = 5f;
        }

        if(rollSpeed <= 2f)
        {
            rollSpeed = 2f;
        }


        RaycastHit hit;
        if (!crashed)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask) && hit.collider.gameObject.CompareTag("Sky"))
            {
                forwardSpeed -= 0.5f;
                rollSpeed += 0.0355f;
                yawSpeed += 0.0355f;
                SetCameraZoom(15f, 5f);


                if (forwardSpeed <= 5f)
                {
                    forwardSpeed = 5f;
                }
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("Céu");

            }
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask) && hit.collider.gameObject.CompareTag("Ground"))
            {
                forwardSpeed += 1.20f;
                yawSpeed -= 0.0155f;
                rollSpeed -= 0.0155f;
                SetCameraZoom(-15f, 5f);

                if (forwardSpeed >= 180f)
                {
                    forwardSpeed = 180f;
                }
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("Chaum");
            }

            if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 200f, layerMask2) || Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 25f, layerMask2))
            {
                colisao.dangerous = true;
            }
            else
            {
                colisao.dangerous = false;
            }

            if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && Input.GetAxis("Vertical") == 0)
            {
                if(forwardSpeed <= 100)
                    forwardSpeed += 1f;
                if (rollSpeed >= 2)
                    rollSpeed -= 0.05f;
            }

            if (Input.GetKeyUp(KeyCode.UpArrow) && Input.GetKeyUp(KeyCode.DownArrow))
                y2 = 0;
            //if ((y2 > 0 || Input.GetKey(KeyCode.UpArrow) && forwardSpeed <= 125))

            if ((y2 > 0 || Input.GetKey(KeyCode.UpArrow)) && forwardSpeed <= 125)
            {
                SetCameraZoom(-6f, 3.0f);
                forwardSpeed += 0.45f;
                yawSpeed -= 0.0055f;
                rollSpeed -= 0.0055f;
            }

            if ((y2 < 0 || Input.GetKey(KeyCode.DownArrow)) && forwardSpeed >= 65)
            {
                SetCameraZoom(6f, 3f);
                forwardSpeed -= 0.45f;
                yawSpeed += 0.0475f;
                rollSpeed += 0.0475f;
            }
            if (y2 == 0)
            {
                SetCameraZoom(0f, 3f);
            }

            if ((y2 < 0 || Input.GetKey(KeyCode.DownArrow)) && forwardSpeed >= 65)
            {
                SetCameraZoom(6f, 3f);
                forwardSpeed -= 0.45f;
                yawSpeed += 0.0475f;
                rollSpeed += 0.0475f;
            }


            if (Input.GetButtonDown("Boost"))
                boost = true;
            if (Input.GetButtonUp("Boost"))
                boost = false;
            if (Input.GetButtonDown("Break"))
                cameraHolder.transform.Rotate(0, 180, 0, Space.Self);
            if (Input.GetButtonUp("Break"))
                cameraHolder.transform.Rotate(0, 0, 0, Space.Self);


            //anim.SetFloat("xInput", x);
            //anim.SetFloat("yInput", y);

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

            if (boost == true)
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
            vectorinput.x = Mathf.SmoothDamp(vectorinput.x, Input.GetAxisRaw("HorizontalDireito") * sensibilidade, ref currentVelocity, 0.03f);
            if (Pause.controleInvertido)
                vectorinput.y = Mathf.SmoothDamp(vectorinput.y, -((Input.GetAxis("Vertical") * 1.2f)) * sensibilidade, ref currentVelocity, 0.03f);
            else vectorinput.y = Mathf.SmoothDamp(vectorinput.y, (Input.GetAxis("Vertical") * 1.2f) * sensibilidade, ref currentVelocity, 0.03f);
            rotation = Mathf.SmoothDamp(rotation, Input.GetAxis("Horizontal") * sensibilidade, ref currentVelocity, 0.03f);
        }
        else
            vectorinput = new Vector3(manobra_x, manobra_y, manobra_z);

        if (Input.GetAxis("Horizontal") == 0 || Input.GetAxis("HorizontalDireito") == 0 || Input.GetAxis("Vertical") == 0)
        {
            CMCamera1.offset = new Vector3(0, 5, -12.68f);
            //anim.SetInteger("State", 0);
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (Pause.controleInvertido)
            {
                if (forwardSpeed >= 65)
                {
                    SetCameraZoom(6f, 3f);
                    forwardSpeed -= 0.45f;
                    yawSpeed += 0.0475f;
                    rollSpeed += 0.0475f;
                }
            }
            else
            {
                if (forwardSpeed >= 65)
                {
                    SetCameraZoom(6f, 3f);
                    forwardSpeed -= 0.45f;
                    yawSpeed += 0.0475f;
                    rollSpeed += 0.0475f;
                }
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (Pause.controleInvertido)
            {
                if (forwardSpeed >= 65)
                {
                    SetCameraZoom(6f, 3f);
                    forwardSpeed -= 0.45f;
                    yawSpeed += 0.0475f;
                    rollSpeed += 0.0475f;
                }
            }
            else
            {
                if (forwardSpeed >= 65)
                {
                    SetCameraZoom(6f, 3f);
                    forwardSpeed -= 0.45f;
                    yawSpeed += 0.0475f;
                    rollSpeed += 0.0475f;
                }

            }
        }



        if (Input.GetAxis("Vertical") > 0)
        {
            //SetSpeed(forwardSpeed + 1);
            //para baixo
            //anim.SetInteger("State", 4);
            cameraHolder.transform.DOLocalMoveY(-10, 4);
            CMCamera1.offset = new Vector3(0, 5, -12.68f);
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            //SetSpeed(forwardSpeed - 1);
            //para cima
            //anim.SetInteger("State", 3);
            cameraHolder.transform.DOLocalMoveY(10, 8);
            CMCamera1.offset = new Vector3(0, 7, -12.68f);
        }
        if(Input.GetAxis("Vertical") == 0)
        {
            cameraHolder.transform.DOLocalMoveY(5, 4);
            CMCamera1.offset = new Vector3(0, 0, -12.68f);
        }



        //if (!manobra)
        // {
        HorizontalLean(playerMesh.transform, rotation, 40, .1f);
        HorizontalLean(playerMesh.transform, vectorinput.x, 40, .1f);
            VerticalLean(playerMesh.transform, -vectorinput.y, 15, .1f);
       // }



        transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed * ded);
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
