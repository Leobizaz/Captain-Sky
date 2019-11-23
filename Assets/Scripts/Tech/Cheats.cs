using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cheats : MonoBehaviour
{
    public static bool CHEAT_Invencivel;
    bool onoff;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            CheckpointSystem.STAGEPHASE = "PHASE0";
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }   
        if (Input.GetKeyDown(KeyCode.F2))
        {
            CheckpointSystem.STAGEPHASE = "PHASE1";
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            CheckpointSystem.STAGEPHASE = "PHASE2";
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            CheckpointSystem.STAGEPHASE = "PHASE3";
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            CheckpointSystem.STAGEPHASE = "PHASE4";
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            onoff = !onoff;
            if (onoff)
                CHEAT_Invencivel = true;
            else
                CHEAT_Invencivel = false;
        }

        if (Input.GetKeyDown(KeyCode.F7))
        {

            ControleSelect.W1 = true;
            ControleSelect.W2 = true;
            ControleSelect.W3 = true;
        }

        if (Input.GetKeyDown(KeyCode.F8))
        {

            SceneManager.LoadScene("MainMenu");
        }

    }
}
