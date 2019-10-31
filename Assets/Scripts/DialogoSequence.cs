using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogoSequence : MonoBehaviour
{
    public static bool isPlayingSequence;
    public InstantiateDialogo[] dialogos;
    bool once;
    int index = 0;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && !once && !isPlayingSequence)
        {
            isPlayingSequence = true;
            once = true;
            PlayDialogo();

        }
    }

    public void PlayDialogo()
    {
        if(index >= dialogos.Length)
        {
            isPlayingSequence = false;
            return;
        }

        if (dialogos[index] != null)
        {
            dialogos[index].PlayDialogo();
            Invoke("PlayDialogo", dialogos[index].delay);
            index++;
        }
    }
}
