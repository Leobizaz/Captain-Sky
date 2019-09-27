using UnityEngine;

public class StartShooting : MonoBehaviour
{
    public InimigoRaia[] raias;
    bool once;

    private void OnTriggerEnter(Collider other)
    {
        if(!once && other.CompareTag("Player"))
        {
            foreach(InimigoRaia raia in raias)
            {
                raia.passive = false;
            }
        }
    }
}
