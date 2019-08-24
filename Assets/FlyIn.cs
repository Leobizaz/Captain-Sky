using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlyIn : MonoBehaviour
{
    public float startPos;
    public float speed;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.SetFloat("Vector1_F06D9476", startPos);
        rend.material.DOFloat(0f, "Vector1_F06D9476", speed).SetEase(Ease.InOutSine);
    }

    void Update()
    {

    }
}
