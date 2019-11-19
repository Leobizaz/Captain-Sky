using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliadoIA : MonoBehaviour
{
    public GameObject player;
    public GameObject slot;
    public ParticleSystem gun1;
    public ParticleSystem gun2;
    bool shootingg;

    public Transform[] ways;
    int indexway = 0;


    [SerializeField] string currentStatus;

    Quaternion newRot;
    public float speed;
    bool shoot;
    bool following;

    public GameObject target;

    private void Start()
    {
        StartCoroutine(shooting());
        player = GameObject.Find("Gameplay 3");
    }

    private void Update()
    {



        if (target != null)
        {
            if (target.gameObject.tag != "Enemy")
            {
                target = null;
                following = false;
            }


            if (Vector3.Distance(this.gameObject.transform.position, target.transform.position) < 45)
            {
                EscapeManuever();
                shoot = false;
            }

            if(Vector3.Distance(this.gameObject.transform.position, target.transform.position) > 500)
            {
                speed = 120;
            }


            else if (Vector3.Distance(this.gameObject.transform.position, target.transform.position) < 500)
            {
                speed = 30;
                FollowPlayer();
                shoot = true;
            }
        }

        if (following)
        {
            FollowPlayer();
        }
        else
        {
            Wander();
        }

    }

    IEnumerator shooting()
    {
        while (true)
        {
            if (shoot)
            {
                gun1.Play();
                gun2.Play();
            }
            yield return new WaitForSeconds(4f);
        }
    }

    void EscapeManuever()
    {
        currentStatus = "ESCAPING";
        Quaternion escapeRot = Quaternion.Euler(-25, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
        transform.rotation = Quaternion.Lerp(transform.rotation, escapeRot, Time.deltaTime);
        transform.Translate(Vector3.forward * Time.deltaTime * (speed / 2));
    }

    void Wander()
    {
        currentStatus = "WANDERING";
        Vector3 dir = ways[indexway].position - transform.position;
        newRot = Quaternion.LookRotation(dir);
        //transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRot, Time.deltaTime * 5);
        transform.Translate(Vector3.forward * Time.deltaTime * (speed * 2));
        if (Vector3.Distance(transform.position, ways[indexway].position) < 20)
        {
            indexway = Random.Range(0, ways.Length);
        }
    }

    void FollowPlayer()
    {
        currentStatus = "FOLLOWING ENEMY";
        Vector3 lookDir = target.transform.position - transform.position;
        newRot = Quaternion.LookRotation(lookDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && target == null && other.gameObject.name != "Cabeça")
        {
            target = other.gameObject;
            FollowPlayer();
            following = true;
        }
    }
}
