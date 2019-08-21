using System.Collections;
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
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.F) && !manobra)
        {
            ManobraVoltar();
        }

    }

    void Movement()
    {

        if (!manobra)
            vectorinput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        else
            vectorinput = new Vector3(manobra_x, manobra_y, manobra_z);

        //if (!manobra)
       // {
            HorizontalLean(playerMesh.transform, vectorinput.x, 40, .1f);
            VerticalLean(playerMesh.transform, -vectorinput.y, 15, .1f);
       // }



        transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed);
        Quaternion yawRot = Quaternion.Euler(new Vector3(vectorinput.y * yawSpeed, 0, 0));
        Quaternion rollRot = Quaternion.Euler(new Vector3(0, 0, -vectorinput.x * rollSpeed));
        transform.localRotation = transform.localRotation * yawRot * rollRot;
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
}
