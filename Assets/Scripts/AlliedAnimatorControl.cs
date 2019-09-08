using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlliedAnimatorControl : MonoBehaviour
{
    private Animator anim;
    bool once;
    public int idle_animation;

    private void Start()
    {
        anim = GetComponent<Animator>();
        PlayIdle();


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AllyTrigger") && !once)
        {
            //MoveDialogo.move = false;
            once = true;
            Dive();
        }
    }

    public void PlayIdle()
    {
        switch (idle_animation)
        {
            case 0:
                anim.Play("AllyIdle");
                break;
            case 1:
                anim.Play("AllyIdle2");
                break;
            case 2:
                anim.Play("AllyIdle3");
                break;
            default:
                idle_animation = Random.Range(0, 2);
                PlayIdle();
                break;

        }
    }

    public void Dive()
    {
        gameObject.transform.parent = null;
        anim.Play("AllyDive");
    }

    public void Vanish()
    {
        Destroy(gameObject);
    }
}
