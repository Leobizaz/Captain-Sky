using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSkin : MonoBehaviour
{
    public GameObject Skin1;
    public GameObject Skin2;
    public GameObject Skin3;
    public GameObject Skin4;
    public static int SkinSelect;
    public GameObject Skin5;
    public static bool tremOn;
    // Start is called before the first frame update
    void Start()
    {
        SkinSelect = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Direita()
    {
        if (tremOn)
        {
            if (Skin5.activeSelf == true)
            {
                Skin5.SetActive(false);
                Skin1.SetActive(true);
                SkinSelect = 1;
            }
        }
        else {
            if (Skin1.activeSelf == true)
            {
                Skin2.SetActive(true);
                Skin1.SetActive(false);
                SkinSelect = 2;
            }
        }
        if (Skin2.activeSelf == true)
        {
            Skin3.SetActive(true);
            Skin2.SetActive(false);
            SkinSelect = 3;
        }
        else if (Skin3.activeSelf == true)
        {
            Skin4.SetActive(true);
            Skin3.SetActive(false);
            SkinSelect = 4;
        }
        if (tremOn)
        {
            if (Skin4.activeSelf == true)
            {
                Skin5.SetActive(true);
                Skin4.SetActive(false);
                SkinSelect = 5;
            }
        }
        else {
            if (Skin4.activeSelf == true)
            {
                Skin1.SetActive(true);
                Skin4.SetActive(false);
                SkinSelect = 1;
            }
        }
    }

    public void Esquerda()
    {
        if (tremOn)
        {
            if (Skin1.activeSelf == true)
            {
                Skin5.SetActive(true);
                Skin1.SetActive(false);
                SkinSelect = 5;
            }
        }
        else
        {
            if (Skin1.activeSelf == true)
            {
                Skin4.SetActive(true);
                Skin1.SetActive(false);
                SkinSelect = 4;
            }
        }
        if (Skin4.activeSelf == true)
        {
            Skin4.SetActive(false);
            Skin3.SetActive(true);
            SkinSelect = 3;
        }
        else if (Skin3.activeSelf == true)
        {
            Skin3.SetActive(false);
            Skin2.SetActive(true);
            SkinSelect = 2;
        }
        
        else if (Skin2.activeSelf == true)
        {
            Skin2.SetActive(false);
            Skin1.SetActive(true);
            SkinSelect = 1;
        }
        
    }
}
