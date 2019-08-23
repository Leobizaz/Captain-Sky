using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MiraController : MonoBehaviour
{

    Vector3 vectorinput;
    public GameObject player;
    Sequence mySequence;

    void Start()
    {
        mySequence = DOTween.Sequence();
    }

    void Update()
    {
        //vectorinput = new Vector3(Input.GetAxis("Horizontal"), -Input.GetAxis("Vertical"), 0);
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        float x2 = Input.GetAxis("HorizontalAxis");
        float y2 = Input.GetAxis("VerticalAxis");

        if(x != 0 || y != 0 && x2 == 0 && y2 == 0)
        {
            transform.localPosition += new Vector3(x, 0, 0) * 333 * Time.deltaTime;
            transform.localPosition += new Vector3(0, -y, 0) * 391 * Time.deltaTime;
            transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, -84f, 84f), Mathf.Clamp(transform.localPosition.y, -90f, 90f), 0);

            transform.localPosition += new Vector3(x2, 0, 0) * 233 * Time.deltaTime;
            transform.localPosition += new Vector3(0, y2, 0) * 291 * Time.deltaTime;
        }
        else
        {
            transform.localPosition += new Vector3(x2, 0, 0) * 233 * Time.deltaTime;
            transform.localPosition += new Vector3(0, y2, 0) * 291 * Time.deltaTime;
            transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, -34f, 34f), Mathf.Clamp(transform.localPosition.y, -30f, 30f), 0);
        }
        if (Input.GetButtonDown("Jump"))
        {
            transform.DOLocalMove(new Vector3(player.transform.localPosition.x, player.transform.localPosition.y, 0), 1f).SetEase(Ease.OutSine);
        }
    }
}
