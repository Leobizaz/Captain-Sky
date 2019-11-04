using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaHexagonal : MonoBehaviour
{
    public Animator anim;
    bool once;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player" && !once)
        {
            once = true;
            CloseDoor();
        }
    }

    void CloseDoor()
    {
        anim.Play("PortaClose");
    }

    public void OpenDoor()
    {
        anim.Play("PortaOPen");
    }
}
