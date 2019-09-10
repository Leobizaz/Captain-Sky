using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RobozaoFlyAway : MonoBehaviour
{
    public static bool Begone;
    public static bool Prepare;
    public GameObject smokeTrail;
    public GameObject sparks;
    public Animator meshAnim;

    private void Update()
    {
        if (Prepare && !Begone)
        {
            meshAnim.Play("E2_Flight");
            sparks.SetActive(true);
        }


        if (Begone)
        {
            sparks.SetActive(false);
            smokeTrail.SetActive(true);
            transform.DOLocalMoveY(1000, 50f);
        }
    }
}
