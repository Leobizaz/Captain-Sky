using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSkin : MonoBehaviour
{
    public GameObject Skin1;
    public GameObject Skin2;
    public GameObject Skin3;
    public GameObject Skin4;
    public GameObject Skin5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Direita()
    {
        if(Skin5.activeSelf == true)
        {
            Skin5.SetActive(false);
            Skin1.SetActive(true);
        }
        else if(Skin1.activeSelf == true)
        {
            Skin2.SetActive(true);
            Skin1.SetActive(false);
        }
        else if (Skin2.activeSelf == true)
        {
            Skin3.SetActive(true);
            Skin2.SetActive(false);
        }
        else if (Skin3.activeSelf == true)
        {
            Skin4.SetActive(true);
            Skin3.SetActive(false);
        }
        else if (Skin4.activeSelf == true)
        {
            Skin5.SetActive(true);
            Skin4.SetActive(false);
        }
    }

    public void Esquerda()
    {
        if (Skin1.activeSelf == true)
        {
            Skin5.SetActive(true);
            Skin1.SetActive(false);
        }
        else if (Skin5.activeSelf == true)
        {
            Skin5.SetActive(false);
            Skin4.SetActive(true);
        }
        else if (Skin4.activeSelf == true)
        {
            Skin4.SetActive(false);
            Skin3.SetActive(true);
        }
        else if (Skin3.activeSelf == true)
        {
            Skin3.SetActive(false);
            Skin2.SetActive(true);
        }
        else if (Skin2.activeSelf == true)
        {
            Skin2.SetActive(false);
            Skin1.SetActive(true);
        }
    }
}
