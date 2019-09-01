using UnityEngine;

public class TriggerRobozao : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke("FlyAway", 8f);
        }
    }

    void FlyAway()
    {
        RobozaoFlyAway.Begone = true;
    }
}
