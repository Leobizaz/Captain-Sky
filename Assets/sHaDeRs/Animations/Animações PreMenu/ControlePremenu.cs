using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlePremenu : MonoBehaviour
{
    public GameObject CBG;
    public GameObject Controle;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Ativa", 4.2f);
        Invoke("Menu", 9f);
    }


    public void Ativa()
    {
        Controle.SetActive(true);
    }
    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
