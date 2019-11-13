using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AveNovaIA : MonoBehaviour
{
    public GameObject player;
    public GameObject slot;
    public ParticleSystem gun1;
    public ParticleSystem gun2;
    bool shootingg;

    [SerializeField] string currentStatus;

    Quaternion newRot;
    public float speed;
    public bool foundSlot;
    bool shoot;

    private void Start()
    {
        StartCoroutine(shooting());
        player = GameObject.Find("Gameplay 3");
    }

    private void Update()
    {
        if (Vector3.Distance(this.gameObject.transform.position, player.transform.position) > 160)
        {
            if (foundSlot)
            {
                FollowSquad();
            }
            else
            {
                //Wander();
            }
            shoot = false;
        }
        
        if(Vector3.Distance(this.gameObject.transform.position, player.transform.position) < 45)
        {
            EscapeManuever();
            shoot = false;
        }
        else if(Vector3.Distance(this.gameObject.transform.position, player.transform.position) < 160)
        {
            FollowPlayer();
            shoot = true;
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
        transform.Translate(Vector3.forward * Time.deltaTime * (speed/2));
    }

    void Wander()
    {
        currentStatus = "WANDERING";
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        Quaternion escapeRot = Quaternion.Euler(-25, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
        transform.rotation = Quaternion.Lerp(transform.rotation, escapeRot, Time.deltaTime);
    }

    void FollowPlayer()
    {
        currentStatus = "FOLLOWING PLAYER";
        Vector3 lookDir = player.transform.position - transform.position;
        newRot = Quaternion.LookRotation(lookDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void FollowSquad()
    {
        currentStatus = "FOLLOWING SQUAD";
        Vector3 dir = slot.transform.position - transform.position;
        newRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime);
        transform.Translate(Vector3.forward * Time.deltaTime * (speed / 2));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Squadleader" && !foundSlot)
        {
            if (!other.GetComponent<SquadLeader>().full)
            {
                other.GetComponent<SquadLeader>().AddMember(this.gameObject);
                foundSlot = true;
            }
            
        }
    }


}
