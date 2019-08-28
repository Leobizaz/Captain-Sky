using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Pause : MonoBehaviour
{
    public GameObject pauseScreen;
    public PlayerOpenMovement playerOpenMovement;
    public PlayerMovement playerMovement;
    private AudioSource[] allAudioSources;
    public Toggle ToggleControleInvertido;
    public static bool paused;
    public static bool controleInvertido;
    public EventSystem eventSys;
    public GameObject pauseScreenContinuar;
    bool onOptions;

    public GameObject menuOpcoes;
    public GameObject menuPause;

    void Awake()
    {

    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
            if (paused)
                PauseGame();
            else UnPauseGame();
        }

        if (ToggleControleInvertido.isOn)
        {
            controleInvertido = true;
        }
        else
        {
            controleInvertido = false;
        }

        if (Input.GetButtonDown("Cancel") && !onOptions)
            UnPauseGame();

        if(onOptions && Input.GetButtonDown("Cancel"))
        {
            menuOpcoes.SetActive(false);
            menuPause.SetActive(true);
            LoadMenu();
        }

        

    }

    public void PauseGame()
    {
        eventSys.SetSelectedGameObject(pauseScreenContinuar);
        paused = true;
        Debug.Log("Game paused");
        Time.timeScale = 0;
        AudioListener.pause = true;
        pauseScreen.SetActive(true);
    }

    public void UnPauseGame()
    {
        paused = false;
        Debug.Log("Game unpaused");
        Time.timeScale = 1;
        AudioListener.pause = false;
        pauseScreen.SetActive(false);
    }

    public void LoadOptionsMenu()
    {
        onOptions = true;
        eventSys.SetSelectedGameObject(ToggleControleInvertido.gameObject);
    }
    public void LoadMenu()
    {
        onOptions = false;
        eventSys.SetSelectedGameObject(pauseScreenContinuar);
    }

}
