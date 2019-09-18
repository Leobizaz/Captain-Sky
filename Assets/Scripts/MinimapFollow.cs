using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapFollow : MonoBehaviour
{
    public Transform player;

        void LateUpdate()
    {
        Vector3 Pos = player.position;
        Pos.y = transform.position.y;
        transform.position = Pos;
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
