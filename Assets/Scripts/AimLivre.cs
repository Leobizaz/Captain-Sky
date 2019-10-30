using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimLivre : MonoBehaviour
{
    public GameObject target;
    public RaycastMira mira;
    public bool targetLocked;
    public LayerMask layermask;

    private void Update()
    {
        
        if (targetLocked)
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position, (target.transform.position - transform.position), Color.green);
            if (Physics.Raycast(transform.position, (target.transform.position - transform.position), out hit, Mathf.Infinity, layermask))
            {
                if (hit.transform.CompareTag("Terrain"))
                {
                    LoseTarget();
                }
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!targetLocked)
            {
                mira.livre = true;
                target = other.gameObject;
                mira.marked = target;
                Invoke("LoseTarget", 2f);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (targetLocked && other.name == target.name)
            {
                CancelInvoke("LoseTarget");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if(targetLocked && target != null)
            {
                LoseTarget();
            }
        }
    }

    public void LoseTarget()
    {
        mira.livre = false;
        target = null;
        mira.marked = null;
        targetLocked = false;
    }


}
