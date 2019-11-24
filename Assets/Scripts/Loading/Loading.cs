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
    public GameObject briefing;
    public GameObject Ato12;
    public GameObject Ato23;
    private float speed = 0.7f;
    bool fill = false;
    public Image FillCircle;

    private void Start()
    {
        ScoreSystem.playerdeaths = 0;
        LoadLevel(levelIndex);
        //controlIcon.SetActive(true);
        if (levelIndex == 2 || levelIndex == 6)
        {
            briefing.SetActive(false);
            Ato12.SetActive(false);
            Ato23.SetActive(false);
        }
        if (levelIndex == 3)
        {
            briefing.SetActive(true);
            Ato12.SetActive(true);
            Ato23.SetActive(false);
        }
        if (levelIndex == 4)
        {
            briefing.SetActive(true);
            Ato12.SetActive(false);
            Ato23.SetActive(true);
        }
    }

    public void LoadLevel(int sceneIndex)
    {
        Pause.victory = false;
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
                progressText.text = "Segure        /SPACE para continuar";
                //controlIcon.SetActive(true);
                //Wait to you press the space key to activate the Scene
                if ((Input.GetButtonDown("Break") || Input.GetKeyDown(KeyCode.Space)) && !IsInvoking("SkipIntro") && FillCircle != null)
                {
                    fill = true;
                    Invoke("SkipIntro", 3f);
                }
                if (Input.GetButtonUp("Break") || Input.GetKeyUp(KeyCode.Space) && FillCircle != null)
                {
                    fill = false;
                    FillCircle.GetComponent<Image>().fillAmount = 0;
                    CancelInvoke("SkipIntro");
                }

                if (fill == true && FillCircle != null)
                {
                    FillCircle.GetComponent<Image>().fillAmount += 0.5f * speed * Time.deltaTime;
                }
            }
            yield return null;
        }
    }
    void SkipIntro()
    {
        operation.allowSceneActivation = true;
    }
}




