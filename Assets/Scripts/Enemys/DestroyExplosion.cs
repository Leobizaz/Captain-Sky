using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyExplosion : MonoBehaviour
{


    // Update is called once per frame
    void FixedUpdate()
    {
        Object.Destroy(this.gameObject, 2.0f);
    }
}
