using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mina : MonoBehaviour
{
    public Animator anim;
    public GameObject explosionFX;
    public ParticleSystem signalFX;
    public GameObject mesh;
    DoCameraShake cameraShake;
    public bool unshootable;
    bool once;
    bool once2;

    void Start()
    {
        cameraShake = GameObject.Find("Game Manager").GetComponent<DoCameraShake>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player" && !once){
            Arm();
            once = true;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if((other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy") && !once2)
        {
            once2 = true;
            Explode();
        }
    }

    private void OnParticleCollision(GameObject other) {
        if(other.tag == "Shoot" && !once2 && !unshootable)
        {
            once2 = true;
            Explode();
        }    
    }

    public void Arm()
    {
        signalFX.Play();
        anim.Play("Mina Aquática");
        Invoke("Despawn", 20f);
    }

    public void Explode()
    {
        Destroy(mesh);
        cameraShake.shakeElapsedTime = cameraShake.ShakeDuration;
        Vector3 newPos = this.transform.position;
        newPos.y = this.transform.position.y + 5;
        Debug.Log("Cu");
        GameObject istance = Instantiate(explosionFX, newPos, Quaternion.identity);
        istance.SetActive(true);
        Destroy(gameObject, cameraShake.ShakeDuration);
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }

}
