using UnityEngine;
public class ComunicadorMarcus : MonoBehaviour
{
    SpriteRenderer rend;
    ParticleSystem particle;

    public static bool active;

    private void Start()
    {
        
        rend = GetComponent<SpriteRenderer>();
        rend.enabled = false;
        particle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (active)
        {
            if(!particle.isPlaying)
                particle.Play();
        }
        else
        {
            particle.Stop();
        }
    }
}
