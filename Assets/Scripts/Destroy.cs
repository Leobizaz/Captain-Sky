using UnityEngine;

public class Destroy : MonoBehaviour
{
    public void Ded()
    {

        Destroy(gameObject);
    }
    private void OnParticleCollision(GameObject other)
    {
        //Debug.Log("COLIDIU");
        if (other.tag == "Shoot")
        {
            if (this.gameObject.tag == "Torre")
            {
                Destroy(this.gameObject);
                Ato3_Objetivo1.torres_restantes -= 1;
            }

        }
    }
}
