using UnityEngine;

public class ActivateHUD : MonoBehaviour
{
    public Animator hudAnimator;
    public PlayerMovement playerMovement;

    private void Start()
    {
        hudAnimator.speed = 0f;


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            hudAnimator.speed = 1f;
            playerMovement.canFirstPerson = true;
        }
    }
}
