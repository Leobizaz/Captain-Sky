using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobozaoImpactFX : MonoBehaviour
{
    public ParticleSystem impactFX_dir;
    public ParticleSystem impactFX_esq;
    public ParticleSystem impactzao;
    public ParticleSystem mediumImpact;

    public void ImpactFXDir()
    {
        impactFX_dir.Play();
    }

    public void ImpactFXEsq()
    {
        impactFX_esq.Play();
    }

    public void Impactzao()
    {
        impactzao.Play();
    }

    public void MediumImpact()
    {
        mediumImpact.Play();
    }
}
