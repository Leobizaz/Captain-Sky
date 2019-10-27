using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSkin : MonoBehaviour
{
    public GameObject Skin1;
    public GameObject Skin2;
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
        if(Skin2.activeSelf == true)
        {
            Skin2.SetActive(false);
            Skin1.SetActive(true);
        }
        else
        {
            Skin2.SetActive(true);
            Skin1.SetActive(false);
        }
    }

    public void Esquerda()
    {
        if (Skin1.activeSelf == true)
        {
            Skin2.SetActive(true);
            Skin1.SetActive(false);
        }
        else
        {
            Skin2.SetActive(false);
            Skin1.SetActive(true);
        }
    }
}
