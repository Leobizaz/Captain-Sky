using System.Collections;
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
            //dialogocfg.GetComponent<RectTransform>().localPosition
            SpawnAnimation cfg = dialogocfg.GetComponent<SpawnAnimation>();
            cfg.lifeTime = lifeTime;
            cfg.icone.sprite = icone;
            cfg.name = nome;
            cfg.text = texto;
            audio.Play();

        }
    }
}
