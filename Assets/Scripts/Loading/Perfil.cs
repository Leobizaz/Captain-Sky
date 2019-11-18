﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Perfil : MonoBehaviour
{
    public TextMeshProUGUI ScoreP1Menu;
    public TextMeshProUGUI ScoreP2Menu;
    public TextMeshProUGUI ScoreP3Menu;

    public TextMeshProUGUI ScoreP1trocaperfil;
    public TextMeshProUGUI ScoreP2trocaperfil;
    public TextMeshProUGUI ScoreP3trocaperfil;

    public TextMeshProUGUI R1M;
    public TextMeshProUGUI R2M;
    public TextMeshProUGUI R3M;

    public TextMeshProUGUI R1P;
    public TextMeshProUGUI R2P;
    public TextMeshProUGUI R3P;

    public static string RankP1Menu;
    public static string RankP2Menu;
    public static string RankP3Menu;

    public static float N1;
    public static float N2;
    public static float N3;

    public GameObject Pnew1;
    public GameObject Pnew2;
    public GameObject Pnew3;

    public GameObject P1;
    public GameObject P2;
    public GameObject P3;

    // Start is called before the first frame update
    void Start()
    {
        ///////////////////LIGAR ISSO ANTES DE BUILDAR////////////////////////////////////
        //Delete();

        N1 = PlayerPrefs.GetFloat("Score1");
         N2 = PlayerPrefs.GetFloat("Score2");
        N3 = PlayerPrefs.GetFloat("Score3");


        if (N1 > 0)
        {
            Pnew1.SetActive(false);
            P1.SetActive(true);
        }
        if (N2 > 0)
        {
            Pnew2.SetActive(false);
            P2.SetActive(true);
        }
        if (N3 > 0)
        {
            Pnew3.SetActive(false);
            P3.SetActive(true);
        }

        if (N1 >= 0 && N1 < 1000)
        {
            RankP1Menu = "F";
        }

        if (N1 > 1000 && N1 < 3000)
        {
            RankP1Menu = "D";
        }
        if (N1 > 3000 && N1 < 6000)
        {
            RankP1Menu = "C";
        }
        if (N1 > 6000 && N1 < 10000)
        {
            RankP1Menu = "B";
        }
        if (N1 > 10000 && N1 < 15000)
        {
            RankP1Menu = "A";
        }
        if (N1 > 15000)
        {
            RankP1Menu = "S";
        }


        if (N2 >= 0 && N2 < 1000)
        {
            RankP2Menu = "F";
        }

        if (N2 > 1000 && N2 < 3000)
        {
            RankP2Menu = "D";
        }
        if (N2 > 3000 && N2 < 6000)
        {
            RankP2Menu = "C";
        }
        if (N2 > 6000 && N2 < 10000)
        {
            RankP2Menu = "B";
        }
        if (N2 > 10000 && N2 < 15000)
        {
            RankP2Menu = "A";
        }
        if (N2 > 15000)
        {
            RankP2Menu = "S";
        }


        if (N3 >= 0 && N3 < 1000)
        {
            RankP3Menu = "F";
        }

        if (N3 > 1000 && N3 < 3000)
        {
            RankP3Menu = "D";
        }
        if (N3 > 3000 && N3 < 6000)
        {
            RankP3Menu = "C";
        }
        if (N3 > 6000 && N3 < 10000)
        {
            RankP3Menu = "B";
        }
        if (N3 > 10000 && N3 < 15000)
        {
            RankP3Menu = "A";
        }
        if (N3 > 15000)
        {
            RankP3Menu = "S";
        }





        R1M.text = "" + RankP1Menu;
        R2M.text = "" + RankP2Menu;
        R3M.text = "" + RankP3Menu;
        R1P.text = "" + RankP1Menu;
        R2P.text = "" + RankP2Menu;
        R3P.text = "" + RankP3Menu;

        ScoreP1Menu.text = "" + N1;
        ScoreP2Menu.text = "" + N2;
        ScoreP3Menu.text = "" + N3;
        ScoreP1trocaperfil.text = "" + N1;
        ScoreP2trocaperfil.text = "" + N2;
        ScoreP3trocaperfil.text = "" + N3;




    }

    ///////////////Deletar TODOS OS SAVES antes da build////////////////
    

    public void Delete()
    {
        PlayerPrefs.DeleteAll();
    }



    
}