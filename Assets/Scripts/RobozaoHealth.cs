using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobozaoHealth : MonoBehaviour
{
    public bool bracoESQ_destroy;
    public bool bracoDIR_destroy;
    public bool cabessa_destroy;

    public RobozaoBrasso braço1;
    public RobozaoBrasso braço2;
    public RobozaoCabess cabeça;

    int hit;

    bool cooldown;

    public bool dead;
    void Update()
    {
        if(bracoESQ_destroy && bracoDIR_destroy && cabessa_destroy && !dead)
        {
            dead = true;
            ScoreSystem.enemysKill++;
            ScoreSystem.currentScore += 3000f;
            GameObject.Find("Game Manager").GetComponent<ScoreSystem>().UpdateScore();
            Destroy(gameObject, 10);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "LaserPlayer" && !cooldown)
        {
            LaserHit();
        }
    }

    public void LaserHit()
    {
        if (hit > 2)
            return;

        cooldown = true;
        if(hit == 0)
        {
            braço1.GetKilled();
        }
        if(hit == 1)
        {
            braço2.GetKilled();
        }
        if(hit == 2)
        {
            cabeça.GetKilled();
        }
        hit++;
        Invoke("ResetCooldown", 1f);
    }

    public void ResetCooldown()
    {
        cooldown = false;
    }

}
