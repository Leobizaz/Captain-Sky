using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaFollow : MonoBehaviour
{
    public GameObject objectToFollow;
    float offsetZ;
    private void Start()
    {
        offsetZ = objectToFollow.transform.position.z - transform.position.z;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, objectToFollow.transform.position.z - offsetZ);
    }
}
