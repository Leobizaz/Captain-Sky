using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdForward : MonoBehaviour
{
    public float fwdSpeed;
    public float lifeTime;
    float verticalSpeed;
    float amplitude;

    private Vector3 tempPosition;

    void Start()
    {
        tempPosition = transform.localPosition;
        fwdSpeed = Random.Range(-1f, 1f);
        verticalSpeed = Random.Range(0.01f, 0.1f);
        amplitude = Random.Range(-0.01f, 0.01f);
    }

    void FixedUpdate()
    {
        tempPosition.x += 0.005f * fwdSpeed;
        tempPosition.y += Mathf.Sin(Time.fixedTime * Mathf.PI + verticalSpeed) * amplitude;
        transform.localPosition = tempPosition;
        //Object.Destroy(this.gameObject, lifeTime);
    }
}
