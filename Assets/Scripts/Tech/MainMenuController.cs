using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public EventSystem eventSys;
    public GameObject fadeOut;
    public GameObject botao_title;
    public GameObject background;

    public GameObject botao_opções;
    public GameObject botao_extras;
    public GameObject botao_perfil;

    public GameObject profile1;
    public GameObject profile2;
    public GameObject profile3;
    public static bool p1 = false;
    public static bool p2 = false;
    public static bool p3 = false;

    public GameObject profile1Display;
    public GameObject profile2Display;
    public GameObject profile3Display;

    public static GameObject selectedProfile;
    public static float sensibilidade = 0.8f;
    public Slider slider;

    public GameObject telaOpções;
    public GameObject telaMenu;
    public GameObject telaExtras;
    public GameObject telaPerfil;
    public GameObject telaSelecionar;
    public GameObject Titulo;
    public GameObject telaJogar;

    public GameObject voltarButtonExtras;

    public GameObject fase1Seleção;

    public GameObject botao_continuar;
    public GameObject botao_seleção;
    public GameObject botao_inicio;
    public Toggle toggle_inverterControles;

    public GameObject lastSelected;

    public GameObject titulo;

    public Camera cam;

    public string whichPanel;

    void Start()
    {
        CheckpointSystem.STAGEPHASE = "PHASE0";



        toggle_inverterControles.isOn = Pause.controleInvertido;
        eventSys.SetSelectedGameObject(botao_title);
        if(selectedProfile == null)
        selectedProfile = profile1;
    }

    void Update()
    {

        sensibilidade = slider.value;

        Debug.Log(selectedProfile);
        Pause.controleInvertido = toggle_inverterControles.isOn;
        if(eventSys.currentSelectedGameObject != null)
        lastSelected = eventSys.currentSelectedGameObject;

        eventSys.SetSelectedGameObject(lastSelected);

        if(ApplicationUtility.IsActivated())
        {

        }
        var colorSelected = GetComponent<Button> ().colors;
        var colorNormal = GetComponent<Button>().colors;
        colorSelected.normalColor = new Color(255,255,255,255);
        colorNormal.normalColor = new Color(255,255,255,0);
        if(selectedProfile == profile1)
        {
            p1 = true;
            profile1Display.SetActive(true);
            profile2Display.SetActive(false);
            profile3Display.SetActive(false);
            profile1.GetComponent<Button>().colors = colorSelected;
            profile2.GetComponent<Button>().colors = colorNormal;
            profile3.GetComponent<Button>().colors = colorNormal;
        } if(selectedProfile == profile2)
        {
            p2 = true;
            profile1Display.SetActive(false);
            profile2Display.SetActive(true);
            profile3Display.SetActive(false);
            profile1.GetComponent<Button>().colors = colorNormal;
            profile2.GetComponent<Button>().colors = colorSelected;
            profile3.GetComponent<Button>().colors = colorNormal;
        } if(selectedProfile == profile3)
        {
            p3 = true;
            profile1Display.SetActive(false);
            profile2Display.SetActive(false);
            profile3Display.SetActive(true);
            profile1.GetComponent<Button>().colors = colorNormal;
            profile2.GetComponent<Button>().colors = colorNormal;
            profile3.GetComponent<Button>().colors = colorSelected;
        }

        if(Input.GetButtonDown("Cancel"))
        {
            switch(whichPanel)
            {
                case "Options":
                    telaOpções.SetActive(false);
                    telaMenu.SetActive(true);
                    GoingToMenuFromOptions();
                break;
                case "Extras":
                    telaExtras.SetActive(false);
                    telaMenu.SetActive(true);
                    GoingToMenuFromExtras();
                break;
                case "Seleção":
                    telaSelecionar.SetActive(false);
                    telaJogar.SetActive(true);
                    Jogar();
                break;
                case "Perfil":
                    telaPerfil.SetActive(false);
                    telaMenu.SetActive(true);
                    GoingToMenuFromPerfil();
                break;
                case "Jogar":
                    telaJogar.SetActive(false);
                    telaMenu.SetActive(true);
                    background.SetActive(true);
                    GoingToMenuFromSkins();
                break;
                case "Menu":
                    telaMenu.SetActive(true);
                    telaJogar.SetActive(false);
                    background.SetActive(true);

                    break;
            }
        }
        


    }

    void OnApplicationFocus(bool focusStatus) 
    {
        eventSys.SetSelectedGameObject(lastSelected);    
    }

    public void GoingToOptions()
    {
        whichPanel = "Options";
        eventSys.SetSelectedGameObject(toggle_inverterControles.gameObject);
    }

    public void GoingToPerfil()
    {
        telaPerfil.SetActive(true);
        whichPanel = "Perfil";
        if(selectedProfile != null)
        eventSys.SetSelectedGameObject(selectedProfile);
        else
        {
            eventSys.SetSelectedGameObject(profile1);
        }
    }

    public void GoingToSeleção()
    {
        whichPanel = "Seleção";
        eventSys.SetSelectedGameObject(fase1Seleção);
    }

    public void GoingToExtras()
    {
        whichPanel = "Extras";
        eventSys.SetSelectedGameObject(voltarButtonExtras);
    }
    

    public void SelectPerfil1()
    {
        selectedProfile = profile1;
    }
    public void SelectPerfil2()
    {
        selectedProfile = profile2;
    }
    public void SelectPerfil3()
    {
        selectedProfile = profile3;
    }

    public void GoingToMenuFromOptions()
    {
        eventSys.SetSelectedGameObject(botao_opções);
        whichPanel = "Menu";
    }

    public void GoingToMenuFromExtras()
    {
        eventSys.SetSelectedGameObject(botao_extras);
        whichPanel = "Menu";
    }

    public void GoingToMenuFromPerfil()
    {
        eventSys.SetSelectedGameObject(botao_perfil);
        whichPanel = "Menu";
    }

    public void GoingToMenuFromSeleção()
    {
        eventSys.SetSelectedGameObject(botao_seleção);
        whichPanel = "Menu";
    }

    public void GoingToMenuFromSkins()
    {
        eventSys.SetSelectedGameObject(botao_continuar);
        cam.transform.position = new Vector3(0f, 0f, 0f);
        whichPanel = "Menu";
    }

    public void Jogar()
    {
        whichPanel = "Jogar";
        telaJogar.SetActive(true);
        cam.transform.position = new Vector3(909f,811f,-981f);
        eventSys.SetSelectedGameObject(botao_inicio);
    }
    public void ButtonContinuar()
    {
        fadeOut.SetActive(true);
        Invoke("LoadFase", 3f);
    }

    void LoadFase()
    {
        SceneManager.LoadScene("Loading1");
        Loading.levelIndex = 1;
    }

    public void ButtonFase3()
    {
        fadeOut.SetActive(true);
        Invoke("Fase3", 3f);
    }

    public void ButtonFase2()
    {
        fadeOut.SetActive(true);
        Invoke("Fase2", 3f);
    }
    public void ButtonFase1()
    {
        fadeOut.SetActive(true);
        Invoke("Fase1", 3f);
    }
    public void Fase3()
    {
        SceneManager.LoadScene("Loading1");
        Loading.levelIndex = 4;
    }
    public void Fase2()
    {
        SceneManager.LoadScene("Loading1");
        Loading.levelIndex = 3;
    }
    public void Fase1()
    {
        SceneManager.LoadScene("Loading1");
        Loading.levelIndex = 2;
    }
    public void DesativaTitulo()
    {
        Titulo.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
