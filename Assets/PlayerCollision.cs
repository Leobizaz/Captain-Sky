using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    GameObject parent;
    bool crashed;
    bool crashou;
    Vector3 crashPosition;
    public GameObject debug_aviao;
    public Animator fade;

    float savedSpeed;
    float oldSpeed;

    Quaternion savedRotation;
    Quaternion oldRotation;

    [SerializeField] Vector3 savedPosition;
    [SerializeField] Vector3 oldPosition;

    public PlayerOpenMovement movement;

    private void Start()
    {
        parent = transform.parent.gameObject;
        StartCoroutine(SavePosition());
    }

    private void Update()
    {
        debug_aviao.transform.position = oldPosition;
        debug_aviao.transform.rotation = oldRotation;

        if(crashPosition != Vector3.zero)
        {
            if(Vector3.Distance(crashPosition, parent.transform.position) > 200)
            {
                crashPosition = Vector3.zero;
                crashed = false;
            }
            else
            {
                crashed = true;
            }
        }
    }

    IEnumerator SavePosition()
    {
        while (true)
        {
            if (!crashed)
            {
                if (savedPosition != null)
                {
                    oldPosition = savedPosition;
                    oldSpeed = savedSpeed;
                    oldRotation = savedRotation;
                }
                savedRotation = parent.transform.localRotation;
                savedSpeed = movement.forwardSpeed;
                savedPosition = parent.transform.position;
            }
            yield return new WaitForSeconds(2f);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Collision"))
        {
            crashPosition = parent.transform.position;
            Crash();
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if(other.gameObject.CompareTag("Collision") && !crashou)
        {
            crashPosition = parent.transform.position;
            Crash();
        } 
    }

    public void Crash()
    {
        fade.Play("QuickFadeOut");
        crashou = true;
        Debug.Log("Crashed");
        movement.crashed = true;
        movement.forwardSpeed = 0;

        Invoke("Teleport", 1f);

    }

    public void Teleport()
    {
        fade.Play("QuickFadeIn");
        movement.crashed = false;
        if (Vector3.Distance(crashPosition, savedPosition) < 250)
        {
            movement.forwardSpeed = oldSpeed;
            parent.transform.position = oldPosition;
            parent.transform.localRotation = oldRotation;

            /*
            Quaternion newRot = parent.transform.localRotation;
            newRot.x = 0;
            parent.transform.localRotation = newRot;
            */

        }
        else
        {
            movement.forwardSpeed = savedSpeed;
            parent.transform.position = savedPosition;
            parent.transform.localRotation = savedRotation;

            /*
            Quaternion newRot = parent.transform.localRotation;
            newRot.x = 0;
            parent.transform.localRotation = newRot;
            */
        }
    }

}
