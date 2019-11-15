using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Loading : MonoBehaviour
{

    public static int levelIndex;
    public TextMeshProUGUI progressText;
    public Slider slider;
    //public GameObject controlIcon;
    public AsyncOperation operation;
    public GameObject pular;
    public GameObject carregando;

    private void Start()
    {
        LoadLevel(levelIndex);
        //controlIcon.SetActive(true);
    }

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        yield return new WaitForSeconds(1);
        operation = SceneManager.LoadSceneAsync(sceneIndex);

        operation.allowSceneActivation = false;


        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            progressText.text = progress * 100f + "%";
            if (operation.progress >= 0.9f)
            {
                slider.gameObject.SetActive(false);
                carregando.SetActive(false);
                pular.SetActive(true);
                //Change the Text to show the Scene is ready
                progressText.text = "Aperte        /SPACE para continuar";
                //controlIcon.SetActive(true);
                //Wait to you press the space key to activate the Scene
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Break"))
                {
                    operation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
}




