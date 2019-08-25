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
    }

    void Die()
    {
        anim.Play("PopOut");
        Invoke("RIP", 1f);
        InstantiateDialogo.dialogoPlaying = false;
    }

    void RIP()
    {
        Destroy(gameObject);
    }

    
}
