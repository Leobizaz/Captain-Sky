using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AveIA : MonoBehaviour
{
    Transform[] ways;
    public GameObject wayfather;

    public float speed;

    public GameObject target;
    int indexway = 0;

    bool targetFound;
    bool cooldown;
    Quaternion newRot;

    void Start()
    {
        if(wayfather != null)
        ways = wayfather.GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        if(wayfather != null)
        {
        Vector3 dir = ways[indexway].position - transform.position;
        newRot = Quaternion.LookRotation(dir);
        }
        if(wayfather == null) FlyAround();
        else if(!targetFound)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime);
            transform.Translate(Vector3.forward * Time.deltaTime * (speed/2));
            if(Vector3.Distance(transform.position, ways[indexway].position) < 2)
            {
                indexway++;
                if(indexway == ways.Length) indexway = 0;
            }
        }
        if(target != null)
            if(Vector3.Distance(transform.position, target.transform.position) < 25) EscapeManuever();

        if (targetFound && !cooldown)
        {
            Vector3 lookDir = target.transform.position - transform.position;
            newRot = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        else if(targetFound && cooldown)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

    }

    private void OnTriggerStay(Collider other) 
    {
        if(!targetFound)
        {
            if(other.CompareTag("Player") || other.CompareTag("Ally"))
            {
                targetFound = true;
                target = other.gameObject;
            }
        }
    }

    void EscapeManuever()
    {
        targetFound = false;
        Quaternion escapeRot = Quaternion.Euler(-25, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
        transform.rotation = Quaternion.Lerp(transform.rotation, escapeRot, Time.deltaTime);
        Invoke("End", 8f);
    }

    void End()
    {
        cooldown = false;
    }

    void FlyAround()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

}
