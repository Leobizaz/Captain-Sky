using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Breakable : MonoBehaviour
{
    public Color Red = Color.red;
    public GameObject explosion;

    void Start()
    {

    }
    private void OnParticleCollision(GameObject other)
    {
        
        if (other.tag == "Shoot")
        {

            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
