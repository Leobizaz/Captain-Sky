using UnityEngine;

public class ActivateDica : MonoBehaviour
{
    public GameObject dicaOBJ;
    public string texto;
    public Sprite img;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dicaOBJ.GetComponent<DicaAnimation>().texto = texto;
            dicaOBJ.GetComponent<DicaAnimation>().img = img;
            dicaOBJ.SetActive(true);
        }
    }

}
