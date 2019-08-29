using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float speed;
    void Update()
    {
        transform.Rotate(new Vector3(0, transform.rotation.y + speed, 0));
    }
}
