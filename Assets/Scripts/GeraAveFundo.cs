using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeraAveFundo : MonoBehaviour
{
    public GameObject Ave;
    public int Max;



    // Use this for initialization
    void Start()
    {


        for (int i = 0; i < Max; i++)
        {

            GameObject star = (GameObject)Instantiate(Ave);
            star.GetComponent<AvesFundo>().vel = -(1f * Random.value + 0.5f);
            star.transform.parent = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
