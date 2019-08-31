﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastMira : MonoBehaviour
{

    public GameObject alvo;
    public GameObject crosshair;
    public LayerMask layermask;
    public EmissorPlayer emissor;
    public GameObject marked;
    public GameObject miraprecisa;

    void Update()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000f, Color.green);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1000f, layermask))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                CancelInvoke("Unmark");
                crosshair.SetActive(true);
                marked = hit.transform.gameObject;
            }
        }
        else
        {
            if(!IsInvoking("Unmark"))
                Invoke("Unmark", 0.5f);
        }

        if(marked != null)
        {
            alvo.transform.position = marked.transform.position;
            emissor.mira = marked;
        }
        else
        {
            crosshair.SetActive(false);
            emissor.mira = miraprecisa;
        }

    }

    void Unmark()
    {
        marked = null;
    }
}