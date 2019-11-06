using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSequence : MonoBehaviour
{
    public TriggerExplosion[] explosions;
    int index = 0;
    bool once;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && !once)
        {
            once = true;
            PlayExplosions();

        }
    }

    public void PlayExplosions()
    {
        if (index >= explosions.Length)
        {
            return;
        }

        if (explosions[index] != null)
        {
            explosions[index].Explode();
            Invoke("PlayExplosions", explosions[index].delay);
            index++;
        }
    }


}
