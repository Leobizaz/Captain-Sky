using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobozaoImpactFX : MonoBehaviour
{
    public ParticleSystem impactFX_dir;
    public ParticleSystem impactFX_esq;

    public void ImpactFXDir()
    {
        impactFX_dir.Play();
    }

    public void ImpactFXEsq()
    {
        impactFX_esq.Play();
    }
}
