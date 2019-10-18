using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEmitterPlayer : MonoBehaviour
{
    public GameObject receiver;
    public LineRenderer laserbeam;
    public float laserDistance;
    public Vector3 receiverOrigin;
    public LayerMask layermask;

    void Start()
    {
        receiverOrigin = receiver.transform.localPosition;
    }

    void Update()
    {

        laserbeam.SetPosition(0, gameObject.transform.localPosition);
        laserbeam.SetPosition(1, receiver.transform.localPosition);

        RaycastHit hit;
        if (Physics.Raycast(transform.localPosition, this.transform.forward, out hit, laserDistance, layermask))
        {
            receiver.transform.LookAt(gameObject.transform);
            receiver.transform.localPosition = hit.point;
        }
        else
        {
            //var localDirection = transform.rotation * Vector3.forward;
            var localDirection = this.transform.forward;


            //receiver.transform.localRotation = transform.localRotation;
            receiver.transform.localPosition = receiverOrigin;
        }
    }

    public void Kill()
    {
        Destroy(laserbeam.gameObject);
    }
}
