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

    public GameObject explosion;

    void Start()
    {
        tempPosition = transform.position;
        fwdSpeed = Random.Range(-1f, 1f);
        verticalSpeed = Random.Range(1f, 3f);
        amplitude = Random.Range(0.1f, 0.4f);
    }

    void FixedUpdate()
    {
        tempPosition.x += 0.5f * fwdSpeed;
        tempPosition.y += Mathf.Sin(Time.fixedTime * Mathf.PI + verticalSpeed) * amplitude;
        transform.position = tempPosition;
        Object.Destroy(this.gameObject, lifeTime);
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("COLIDIU");
        if (other.tag == "Shoot")
        {
            Instantiate(explosion, tempPosition, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
