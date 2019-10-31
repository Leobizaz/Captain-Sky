using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UnderwaterEffects : MonoBehaviour
{
    public GameObject particleFX;
    public GameObject mergulhoFX;
    public GameObject vento;
    public GameObject bigsplash;
    public GameObject cameraLayer;
    public GameObject snowFX;
    public SpriteRenderer helice;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "AllyTrigger" && other.name == "Transform")
        {
            helice.DOColor(new Color(255, 255, 255, 0), 6f);
        }




        if(other.tag == "AllyTrigger" && other.name == "MergulhoTrigger")
        {
            RaycastHit hit;
            //transform.LookAt(other.transform);
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                Debug.Log("Point of contact: " + hit.point);
                bigsplash.transform.position = hit.point;
            }


            bigsplash.SetActive(true);
            mergulhoFX.SetActive(true);

            particleFX.SetActive(true);
            vento.SetActive(false);
            
        }

        if(other.tag == "AllyTrigger" && other.name == "UpdateCamera")
        {
            cameraLayer.SetActive(true);
            snowFX.SetActive(false);
        }
    }

    public void Enable()
    {
        cameraLayer.SetActive(true);
        particleFX.SetActive(true);
        vento.SetActive(false);
    }

    public void Disable()
    {
        cameraLayer.SetActive(false);
        particleFX.SetActive(false);
        vento.SetActive(true);
    }
}
