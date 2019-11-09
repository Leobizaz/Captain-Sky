using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobozaoHealth : MonoBehaviour
{
    public bool bracoESQ_destroy;
    public bool bracoDIR_destroy;
    public bool cabessa_destroy;

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
}
