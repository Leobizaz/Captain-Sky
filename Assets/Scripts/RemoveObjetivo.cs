using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveObjetivo : MonoBehaviour
{
    public GameObject objetivoHUD;
    bool once;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !once)
        {
            once = true;
            objetivoHUD.GetComponent<Animator>().Play("objetivoGone");
            Destroy(objetivoHUD, 4f);
        }
    }

}
