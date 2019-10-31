using UnityEngine;

public class ExitCave : MonoBehaviour
{
    public GameObject snowFX;
    public GameObject snowflakes;
    public GameObject snowBeam;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            snowFX.SetActive(true);
            snowflakes.SetActive(false);
            snowBeam.SetActive(false);
        }
    }

}
