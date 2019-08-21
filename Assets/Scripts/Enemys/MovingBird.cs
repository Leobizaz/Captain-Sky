using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBird : MonoBehaviour
{
    float horizontalSpeed;
    float verticalSpeed;
    float amplitude;

    private Vector3 tempPosition;


    void Start()
    {
        tempPosition = transform.position;
         horizontalSpeed = Random.Range(0.1f, 0.3f);
         verticalSpeed = Random.Range(1f, 3f);
         amplitude = Random.Range(2f, 4f);
    }

    void FixedUpdate()
    {
        tempPosition.x += horizontalSpeed;
        tempPosition.y = Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed) * amplitude;
        transform.position = tempPosition;
        Object.Destroy(this.gameObject, 10.0f);
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("COLIDIU");
        if (other.tag == "Shoot")
        {
            Destroy(this.gameObject);
        }
    }
}
