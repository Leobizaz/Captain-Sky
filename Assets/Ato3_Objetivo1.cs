using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ato3_Objetivo1 : MonoBehaviour
{
    public GameObject objetivoHUD;
    Animator objetivoAnim;
    public Text TXT_objetivo;

    public static int torres_restantes = 3;

    void Start()
    {
        objetivoHUD.SetActive(true);
        objetivoAnim = objetivoHUD.GetComponent<Animator>();
    }

    void Update()
    {
        TXT_objetivo.text = torres_restantes.ToString() + " torres restantes";
    }
}
