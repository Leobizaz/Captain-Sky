using UnityEngine;

public class TriggerRobozao : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            RobozaoFlyAway.Prepare = true;
            Invoke("FlyAway", 8f);
        }
    }

    void FlyAway()
    {
        RobozaoFlyAway.Begone = true;
    }
}
