using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookMechanic : MonoBehaviour
{
    bool hooked;
    Rigidbody playerRB;
    PlayerOpenMovement openMovement;

    private void Start()
    {
        openMovement = GetComponent<PlayerOpenMovement>();
        playerRB = GetComponent<Rigidbody>();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hookable"))
        {
            Debug.Log(other.name);
            HingeJoint joint = other.GetComponent<HingeJoint>();
            if (Input.GetButtonDown("P.O.V"))
            {
                hooked = !hooked;

                if (hooked)
                {
                    playerRB.AddForce(Vector3.forward * 10);
                    openMovement.enabled = false;
                    joint.connectedBody = playerRB;
                }
                else
                {
                    openMovement.enabled = true;
                    joint.connectedBody = null;
                }

            }
        }
    }
}
