using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ato3_Objetivo1 : MonoBehaviour
{
    public GameObject objetivoHUD;
    public GameObject dialogoEntrada;
    Animator objetivoAnim;
    public Text TXT_objetivo;

    bool once1;
    bool once2;
    bool once3;


    public DialogoSequence sequenciaDialogo;
    public InstantiateDialogo dialogo1;
    public InstantiateDialogo dialogo2;

    public static int torres_restantes = 3;

    void Start()
    {
        objetivoHUD.SetActive(true);
        objetivoAnim = objetivoHUD.GetComponent<Animator>();
    }

    void Update()
    {
        if (torres_restantes < 0) torres_restantes = 0;
        TXT_objetivo.text = torres_restantes.ToString() + " torres restantes";

        if(torres_restantes == 2 && !once1 && !DialogoSequence.isPlayingSequence)
        {
            once1 = true;
            dialogo1.PlayDialogo();
        }
        if(torres_restantes == 1 && !once2 && !DialogoSequence.isPlayingSequence)
        {
            once2 = true;
            dialogo2.PlayDialogo();
        }
        if(torres_restantes == 0 && !once3 && !DialogoSequence.isPlayingSequence)
        {
            once3 = true;
            Invoke("PlayDialogo", 3f);
            objetivoAnim.Play("objetivoGone");
            dialogoEntrada.SetActive(false);
        }

    }

    void PlayDialogo()
    {
        objetivoHUD.SetActive(false);
        sequenciaDialogo.PlayDialogo();
    }
}
