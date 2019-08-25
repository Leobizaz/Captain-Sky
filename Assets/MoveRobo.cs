using UnityEngine;

public class MoveRobo : MonoBehaviour
{
    public float speedX;
    public float speedY;
    public float speedZ;

    void Update()
    {
        transform.Translate(new Vector3(speedX, speedY, speedZ));
    }
}
