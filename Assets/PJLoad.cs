﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
 
public class PJLoad : MonoBehaviour {

    public GameObject player, imagePlayer, cartelPlayer, cartelEnemy;

    public GameObject fight, table;
 
    public Sprite champ1, champ2, champ3, champ4;

    public Sprite enemy1,enemy2,enemy3,enemy4,enemy5,enemy6,enemy7, player1,player2,player3,player4; 
 
    public TextMeshProUGUI playerName, fightName, cartelName;

    private void Start() {

        player.transform.SetSiblingIndex(PlayerPrefs.GetInt("IDEnemy"));

        //Debug.Log(transform.GetSiblingIndex());


        if(PlayerPrefs.GetInt("IDPlayer")==1)
        {
            imagePlayer.GetComponent<Image>().sprite = champ1;
            cartelPlayer.GetComponent<Image>().sprite = player1;
            playerName.GetComponent<TextMeshProUGUI>().text="Arlen Smith";
            cartelName.GetComponent<TextMeshProUGUI>().text="Arlen Smith";
        }
        else if(PlayerPrefs.GetInt("IDPlayer")==2)
        {
            imagePlayer.GetComponent<Image>().sprite = champ2;
            cartelPlayer.GetComponent<Image>().sprite = player2;
            playerName.GetComponent<TextMeshProUGUI>().text="Daga Johar";
            cartelName.GetComponent<TextMeshProUGUI>().text="Daga Johar";
        }
        else if(PlayerPrefs.GetInt("IDPlayer")==3)
        {
            imagePlayer.GetComponent<Image>().sprite = champ3;
            cartelPlayer.GetComponent<Image>().sprite = player3;
            playerName.GetComponent<TextMeshProUGUI>().text="Irina Jones";
            cartelName.GetComponent<TextMeshProUGUI>().text="Irina Jones";
        }
        else if(PlayerPrefs.GetInt("IDPlayer")==4)
        {
            imagePlayer.GetComponent<Image>().sprite = champ4;
            cartelPlayer.GetComponent<Image>().sprite = player4;
            playerName.GetComponent<TextMeshProUGUI>().text="Angenis Nadai";
            cartelName.GetComponent<TextMeshProUGUI>().text="Angenis Nadai";
        }
    }

    public void OpenFight()
    {
        if(fight!=null && table!=null)
        {
            Animator animator2 = fight.GetComponent<Animator>();
            Animator animator3 = table.GetComponent<Animator>();

            if(animator2!=null && animator3!=null)
            {
                animator2.SetBool("Open", true);
                animator3.SetBool("Open", true);

                if(PlayerPrefs.GetInt("IDEnemy")==7)
                {
                    fightName.GetComponent<TextMeshProUGUI>().text="El Calvo";
                    cartelEnemy.GetComponent<Image>().sprite = enemy1;
                }
            }
        }


    }


}