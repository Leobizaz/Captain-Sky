using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalMission : MonoBehaviour
{
    public bool playerIn;
    public GameObject image;
    public GameObject beam;

    public AudioClip signalIn;
    public AudioClip signalOff;

    public InstantiateDialogo wilburneDica;

    public AudioSource source;

    bool once;
    bool once2;
    bool oncememo;

    private void Update()
    {
        if (playerIn)
        {
            beam.SetActive(true);
            image.SetActive(true);
            if (!once)
            {
                source.PlayOneShot(signalIn);
                image.GetComponent<Animator>().Play("signalin");
                once = true;
            }
            WaveSystem.missaoRadio = WaveSystem.missaoRadio + Time.deltaTime;
        }
        else
        {
            once = false;
            beam.SetActive(false);
        }
    }

    public void Deactivate()
    {
        beam.SetActive(false);
        image.GetComponent<Animator>().Play("signaloof");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            once2 = false;
            playerIn = true;

            if (!oncememo)
            {
                oncememo = true;
                wilburneDica.PlayDialogo();
                StartCoroutine(repeatWilburneDica());
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            playerIn = false;
            if (!once2)
            {
                once2 = true;
                source.PlayOneShot(signalOff);
                image.GetComponent<Animator>().Play("signaloof");
            }

        }
    }

    IEnumerator repeatWilburneDica()
    {
        while (WaveSystem.missaoRadio < 29f)
        {
            yield return new WaitForSeconds(80f);
            if(WaveSystem.missaoRadio < 29)
                wilburneDica.PlayDialogo();
        }
    }


}
