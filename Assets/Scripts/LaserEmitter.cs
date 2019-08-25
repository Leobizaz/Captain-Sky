﻿using System.Collections;
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

        laserbeam.SetPosition(0, gameObject.transform.position);
        laserbeam.SetPosition(1, receiver.transform.position);

        int layermask = 1 << 9;
        layermask = ~layermask;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, laserDistance, layermask))
        {
            receiver.transform.LookAt(gameObject.transform);
            receiver.transform.position = hit.point;
            Debug.Log(hit.collider.gameObject.name);
        }
        else
        {
            
            //receiver.transform.localRotation = transform.localRotation;
            receiver.transform.localPosition = this.gameObject.transform.TransformDirection(Vector3.forward * laserDistance);
        }
    }
}
