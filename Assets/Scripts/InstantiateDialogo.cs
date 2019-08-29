﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantiateDialogo : MonoBehaviour
{
    public GameObject dialogo;
    public Canvas canvas;
    public float lifeTime;
    public Sprite icone;
    public string nome;
    public string texto;
    AudioSource audio;

    public static bool dialogoPlaying;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject dialogocfg = Instantiate(dialogo) as GameObject;
            dialogocfg.transform.SetParent(canvas.transform, false);
            if (dialogoPlaying)
            {
                RectTransform rt = dialogocfg.GetComponent<RectTransform>();
                rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 458f, 458f);
                rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 352f, 352f);

            }
            SpawnAnimation cfg = dialogocfg.GetComponent<SpawnAnimation>();
            cfg.lifeTime = lifeTime;
            cfg.icone.sprite = icone;
            cfg.name = nome;
            cfg.text = texto;
            audio.Play();

        }
    }
}