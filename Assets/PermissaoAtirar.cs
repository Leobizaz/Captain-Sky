using UnityEngine;

public class PermissaoAtirar : MonoBehaviour
{
    public bool podeAtirar;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Passou por pode atirar");
            other.GetComponent<PlayerMovement>().podeAtirar = podeAtirar;
        }
    }
}
