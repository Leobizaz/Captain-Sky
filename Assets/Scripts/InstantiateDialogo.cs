using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantiateDialogo : MonoBehaviour
{
    public GameObject dialogo;
    public Canvas canvas;
    public GameObject HUD;
    public float lifeTime;
    public Sprite icone;
    public string nome;
    public string texto;
    public bool italic;
    public bool ativaNoFinal;
    public GameObject objeto;
    public float delay;
    bool tocou;
    public bool autoplay;
    AudioSource audio;
    public GameObject Charlie;

    public static bool dialogoPlaying;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        if (autoplay) PlayDialogo();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !tocou &&!autoplay)
        {
            PlayDialogo();
            if(Charlie != null)
            {
                Invoke("Passa",5.8f);
            }
        }
    }

    public void PlayDialogo()
    {
        tocou = true;
        GameObject dialogocfg = Instantiate(dialogo) as GameObject;
        dialogocfg.transform.SetParent(canvas.transform, false);
        dialogocfg.name = "Dialogo";
        //int index = dialogocfg.transform.GetSiblingIndex();
        dialogocfg.transform.SetAsFirstSibling();
        //dialogocfg.transform.SetSiblingIndex(8);

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
        cfg.italic = italic;
        audio.Play();

        if (ativaNoFinal)
            Invoke("Ativa", delay);


    }

    public void Ativa()
    {
        objeto.SetActive(true);
    }

    public void MoveInHierarchy(int delta)
    {
        int index = transform.GetSiblingIndex();
        transform.SetSiblingIndex(index + delta);
    }

    public void Passa()
    {
        Charlie.SetActive(true);
    }
}
