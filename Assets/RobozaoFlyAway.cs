using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RobozaoFlyAway : MonoBehaviour
{
    public static bool Begone;

    private void Update()
    {
        if (Begone)
        {
            transform.DOLocalMoveY(1000, 50f);
        }
    }
}
