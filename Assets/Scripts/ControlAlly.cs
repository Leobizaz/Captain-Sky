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

    bool isDoingRandomMovement;

    bool brakes;

    bool movementCooldown;

    public float x;
    public float y;

    void Start()
    {

    }

    void Update()
    {
        //get inputs from player 
        /*
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
        */

        if (brakes)
        {
            x = Mathf.Lerp(x, 0, 1 * Time.deltaTime);
            y = Mathf.Lerp(y, 0, 1 * Time.deltaTime);
        }






        if (!Pause.paused && !Pause.victory)
        {
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
                //x = 0;
                //y = 0;
            }

            if(!isDoingRandomMovement && !movementCooldown)
                RandomMovement();

            MovimentoLocal(x, y, horizontalSpeed);
            Inclinada(mesh.transform, -y, 25, 0.1f);
            InclinadaPraCima(mesh.transform, x, 25, 0.1f);
            /*
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
            */

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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ally"))
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, other.transform.localPosition, -1 * 1 * Time.deltaTime);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -7.3f);
        }
    }

    public void StopFakeInput()
    {
        isDoingRandomMovement = false;
        brakes = true;
        movementCooldown = true;
        if (!IsInvoking("MovementCooldown"))
        Invoke("MovementCooldown", Random.Range(1f, 3f));
    }

    public void MovementCooldown()
    {
        movementCooldown = false;
    }

    public void RandomMovement()
    {
        brakes = false;
        isDoingRandomMovement = true;
        int random = Random.Range(0, 3);
        switch (random)
        {
            case 0:
                x = 0;
                y = 1;
                break;
            case 1:
                x = 0;
                y = -1;
                break;
            case 2:
                x = 1;
                y = 0;
                break;
            case 3:
                x = -1;
                y = 0;
                break;
        }
        if (!IsInvoking("StopFakeInput"))
            Invoke("StopFakeInput", Random.Range(0.5f, 1f));

    }

    public void GetReadyToMove()
    {
        readyToMove = true;
    }

}
