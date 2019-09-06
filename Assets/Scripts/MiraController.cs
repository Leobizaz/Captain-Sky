using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MiraController : MonoBehaviour
{

    Vector3 vectorinput;

    Sequence mySequence;

    void Start()
    {
        mySequence = DOTween.Sequence();
    }

    void Update() { 

        float x2 = Input.GetAxis("HorizontalAxis");
        float y2 = Input.GetAxis("VerticalAxis");


            transform.localPosition += new Vector3(x2, 0, 0) * 233 * Time.deltaTime;
            transform.localPosition += new Vector3(0, y2, 0) * 291 * Time.deltaTime;
            transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, -34f, 34f), Mathf.Clamp(transform.localPosition.y, -30f, 30f), 0);
 
    }
}
