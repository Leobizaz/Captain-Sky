﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject gameOverScreen;
    public GameObject tentarNovamente;
    public PlayerOpenMovement playerOpenMovement;
    public PlayerMovement playerMovement;
    private AudioSource[] allAudioSources;
    public Toggle ToggleControleInvertido;
    public static bool paused;
    public static bool victory;
    public static bool controleInvertido;
    public EventSystem eventSys;
    public GameObject pauseScreenContinuar;
    bool onOptions;

    bool teclado;
    bool controle;

    public GameObject menuOpcoes;
    public GameObject menuPause;
    bool liberado;

    void Awake()
    {
        PlayerHealth.dead = false;
        victory = false;
        ComunicadorAdam.active = false;
        ComunicadorJack.active = false;
        ComunicadorCharlie.active = false;
        ComunicadorMarcus.active = false;
    }

    public void Start()
    {
        PlayerOpenMovement.ded = 1;
        PlayerMovement.ded = 1;
        DialogoSequence.isPlayingSequence = false;
        InstantiateDialogo.dialogoPlaying = false;
        PlayerHealth.dead = false;
        victory = false;
        paused = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            teclado = true;
            controle = false;
        }
        if(Input.GetAxis("VerticalDireito") != 0 || Input.GetAxis("HorizontalDireito") != 0)
        {
            teclado = false;
            controle = true;
        }




        if (Input.GetButtonDown("Pause") || Input.GetKeyDown(KeyCode.Escape))
        {
            liberado = false;
            if(!IsInvoking("Libera"))
            Invoke("Libera", 0.3f);

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

        if ((Input.GetButtonDown("Cancel") && !onOptions) && liberado)
            UnPauseGame();

        if(onOptions && Input.GetButtonDown("Cancel"))
        {
            menuOpcoes.SetActive(false);
            menuPause.SetActive(true);
            LoadMenu();
        }

        

    }

    public void Libera()
    {
        liberado = true;
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

    public void LoadGameOverMenu()
    {
        gameOverScreen.SetActive(true);
        eventSys.SetSelectedGameObject(tentarNovamente);
    }

    public void LoadMenu()
    {
        onOptions = false;
        eventSys.SetSelectedGameObject(pauseScreenContinuar);
    }

    public void Retry()
    {
        paused = false;
        PlayerHealth.dead = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadAto3()
    {
        SceneManager.LoadScene("Ato 3 - Comes e bebes");
    }
    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

}