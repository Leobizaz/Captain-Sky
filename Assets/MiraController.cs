using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiraController : MonoBehaviour
{

    Vector3 vectorinput;

    void Start()
    {
        
    }

    void Update()
    {
        vectorinput = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);

        transform.localPosition += vectorinput * 800 * Time.deltaTime;
        transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, -84f, 84f), Mathf.Clamp(transform.localPosition.y, -56f, 56f), 0);

    }
}
