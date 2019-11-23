using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowAimAtPlayer : MonoBehaviour
{
    public GameObject target;
    public float speed;

    void Update()
    {
        Quaternion neededRotation = Quaternion.LookRotation(target.transform.position - this.transform.position);
        Quaternion.Slerp(transform.localRotation, neededRotation, speed * Time.deltaTime);
    }
}
