using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEmitter : MonoBehaviour
{
    public GameObject receiver;
    public LineRenderer laserbeam;
    public float laserDistance;

    void Start()
    {
        
    }

    void Update()
    {

        laserbeam.SetPosition(0, gameObject.transform.localPosition);
        laserbeam.SetPosition(1, receiver.transform.localPosition);

        int layermask = 1 << 9;
        layermask = ~layermask;
        RaycastHit hit;
        if(Physics.Raycast(transform.localPosition, this.transform.forward, out hit, laserDistance, layermask))
        {
            receiver.transform.LookAt(gameObject.transform);
            receiver.transform.localPosition = hit.point;
            Debug.Log(hit.collider.gameObject.name);
            if(hit.collider.gameObject.tag == "Player")
            {
                hit.collider.gameObject.GetComponent<PlayerHealth>().GetHit(20);
            }
        }
        else
        {
            //var localDirection = transform.rotation * Vector3.forward;
           
            var localDirection = this.transform.forward;


            //receiver.transform.localRotation = transform.localRotation;
            receiver.transform.localPosition = this.gameObject.transform.localPosition + (localDirection * laserDistance);
        }
    }

    public void Kill()
    {
        Destroy(laserbeam.gameObject);
    }
}
