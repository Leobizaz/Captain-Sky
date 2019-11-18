using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialogoBriefing : MonoBehaviour
{
    public Sprite image;
    public string texto;
    public float tempoqueleva;
    public bool wilburne;
    public bool sameIMG;
    public float textSpeed;
    public float delay;

    private Text text;
    private AudioSource audio;

    private Color32 inactiveColor = new Color(73, 73, 73, 255);
    private Color32 activeColor = new Color(255, 255, 255, 255);
    private Color32 oofColor = new Color(73, 73, 73, 0);

    private Image wilburneimg;
    private Image squadimg;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        inactiveColor = new Color32(73, 73, 73, 255);
        activeColor = new Color32(255, 255, 255, 255);
        text = GameObject.Find("Canvas/Briefing/Textbox/BriefingText").GetComponent<Text>();
        wilburneimg = GameObject.Find("Wilburne").GetComponent<Image>();
        squadimg = GameObject.Find("Squad").GetComponent<Image>();
    }

    public void PlayDialogo()
    {
        RadioBriefing.alguemfalando = true;
        Invoke("ParouDeFalar", tempoqueleva);
        if (!wilburne)
        {
            Sequence changeSprite = DOTween.Sequence();
            wilburneimg.DOColor(inactiveColor, 0.3f);
            //squadimg.DOColor(activeColor, 0.3f);
            if (!sameIMG)
            {
                StartCoroutine(Cu());
            }
            else
            {
                squadimg.DOColor(activeColor, 0.3f);
            }
        }
        else
        {
            squadimg.DOColor(inactiveColor, 0.3f);
            //wilburneimg.DOColor(activeColor, 0.3f);
            if (!sameIMG)
            {
                StartCoroutine(Cu2());
            }
            else
            {
                wilburneimg.DOColor(activeColor, 0.3f);
            }



        }
        audio.Play();
        text.text = "";
        text.DOText(texto, textSpeed);



    }

    void ParouDeFalar()
    {
        RadioBriefing.alguemfalando = false;
    }

    IEnumerator Cu()
    {
        squadimg.DOColor(inactiveColor, 0.3f);
        yield return new WaitForSeconds(0.3f);
        squadimg.sprite = image;
        yield return new WaitForSeconds(0.1f);
        squadimg.DOColor(activeColor, 0.3f);
    }

    IEnumerator Cu2()
    {
        wilburneimg.DOColor(inactiveColor, 0.3f);
        yield return new WaitForSeconds(0.3f);
        wilburneimg.sprite = image;
        yield return new WaitForSeconds(0.1f);
        wilburneimg.DOColor(activeColor, 0.3f);
    }


}
