using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogoSequence : MonoBehaviour
{
    public InstantiateDialogo[] dialogos;
    bool once;
    int index = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !once)
        {
            once = true;
            PlayDialogo();

        }
    }

    void PlayDialogo()
    {
        if (dialogos[index] != null)
        {
            dialogos[index].PlayDialogo();
            Invoke("PlayDialogo", dialogos[index].delay);
            index++;
        }
    }
}
