using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AveIA : MonoBehaviour
{
    Transform[] ways;
    public GameObject wayfather;

    public float speed;
    public float maxHealth;
    public float currentHealth;
    public float fakeHealth;

    List<GameObject> avesNearby;

    public GameObject explosionFX;
    public GameObject smoke;
    public GameObject model;
    public GameObject mapSphere;
    public AudioClip[] audios;
    public AudioClip explosionSFX;

    AudioSource audio;
    Rigidbody rb;
    public GameObject target;
    public GameObject player;
    int indexway = 0;
    bool died;

    bool targetFound;
    bool cooldown;
    Quaternion newRot;

    void Start()
    {
        avesNearby = new List<GameObject>();
        player = GameObject.Find("Testplayer");
        if (player == null) player = GameObject.Find("Gameplay 3");
        fakeHealth = maxHealth;
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        if(wayfather != null)
        ways = wayfather.GetComponentsInChildren<Transform>();
    }
    void Update()
    {

        float sphereSize = 1000 / ((Vector3.Distance(player.transform.position, this.transform.position)) / 12);
        sphereSize = Mathf.Clamp(sphereSize, 25, 150);
        mapSphere.transform.DOScale(sphereSize, 1);

        KeepDistance();

        if (!died)
        {
            if (fakeHealth <= 0) FakeDeath();

            if (currentHealth <= 0) Death();

            if (wayfather != null)
            {
                Vector3 dir = ways[indexway].position - transform.position;
                newRot = Quaternion.LookRotation(dir);
            }
            if (wayfather == null) FlyAround();
            else if (!targetFound)
            {
                Wander();
            }

            if (target != null)
                if (Vector3.Distance(transform.position, target.transform.position) < 25) EscapeManuever();

            if (targetFound && !cooldown)
            {
                Chase();
            }
            else if (targetFound && cooldown)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }

           

        }
    }

    private void LateUpdate()
    {

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

    private void OnTriggerEnter(Collider other)
    {
        bool once = false;
        if(other.tag == "Enemy" && !once)
        {
            once = true;
            avesNearby.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        bool once = false;
        if(other.tag == "Enemy" && !once)
        {
            once = true;
            avesNearby.Remove(other.gameObject);
        }
    }

    void KeepDistance()
    {
        float distance = 10;
        if (avesNearby.Count > 0)
        {
            foreach (GameObject ave in avesNearby)
            {
                if (ave != null)
                {
                    distance = Vector3.Distance(this.transform.position, ave.transform.position);
                    if (distance < 100)
                    {
                        transform.position = (transform.position - ave.transform.position).normalized * distance + ave.transform.position;
                    }
                }
            }
        }
    }

    void Wander()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime);
        transform.Translate(Vector3.forward * Time.deltaTime * (speed / 2));
        if (Vector3.Distance(transform.position, ways[indexway].position) < 2)
        {
            indexway++;
            if (indexway == ways.Length) indexway = 0;
        }
    }

    void Chase()
    {
        Vector3 lookDir = target.transform.position - transform.position;
        newRot = Quaternion.LookRotation(lookDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
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

    void Death()
    {
        if (!died)
        {
            ScoreSystem.currentScore += 1000f;
            GameObject.Find("Game Manager").GetComponent<ScoreSystem>().UpdateScore();
            Instantiate(explosionFX, model.transform.position, model.transform.localRotation);
            audio.pitch = Random.Range(0.8f, 1.2f);
            audio.PlayOneShot(explosionSFX);
            gameObject.tag = "DeadEnemy";
            ScoreSystem.enemysKill++;
        }
        rb.isKinematic = false;
        smoke.SetActive(true);

        died = true;
        rb.useGravity = true;
        //gun1.Stop();
        //gun2.Stop();
        model.transform.DOLocalRotate(new Vector3(360, 0, 0), 10f, RotateMode.LocalAxisAdd);
        Invoke("Despawn", 10f);
    }

    private void OnParticleCollision(GameObject other)
    {
        //Debug.Log("COLIDIU");
        if (other.tag == "Shoot")
        {
            //play hit fx
            audio.pitch = 0.7f;
            audio.PlayOneShot(audios[Random.Range(0, audios.Length)]);


            currentHealth = currentHealth - 10f;
        }

        if (other.tag == "Ally")
        {
            audio.pitch = 0.7f;
            audio.PlayOneShot(audios[Random.Range(0, audios.Length)]);


            fakeHealth = fakeHealth - 10f;
        }
    }

    void FakeDeath()
    {
        Instantiate(explosionFX, model.transform.position, model.transform.localRotation);
        audio.pitch = Random.Range(0.8f, 1.2f);
        audio.PlayOneShot(explosionSFX);
        gameObject.tag = "DeadEnemy";
        rb.isKinematic = false;
        smoke.SetActive(true);

        died = true;
        rb.useGravity = true;
        //gun1.Stop();
        //gun2.Stop();
        model.transform.DOLocalRotate(new Vector3(360, 0, 0), 10f, RotateMode.LocalAxisAdd);
        Invoke("Despawn", 10f);
    }

    public void Despawn()
    {
        Destroy(this.gameObject);
    }

}
