using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DicaAnimation : MonoBehaviour
{
    public string texto;
    public Sprite img;

    public TextMeshProUGUI TXT;
    public Image IMG;

    void Update()
    {
        TXT.text = texto;
        IMG.sprite = img;
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }
}
