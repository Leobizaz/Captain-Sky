using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvesFundo : MonoBehaviour
{
    public float vel;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        pos = new Vector3(pos.x + vel * Time.deltaTime, pos.y);
        transform.position = pos;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        if (transform.position.x < min.x)
        {
            transform.position = new Vector3(Random.Range(min.x, max.x), max.y);
        }

    }
}
