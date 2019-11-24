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

    void Start()
    {
       // Invoke("Esq", 2f);
    }

    private void Update()
    {
        if (Prepare && !Begone)
        {
            meshAnim.SetInteger("State", 3);
            meshAnim.Play("TPose_voar");
            sparks.SetActive(true);
        }


        if (Begone)
        {
            sparks.SetActive(false);
            smokeTrail.SetActive(true);
            transform.DOLocalMoveY(1000, 50f);
        }
    }

    public void Esq()
    {
        if (meshAnim.GetInteger("State") != 3)
        {
            meshAnim.SetInteger("State", 1);
            Invoke("Dir", 4f);
        }
    }

    public void Dir()
    {
        if (meshAnim.GetInteger("State") != 3)
        {
            meshAnim.SetInteger("State", 0);
            Invoke("Esq", 4f);
        }
    }
}
