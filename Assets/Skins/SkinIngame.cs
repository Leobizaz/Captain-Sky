using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinIngame : MonoBehaviour
{
    public GameObject Skin1;
    public GameObject Skin2;
    public GameObject Skin3;
    public GameObject Skin4;
    public GameObject Skin5;
    // Start is called before the first frame update
    void Start()
    {
        if(SelectSkin.SkinSelect == 1)
        {
            Skin1.SetActive(true);
            Skin2.SetActive(false);
            Skin3.SetActive(false);
            Skin4.SetActive(false);
            Skin5.SetActive(false);
        }
        if (SelectSkin.SkinSelect == 2)
        {
            Skin1.SetActive(false);
            Skin2.SetActive(true);
            Skin3.SetActive(false);
            Skin4.SetActive(false);
            Skin5.SetActive(false);
        }
        if (SelectSkin.SkinSelect == 3)
        {
            Skin1.SetActive(false);
            Skin2.SetActive(false);
            Skin3.SetActive(true);
            Skin4.SetActive(false);
            Skin5.SetActive(false);
        }
        if (SelectSkin.SkinSelect == 4)
        {
            Skin1.SetActive(false);
            Skin2.SetActive(false);
            Skin3.SetActive(false);
            Skin4.SetActive(true);
            Skin5.SetActive(false);
        }
        if (SelectSkin.SkinSelect == 5)
        {
            Skin1.SetActive(false);
            Skin2.SetActive(false);
            Skin3.SetActive(false);
            Skin4.SetActive(false);
            Skin5.SetActive(true);
        }
    }


}
