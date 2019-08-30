using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAtPlayer : MonoBehaviour
{

    GameObject target;

    void Awake()
    {
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.transform);
    }
}
