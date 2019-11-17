using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioBriefing : MonoBehaviour
{
    public static bool alguemfalando;

    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (alguemfalando && !anim.GetCurrentAnimatorStateInfo(0).IsName("radiobriefing"))
        {
            anim.Play("radiobriefing");
        }
        if(!alguemfalando)
        {
            anim.Play("radiostill");
        }
    }
}
