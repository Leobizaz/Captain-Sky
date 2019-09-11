using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAlly : MonoBehaviour
{
    public GameObject mesh;

    private bool esquerda = false;
    private bool direita = false;
    public float horizontalSpeed = 7;
    private bool pressedOnce;
    private float time;
    private float timerLength;
    public Camera boundaryCamera;
    public bool readyToMove;

    void Start()
    {

    }

    void Update()
    {
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if(!IsInvoking("GetReadyToMove"))
                Invoke("GetReadyToMove", Random.Range(0.1f, 1f));
            //GetReadyToMove();
        }

        if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            //CancelInvoke("GetReadyToMove");
            readyToMove = false;
        }



        if (!Pause.paused && !Pause.victory)
        {
            float x;
            float y;
            float x2;
            float y2;
            y2 = Input.GetAxis("VerticalDireito");
            x2 = Input.GetAxis("HorizontalDireito");


            if (!PlayerHealth.dead && readyToMove)
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

            MovimentoLocal(x, y, horizontalSpeed);
            Inclinada(mesh.transform, -y, 25, 0.1f);
            InclinadaPraCima(mesh.transform, x, 25, 0.1f);

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
                    //BarrelRoll(-1);
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
                    //BarrelRoll(1);
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

            ClampPosition();

        }

    }

    void MovimentoLocal(float x, float y, float speed)
    {
        transform.localPosition += new Vector3(x, y, 0) * speed * Time.deltaTime;
    }


    void Inclinada(Transform target, float eixo, float limite, float lerpTime)
    {
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

    void ClampPosition()
    {
        Vector3 pos = boundaryCamera.WorldToViewportPoint(transform.position);

        //Boundary em relação a camera

        pos.x = Mathf.Clamp(pos.x, 0.1f, 0.9f);
        pos.y = Mathf.Clamp(pos.y, 0.1f, 0.9f);

        transform.position = boundaryCamera.ViewportToWorldPoint(pos);
    }

    public void GetReadyToMove()
    {
        readyToMove = true;
    }

}
