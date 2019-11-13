using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public GameObject destroços;
    public GameObject intacto;
    public GameObject indicador;
    public GameObject particle;
    bool once;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if(currentHealth <= 0 && !once)
        {
            once= true;
            Ded();
        }
    }


    public void Ded()
    {
        Ato3_Objetivo1.torres_restantes -= 1;
        Destroy(intacto);
        destroços.SetActive(true);
        particle.SetActive(false);
        indicador.SetActive(false);
    }
    private void OnParticleCollision(GameObject other)
    {
        //Debug.Log("COLIDIU");
        if (other.tag == "Shoot")
        {
            currentHealth = currentHealth - 10;
        }
    }
}
