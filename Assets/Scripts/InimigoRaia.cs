using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class InimigoRaia : MonoBehaviour
{
    public float speed;
    public GameObject player;
    public GameObject playerModel;
    public CinemachineDollyCart dollyCart;
    public GameObject mapSphere;
    public GameObject model;
    public GameObject olho;
    public GameObject laserbeamFX;
    public ParticleSystem chargedBallFX;
    public LaserEmitter emitter;

    public float spawnTime;
    bool spawned;

    public float maxHealth;
    public float currentHealth;
    public float fakeHealth;
    public Vector3 lastPosition;
    public GameObject chargingFX;
    public GameObject explosionFX;

    public AudioClip[] audios;
    public AudioClip explosionSFX;
    AudioSource audio;
    Rigidbody rb;
    public bool passive;

    bool laserCooldown;
    bool chargingLaser;
    public bool firingLaser;
    bool once;
    bool died;


    public GameObject target;

    void Start()
    {
        gameObject.tag = "Untagged";
        Invoke("Spawn", spawnTime);
        //dollyCart = GetComponent<CinemachineDollyCart>();
        player = GameObject.Find("Player");
        playerModel = player.transform.GetChild(3).gameObject;
        fakeHealth = maxHealth;
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        laserbeamFX.SetActive(false);
        olho.GetComponent<BoxCollider>().enabled = false;
        //olho.SetActive(false);
    }

    void Update()
    {
        speed = player.transform.parent.GetComponent<CinemachineDollyCart>().m_Speed;
        dollyCart.m_Speed = speed;

        float sphereSize = 1000 / ((Vector3.Distance(player.transform.position, this.transform.position)) / 10);
        sphereSize = Mathf.Clamp(sphereSize, 25, 75);
        mapSphere.transform.DOScale(sphereSize, 1);

        if (spawned && !chargingLaser && !IsInvoking("ChargeLaser") && !once && !passive)
        {
            once = true;
            Invoke("ChargeLaser", Random.Range(2, 4));
        }


        if (!firingLaser)
        {
            olho.transform.LookAt(playerModel.transform.position);
            emitter.receiver.transform.position = playerModel.transform.position;
            lastPosition = playerModel.transform.position;
        }
        else
        {
            olho.transform.LookAt(lastPosition);
            emitter.receiver.transform.position = lastPosition;
        }




        if (!died)
        {
            if (fakeHealth <= 0) FakeDeath();

            if (currentHealth <= 0) Death();

        }
    }

    public void Spawn()
    {
        gameObject.tag = "Enemy";
        spawned = true;
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(model.transform.DOLocalRotate(new Vector3(-90f, 0, 0), 1));
        mySequence.Append(model.transform.DOLocalRotate(new Vector3(-90f, 180, 0), 1));
    }

    void ChargeLaser()
    {
        chargingLaser = true;
        chargingFX.SetActive(true);
        Invoke("Lag", 4.6f);
        Invoke("FireLaser", 5f);
    }

    void Lag()
    {
        firingLaser = true;

    }

    void FireLaser()
    {
        chargedBallFX.Play();
        olho.GetComponent<BoxCollider>().enabled = true;
        chargingLaser = false;
        chargingFX.SetActive(false);
        laserbeamFX.SetActive(true);
        //olho.SetActive(true);
        Invoke("LaserCooldown", 1.5f);
    }

    void LaserCooldown()
    {
        olho.GetComponent<BoxCollider>().enabled = false;
        firingLaser = false;
        laserbeamFX.SetActive(false);
        //olho.SetActive(false);
        Invoke("ResetCooldown", 6f);
    }

    void ResetCooldown()
    {
        Invoke("ChargeLaser", 2f);
    }


    void FakeDeath()
    {
        died = true;
    }

    void Death()
    {
        died = true;
        Instantiate(explosionFX, model.transform.position, model.transform.localRotation);
        audio.pitch = Random.Range(0.8f, 1.2f);
        audio.PlayOneShot(explosionSFX);
        gameObject.tag = "DeadEnemy";
        mapSphere.SetActive(false);
        laserbeamFX.SetActive(false);
        chargingFX.SetActive(false);
        olho.SetActive(false);
        transform.DOMoveY(50, 3);
        Invoke("Despawn", 3f);
    }

    void Despawn()
    {
        Destroy(gameObject);
    }

    private void OnParticleCollision(GameObject other)
    {
        //Debug.Log("COLIDIU");
        if (other.tag == "Shoot" && spawned)
        {
            currentHealth = currentHealth - 10f;
        }
    }
}
