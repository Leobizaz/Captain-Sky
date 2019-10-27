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

    public GameObject botao_opções;
    public GameObject botao_extras;
    public GameObject botao_perfil;

    public GameObject profile1;
    public GameObject profile2;
    public GameObject profile3;

    public GameObject profile1Display;
    public GameObject profile2Display;
    public GameObject profile3Display;

    public static GameObject selectedProfile;

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
    public Toggle toggle_inverterControles;

    public GameObject lastSelected;

    public GameObject titulo;

    public string whichPanel;

    void Start()
    {
        toggle_inverterControles.isOn = Pause.controleInvertido;
        eventSys.SetSelectedGameObject(botao_continuar, null);
        if(selectedProfile == null)
        selectedProfile = profile1;
    }

    void Update()
    {
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
            profile1Display.SetActive(true);
            profile2Display.SetActive(false);
            profile3Display.SetActive(false);
            profile1.GetComponent<Button>().colors = colorSelected;
            profile2.GetComponent<Button>().colors = colorNormal;
            profile3.GetComponent<Button>().colors = colorNormal;
        } if(selectedProfile == profile2)
        {
            profile1Display.SetActive(false);
            profile2Display.SetActive(true);
            profile3Display.SetActive(false);
            profile1.GetComponent<Button>().colors = colorNormal;
            profile2.GetComponent<Button>().colors = colorSelected;
            profile3.GetComponent<Button>().colors = colorNormal;
        } if(selectedProfile == profile3)
        {
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
                    telaMenu.SetActive(true);
                    GoingToMenuFromSeleção();
                break;
                case "Perfil":
                    telaPerfil.SetActive(false);
                    telaJogar.SetActive(true);

                break;
                case "Jogar":
                    telaJogar.SetActive(false);
                    telaMenu.SetActive(true);

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
    }

    public void GoingToMenuFromExtras()
    {
        eventSys.SetSelectedGameObject(botao_extras);
    }

    public void GoingToMenuFromPerfil()
    {
        eventSys.SetSelectedGameObject(botao_perfil);
    }

    public void GoingToMenuFromSeleção()
    {
        eventSys.SetSelectedGameObject(botao_seleção);
    }

    public void Jogar()
    {
        telaJogar.SetActive(true);
    }
    public void ButtonContinuar()
    {
        fadeOut.SetActive(true);
        Invoke("LoadFase", 3f);
    }

    void LoadFase()
    {
        SceneManager.LoadScene(1);
    }

    public void ButtonFase3()
    {
        fadeOut.SetActive(true);
        Invoke("Fase3", 3f);
    }

    public void Fase3()
    {
        SceneManager.LoadScene(2);
    }
    public void DesativaTitulo()
    {
        Titulo.SetActive(false);
    }
}
