using UnityEngine;

public class TriggerRobozao : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RobozaoFlyAway.Begone = true;
        }
    }
}
