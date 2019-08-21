using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissorPlayer : MonoBehaviour
{
    public GameObject mira;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(mira.transform);
    }
}
