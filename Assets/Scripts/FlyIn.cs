using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlyIn : MonoBehaviour
{
    public float startYPos;
    public float speedModifier;
    public Renderer rend;

    void Start()
    {
        float approachSpeed = Random.Range(0.1f, 1f) + speedModifier;

        //rend = GetComponent<Renderer>();
        rend.material.SetFloat("Vector1_9B552C0F", 1000f);
        rend.material.SetFloat("Vector1_F06D9476", startYPos);
        rend.material.DOFloat(0f, "Vector1_9B552C0F", approachSpeed);
        rend.material.DOFloat(0f, "Vector1_F06D9476", approachSpeed).SetEase(Ease.InOutSine);
    }

    void Update()
    {

    }
}
