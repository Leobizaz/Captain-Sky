using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class InimigoRaia : MonoBehaviour
{
    public float speed;
    public GameObject player;
    CinemachineDollyCart dollyCart;
    public GameObject mapSphere;

    public float maxHealth;
    public float currentHealth;
    public float fakeHealth;

    public AudioClip[] audios;
    public AudioClip explosionSFX;
    AudioSource audio;
    Rigidbody rb;

    bool died;


    public GameObject target;

    void Start()
    {
        dollyCart = GetComponent<CinemachineDollyCart>();
        player = GameObject.Find("Player");
        fakeHealth = maxHealth;
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        speed = player.GetComponent<CinemachineDollyCart>().m_Speed;
        dollyCart.m_Speed = speed;

        float sphereSize = 1000 / ((Vector3.Distance(player.transform.position, this.transform.position)) / 10);
        sphereSize = Mathf.Clamp(sphereSize, 25, 75);
        mapSphere.transform.DOScale(sphereSize, 1);

        if (!died)
        {
            if (fakeHealth <= 0) FakeDeath();

            if (currentHealth <= 0) Death();

        }
    }


    void FakeDeath()
    {

    }

    void Death()
    {

    }
}
