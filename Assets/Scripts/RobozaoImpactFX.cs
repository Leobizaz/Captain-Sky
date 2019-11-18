using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobozaoImpactFX : MonoBehaviour
{
    public ParticleSystem impactFX_dir;
    public ParticleSystem impactFX_esq;
    public ParticleSystem impactzao;
    public ParticleSystem mediumImpact;
    AudioSource audio;
    public AudioClip[] sounds;
    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void PassoSFX()
    {
        audio.PlayOneShot(sounds[0]);
    }

    public void FallSFX()
    {
        audio.PlayOneShot(sounds[1]);
    }



    public void ImpactFXDir()
    {
        impactFX_dir.Play();
        //impactFX_dir.gameObject.GetComponent<AudioSource>().Play();
    }

    public void ImpactFXEsq()
    {
        impactFX_esq.Play();
        //impactFX_esq.gameObject.GetComponent<AudioSource>().Play();
    }

    public void Impactzao()
    {
        impactzao.Play();
        impactzao.gameObject.GetComponent<AudioSource>().Play();
    }

    public void MediumImpact()
    {
        mediumImpact.Play();
        mediumImpact.gameObject.GetComponent<AudioSource>().Play();
    }
}
