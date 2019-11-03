using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Ato3_Objetivo2 : MonoBehaviour
{
    public static int geradores_restantes = 4;
    public ParticleSystem explosao;
    public GameObject objetivoHUD;
    Animator objetivoAnim;
    public Animator geradorAnim;
    public Text TXT_objetivo;
    public EnterDolly dollyEscape;
    public CinemachinePathBase track;
    public DialogoSequence sequenciaDialogo;
    public GameObject lasers1;
    public GameObject lasers2;
    public DoCameraShake cameraShake;
    public InstantiateDialogo dialogo;

    bool once1;
    bool once2;
    bool once3;

    void OnEnable()
    {
        dollyEscape.gameObject.SetActive(false);
        objetivoHUD.SetActive(true);
        objetivoAnim = objetivoHUD.GetComponent<Animator>();
        objetivoAnim.Play("objetivoIn");
    }

    private void Update()
    {
        if (geradores_restantes < 0) geradores_restantes = 0;
        TXT_objetivo.text = geradores_restantes.ToString() + " estabilizadores restantes";

        if(geradores_restantes == 2 && !once1)
        {
            dialogo.PlayDialogo();
            once1 = true;
            lasers1.SetActive(true);
        }
        if(geradores_restantes == 1 && !once2)
        {
            once2 = true;
            lasers2.SetActive(true);
        }
        if(geradores_restantes == 0 && !once3)
        {
            dollyEscape.gameObject.SetActive(true);
            once3 = true;
            dollyEscape.dollyTrack = track;
            Invoke("PlayDialogo", 2f);
        }

    }

    void PlayDialogo()
    {
        objetivoHUD.SetActive(false);
        sequenciaDialogo.PlayDialogo();
        Invoke("Explosao", 3f);
    }

    void Explosao()
    {
        explosao.Play();
        cameraShake.shakeElapsedTime = 1.0f;
        geradorAnim.Play("SalaGerador_EXplode");

    }


}
