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

    // Start is called before the first frame update
    void Start()
    {
        SetSpeed(progressionSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        MovimentoLocal(x, y, horizontalSpeed);
        RotationLook(x, y, lookSpeed);
        Inclinada(modelo.transform, x, 45, 0.1f);

        if (Input.GetButtonDown("Fire3"))
            Boost(true);

        if (Input.GetButtonUp("Fire3"))
            Boost(false);

        if (Input.GetButtonDown("Fire1"))
            Break(true);

        if (Input.GetButtonUp("Fire1"))
            Break(false);

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

    void RotationLook(float x, float y, float speed)
    {
        aimTarget.parent.position = Vector3.zero;
        aimTarget.localPosition = new Vector3(x, y, 1);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(aimTarget.position), Mathf.Deg2Rad * speed * Time.deltaTime);
    }

    void Inclinada(Transform target, float eixo, float limite, float lerpTime)
    {
        Vector3 targetEulerAngles = target.localEulerAngles;
        target.localEulerAngles = new Vector3(targetEulerAngles.x, targetEulerAngles.y, Mathf.LerpAngle(targetEulerAngles.z, -eixo * limite, lerpTime));
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


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(aimTarget.position, 0.5f);
        Gizmos.DrawSphere(aimTarget.position, 0.15f);
    }
}
