using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public GameObject objectToFollow;

    void LateUpdate()
    {
        this.transform.position = objectToFollow.transform.position;
    }
}
