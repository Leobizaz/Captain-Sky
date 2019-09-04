using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBird : MonoBehaviour
{
    float horizontalSpeed;
    float verticalSpeed;
    float amplitude;

    private Vector3 tempPosition;

    public GameObject explosion;

    void Start()
    {
        tempPosition = transform.position;
         horizontalSpeed = Random.Range(-1f, 1f) ;
         verticalSpeed = Random.Range(1f, 3f);
         amplitude = Random.Range(0.1f, 0.4f);
    }

    void FixedUpdate()
    {
        tempPosition.x += 0.5f * horizontalSpeed;
        tempPosition.y += Mathf.Sin(Time.fixedTime * Mathf.PI + verticalSpeed) * amplitude;
        transform.position = tempPosition;
        Object.Destroy(this.gameObject, 10.0f);
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("COLIDIU");
        if (other.tag == "Shoot")
        {
            Instantiate(explosion,tempPosition, transform.rotation);
            ScoreSystem.enemysKill += 1;
            Destroy(this.gameObject);
        }
    }
}
