using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCheckpoint : MonoBehaviour
{
    public Animator popUpAnim;
    public string phase;
    bool once;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !once)
        {
            once = true;
            Checkpoint();


        }   
    }

    public void Checkpoint()
    {
        popUpAnim.Play("CheckpointAnim");
        CheckpointSystem.storedKills = ScoreSystem.enemysKill;
        CheckpointSystem.storedTime = ScoreSystem.time;
        CheckpointSystem.storedScore = ScoreSystem.currentScore;
        CheckpointSystem.STAGEPHASE = phase;
    }
}
