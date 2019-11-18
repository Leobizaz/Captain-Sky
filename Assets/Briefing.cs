using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Briefing : MonoBehaviour
{
    public Loading loading;
    public GameObject radio;
    public DialogoBriefing[] dialogos_ato1_2;
    public DialogoBriefing[] dialogos_ato2_3;
    Animator anim;
    bool once;
    int index = 0;

    private void Start()
    {
        Loading.levelIndex = 3;
        loading.gameObject.SetActive(true);
        anim = GetComponent<Animator>();
        if (Loading.levelIndex == 3)
        {
            Invoke("PlayDialogo_ato1_2", 4f);
        }
        else if(Loading.levelIndex == 4)
        {
            Invoke("PlayDialogo_ato2_3", 4f);
        }
        else
        {
            anim.gameObject.SetActive(false);
        }

    }

    public void Gone()
    {
        radio.SetActive(false);
        anim.Play("BriefingGone");
    }

    public void LoadNextLevel()
    {
        loading.operation.allowSceneActivation = true;
    }

    public void PlayDialogo_ato1_2()
    {
        radio.SetActive(true);
        if (index >= dialogos_ato1_2.Length && !once)
        {
            once = true;
            Invoke("Gone", 2f);
            return;
        }

        if (dialogos_ato1_2[index] != null)
        {
            dialogos_ato1_2[index].PlayDialogo();
            Invoke("PlayDialogo_ato1_2", dialogos_ato1_2[index].delay);
            index++;
        }
    }

    public void PlayDialogo_ato2_3()
    {
        radio.SetActive(true);
        if (index >= dialogos_ato2_3.Length && !once)
        {
            once = true;
            Invoke("Gone", 2f);
            return;
        }

        if (dialogos_ato2_3[index] != null)
        {
            dialogos_ato2_3[index].PlayDialogo();
            Invoke("PlayDialogo_ato2_3", dialogos_ato2_3[index].delay);
            index++;
        }
    }
}
