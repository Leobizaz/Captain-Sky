using UnityEngine;

public class ActivateDica : MonoBehaviour
{
    public GameObject dicaOBJ;
    public string texto;
    public Sprite img;
    public bool autoPlay;

    private void OnEnable()
    {
        if (autoPlay)
        {
            Activate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Activate();
        }
    }

    public void Activate()
    {
        dicaOBJ.GetComponent<DicaAnimation>().texto = texto;
        dicaOBJ.GetComponent<DicaAnimation>().img = img;
        dicaOBJ.SetActive(true);
        Invoke("Deactivate", 20f);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
