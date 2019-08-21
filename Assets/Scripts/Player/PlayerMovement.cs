using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering.PostProcessing;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{

    public float horizontalSpeed = 10;
    public float progressionSpeed = 6;
    public float lookSpeed = 5f;
    public GameObject modelo;
    public Transform aimTarget;
    public CinemachineDollyCart dolly;
    public Transform cameraParent;
    public GameObject cursor;
    private bool esquerda = false;
    private bool direita = false;
    public ParticleSystem shoot;


    // Start is called before the first frame update
    void Start()
    {
        SetSpeed(progressionSpeed);
        shoot.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = -Input.GetAxis("Vertical");

        MovimentoLocal(x, y, horizontalSpeed);
            //RotationLook(x, y, 1, lookSpeed);
            Inclinada(modelo.transform, x, 45, 0.1f);
        InclinadaPraCima(modelo.transform, y, 45, 0.1f);


        if (Input.GetButtonDown("Fire3"))
            Boost(true);

        if (Input.GetButtonUp("Fire3"))
            Boost(false);

        if (Input.GetButtonDown("Fire2"))
            shoot.Play();

        if (Input.GetButtonUp("Fire2"))
            shoot.Stop();

        if (Input.GetButtonDown("Fire1"))
            Break(true);

        if (Input.GetButtonUp("Fire1"))
            Break(false);

        if (Input.GetButtonDown("Left") )
            esquerda = true;

        if (Input.GetButtonUp("Left"))
            esquerda = false;

        if (Input.GetButtonDown("Right"))
            direita = true;

        if (Input.GetButtonUp("Right"))
            direita = false;

    }

    void MovimentoLocal(float x, float y, float speed)
    {
        transform.localPosition += new Vector3(x, y, 0) * speed * Time.deltaTime;
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
        target.localEulerAngles = new Vector3(targetEulerAngles.x, targetEulerAngles.y, Mathf.LerpAngle(targetEulerAngles.z, -eixo * limite, lerpTime));
        else if(esquerda == true)
            target.localEulerAngles = new Vector3(targetEulerAngles.x, targetEulerAngles.y, Mathf.LerpAngle(targetEulerAngles.z + 10f, -eixo * limite, lerpTime));
        else if (direita == true)
            target.localEulerAngles = new Vector3(targetEulerAngles.x, targetEulerAngles.y, Mathf.LerpAngle(targetEulerAngles.z - 10f, -eixo * limite, lerpTime));
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

        DOVirtual.Float(dolly.m_Speed, speed, .15f, SetSpeed);
        SetCameraZoom(zoom, .4f);
    }

    void Boost(bool state)
    {
        float speed = state ? progressionSpeed * 2 : progressionSpeed;
        float zoom = state ? -7 : 0;

        DOVirtual.Float(dolly.m_Speed, speed, .15f, SetSpeed);
        SetCameraZoom(zoom, .4f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Boost")
        {
            Debug.Log("Boost ON");
            SetSpeed(progressionSpeed + 20f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Boost")
        {
            Debug.Log("Boost OFF");
            SetSpeed(progressionSpeed);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(aimTarget.position, 0.5f);
        Gizmos.DrawSphere(aimTarget.position, 0.15f);
    }

}
