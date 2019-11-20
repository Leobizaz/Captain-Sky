using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RobozaoWalk : MonoBehaviour
{
    public Animator meshAnim;
    private Rigidbody rb;
    public float moveAmmount;
    public float duration;
    public float timeForStep;
    public GameObject target;
    GameObject closest;
    public DestructableBuilding building;
    public float damping;
    public float attackRange;
    bool onceAttack;
    [SerializeField] bool isAttacking;
    bool moving;
    [SerializeField] bool isWithinAttackRange;
    bool tryingToMove;
    public GameObject laserPosition;
    public GameObject alvo;
    RobozaoHealth healthScript;
    public GameObject laserimpact;
    AudioSource audio;
    bool once;

    public LineRenderer laser;

    public void Start()
    {
        audio = GetComponent<AudioSource>();
        healthScript = GetComponent<RobozaoHealth>();
        rb = GetComponent<Rigidbody>();
        laser.SetPosition(0, laserPosition.transform.localPosition);
        Invoke("PassoEsq", 3f + Random.Range(0f, 2f));
        tryingToMove = true;
        StartCoroutine(Attacking());
    }


    private void Update()
    {
        if (!healthScript.dead)
        {

            if (target == null)
            {
                FindTarget();
            }


            RotateToTarget();

            if (Vector3.Distance(target.transform.position, this.transform.position) < attackRange)
                isWithinAttackRange = true;
            else
                isWithinAttackRange = false;

            if (moving)
            {
                transform.position += (transform.forward * moveAmmount * Time.deltaTime);
            }

            if (!isWithinAttackRange)
            {
                isAttacking = false;
            }

            if (isWithinAttackRange)
            {
                tryingToMove = false;
                //meshAnim.Play("")
                moving = false;
                if (target != null && building != null)
                {
                    isAttacking = true;
                }

            }
            else
            {
                if (!tryingToMove)
                    PassoEsq();
                isAttacking = false;
                StopCoroutine(Attacking());
            }

            if (building.tag != "Building")
            {
                target = null;
            }

            if (isAttacking)
            {
                CancelInvoke("PassoEsq");
                CancelInvoke("PassoDir");

                if (!audio.isPlaying)
                    audio.Play();

                if (!onceAttack)
                {
                    onceAttack = true;
                    meshAnim.Play("LASER");
                }
                laser.gameObject.SetActive(true);
                laser.SetPosition(0, laserPosition.transform.position);
                alvo.transform.position = target.transform.position;
                laser.SetPosition(1, alvo.transform.position);
                laserimpact.SetActive(true);
                laserimpact.transform.position = alvo.transform.position;
            }
            else
            {
                onceAttack = false;
                audio.Stop();
                laserimpact.SetActive(false);
                laser.gameObject.SetActive(false);
            }
        }
        else
        {
            if (!once)
            {
                once = true;
                meshAnim.Play("MORTO");
            }
            audio.Stop();
            isAttacking = false;
            laser.gameObject.SetActive(false);
        }

    }

    public void FindTarget()
    {
        target = FindClosestEnemy();
        building = target.GetComponent<DestructableBuilding>();
    }


    void RotateToTarget()
    {
        var lookPos = target.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }

    IEnumerator Attacking()
    {
        while (true)
        {
            if (isAttacking)
            {
                building.GetHit();
            }
            yield return new WaitForSeconds(2);

        }
    }



    public void PassoEsq()
    {
        if (!once)
        {
            tryingToMove = true;
            meshAnim.Play("PassoEsq");
            //transform.DOLocalMove(transform.position += (transform.forward * moveAmmount), duration).SetEase(Ease.InOutQuad);
            //rb.DOMove(transform.position += (transform.forward * moveAmmount), duration).SetEase(Ease.InOutQuad);
            moving = true;
            Invoke("StopMoving", duration);

            if (!isWithinAttackRange)
                Invoke("PassoDir", timeForStep);
        }
    }

    public void StopMoving()
    {
        moving = false;
    }

    public void PassoDir()
    {
        if (!once)
        {
            tryingToMove = true;
            meshAnim.Play("PassoDir");
            //transform.DOLocalMove(transform.position += (transform.forward * moveAmmount), duration).SetEase(Ease.InOutQuad);
            //rb.DOMove(transform.position += (transform.forward * moveAmmount), duration).SetEase(Ease.InOutQuad);
            moving = true;
            Invoke("StopMoving", duration);
            if (!isWithinAttackRange)
                Invoke("PassoEsq", timeForStep);
        }
    }

    GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Building");
        GameObject closest;
        closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }



}
