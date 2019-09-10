using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeblackIntroAto1 : MonoBehaviour
{
    public GameObject gameplay;
    public GameObject timeline;
    public GameObject musica;

    public GameObject canvas2;

    public void LigarTUDO()
    {
        Destroy(canvas2);
        gameplay.SetActive(true);
        timeline.SetActive(true);
        //musica.SetActive(true);
        Invoke("Dead", 2.9f);
    }

    public void Dead()
    {
        Destroy(gameObject);
    }
}
