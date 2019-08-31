using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class SpawnAnimation : MonoBehaviour
{
    public TextMeshProUGUI nome;
    public TextMeshProUGUI mensagem;
    public Image icone;
    public float lifeTime;
    public string text;
    public string name;
    Animator anim;

    void Start()
    {
        InstantiateDialogo.dialogoPlaying = true;
        anim = GetComponent<Animator>();
        Invoke("Die", lifeTime);
        nome.text = name;
        mensagem.text = text;

        if (name == "Jack") ComunicadorJack.active = true;
        if (name == "Marcus") ComunicadorMarcus.active = true;
        if (name == "Adam") ComunicadorAdam.active = true;
        if (name == "Charlie") ComunicadorCharlie.active = true;

    }

    void Die()
    {
        if (name == "Jack") ComunicadorJack.active = false;
        if (name == "Marcus") ComunicadorMarcus.active = false;
        if (name == "Adam") ComunicadorAdam.active = false;
        if (name == "Charlie") ComunicadorCharlie.active = false;

        anim.Play("PopOut");
        Invoke("RIP", 1f);
        InstantiateDialogo.dialogoPlaying = false;
    }

    void RIP()
    {
        Destroy(gameObject);
    }

    
}
