using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    public bool targetLocked;
    public GameObject target;

    public ParticleSystem gun1;
    public ParticleSystem gun2;

    public GameObject guns;


    bool isDoingRandomMovement;

    bool brakes;

    bool movementCooldown;

    public float x;
    public float y;

    float yVelocity;

    void Start()
    {
        yVelocity = 0f;
    }

    void Update()
    {
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

            if((!isDoingRandomMovement && !movementCooldown))
                RandomMovement();

            MovimentoLocal(x, y, horizontalSpeed);
            Inclinada(mesh.transform, -y, 25, 0.1f);
            InclinadaPraCima(mesh.transform, x, 25, 0.1f);

            ClampPosition();

           


            if(targetLocked && target != null)
            {
                /*
                x = 0;
                y = 0;
                Vector3 newPos = new Vector3(0,0,0);

                newPos.x = Mathf.SmoothDamp(transform.localPosition.x, target.transform.position.x, ref yVelocity, 0.05f);
                newPos.y = Mathf.SmoothDamp(transform.localPosition.y, target.transform.position.y, ref yVelocity, 0.05f);
                newPos.z = transform.localPosition.z;
                transform.localPosition = newPos;
                */
                gun1.Play();
                gun2.Play();
                guns.transform.LookAt(target.transform.position);

                //Vector3 localPos = transform.TransformPoint(transform.localPosition);
                //localPos.x = target.transform.position.x;
                // localPos.y = target.transform.position.y;

                //localPos = transform.InverseTransformPoint(localPos);
                //transform.localPosition = localPos;



                //localPos.x = target.transform.InverseTransformPoint(target.transform.position).x;
                //localPos.y = target.transform.InverseTransformPoint(target.transform.position).y;
                //transform.localPosition = localPos;
                //transform.localPosition = Vector3.MoveTowards(transform.localPosition, localPos, 1000);
            }
            else
            {
                //gun1.Stop();
                //gun2.Stop();
            }





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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!targetLocked)
            {
                targetLocked = true;
                target = other.gameObject;
                //StopFakeInput();
                Invoke("RefreshEnemy", 4f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(target != null)
        if(other.name == target.name)
        {
            RefreshEnemy();
        }
    }

    void RefreshEnemy()
    {
        targetLocked = false;
        target = null;
    }

    public void StopFakeInput()
    {
        isDoingRandomMovement = false;
        brakes = true;
        movementCooldown = true;
        if (!IsInvoking("MovementCooldown"))
        Invoke("MovementCooldown", Random.Range(0.4f, 2f));
    }

    public void MovementCooldown()
    {
        movementCooldown = false;
    }

    public void RandomMovement()
    {
        bool doRandom = true;
        brakes = false;
        isDoingRandomMovement = true;

        if (transform.localPosition.y > 10)
            y = Mathf.SmoothDamp(y, -1, ref yVelocity, 0.03f);

        if (transform.localPosition.y < -10)
            y = Mathf.SmoothDamp(y, 1, ref yVelocity, 0.03f);

        if (transform.localPosition.x > 21)
            x = Mathf.SmoothDamp(x, -1, ref yVelocity, 0.03f);

        if (transform.localPosition.x < -21)
            x = Mathf.SmoothDamp(x, 1, ref yVelocity, 0.03f);

        if (doRandom)
        {
            int random = Random.Range(0, 3);
            switch (random)
            {
                case 0:
                    x = Mathf.SmoothDamp(x, 0, ref yVelocity, 0.93f);
                    y = Mathf.SmoothDamp(y, 1, ref yVelocity, 0.03f);
                    if (transform.localPosition.y > 10)
                        y = Mathf.SmoothDamp(y, -1, ref yVelocity, 0.03f);
                    break;
                case 1:
                    x = Mathf.SmoothDamp(x, 0, ref yVelocity, 0.93f);
                    y = Mathf.SmoothDamp(y, -1, ref yVelocity, 0.03f);
                    if (transform.localPosition.y < -10)
                        y = Mathf.SmoothDamp(y, 1, ref yVelocity, 0.03f);
                    break;
                case 2:
                    x = Mathf.SmoothDamp(x, 1, ref yVelocity, 0.03f);
                    y = Mathf.SmoothDamp(y, 0, ref yVelocity, 0.93f);
                    if (transform.localPosition.x > 21)
                        x = Mathf.SmoothDamp(x, -1, ref yVelocity, 0.03f);
                    break;
                case 3:
                    x = Mathf.SmoothDamp(x, -1, ref yVelocity, 0.03f);
                    y = Mathf.SmoothDamp(y, 0, ref yVelocity, 0.93f);
                    if (transform.localPosition.x < -21)
                        x = Mathf.SmoothDamp(x, 1, ref yVelocity, 0.03f);
                    break;
            }
        }
        if (!IsInvoking("StopFakeInput"))
            Invoke("StopFakeInput", Random.Range(0.5f, 3f));

    }

    public void GetReadyToMove()
    {
        readyToMove = true;
    }

}
