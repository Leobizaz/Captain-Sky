using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleSelect : MonoBehaviour
{
    public GameObject L1;
    public GameObject L2;
    public GameObject L3;
    public GameObject L4;

    public GameObject P1;
    public GameObject P2;
    public GameObject P3;
    public GameObject P4;

    public static bool W1 = false;
    public static bool W2 = false;
    public static bool W3 = false;



    // Start is called before the first frame update
    void Start()
    {
        int Level = PlayerPrefs.GetInt("Level");

        if (W1 == true || Level >=1 )
        {
            L1.SetActive(true);
            P1.SetActive(false);
        }
        if (W2 == true || Level >= 2)
        {
            L2.SetActive(true);
            P2.SetActive(false);
        }
        if (W3 == true || Level >= 3)
        {
            L3.SetActive(true);
            P3.SetActive(false);
            L4.SetActive(true);
            P4.SetActive(false);
        }
    }

}
