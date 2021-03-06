﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
 
public class PJLoad : Manager {

    private AudioSource audioSrc;

    [Header("Scenes")]
    public GameObject fight;
    public GameObject table;
    public GameObject championship;
    public GameObject tutorial;
    public GameObject wallTable;
    public GameObject buttonCartel;
    public GameObject buttonExit;

    [Header("Load Objects")]
    public GameObject playerTable;
    public GameObject imagePlayer;
    public GameObject cartelPlayer;
    public GameObject cartelEnemy;
    public GameObject btnCup;
    public GameObject playerFlag;

    [Header("Settings Player")]
    public Sprite[] posterPlayer;
    public Sprite[] headPlayer;
    public Sprite[] flagPlayer;
    public TextMeshProUGUI tableNamePlayer;
    public TextMeshProUGUI carteleNamePlayer;

    [Header("Settings Enemy")]
    public Sprite[] enemy; 
    public TextMeshProUGUI[] positions;
    public TextMeshProUGUI[] names;
    public GameObject[] eliminated;
    public TextMeshProUGUI fightName;

    [Header("Black Animation")]
    public Animator BlackPanel;


    private void Start() {
        //PlayerPrefs.DeleteAll();
        audioSrc=GetComponent<AudioSource>();
        audioSrc.volume=PlayerPrefs.GetFloat("volume");

        int IDPlayer = GlobalManager.GameplayData.IDPlayer;


        if (IDPlayer == 1)
        {
            imagePlayer.GetComponent<Image>().sprite = posterPlayer[0];
            cartelPlayer.GetComponent<Image>().sprite = headPlayer[0];
            playerFlag.GetComponent<Image>().sprite = flagPlayer[0];
            tableNamePlayer.GetComponent<TextMeshProUGUI>().text=" Arlen Smith";
            carteleNamePlayer.GetComponent<TextMeshProUGUI>().text="Arlen Smith";
        }
        else if (IDPlayer == 2)
        {
            imagePlayer.GetComponent<Image>().sprite = posterPlayer[1];
            cartelPlayer.GetComponent<Image>().sprite = headPlayer[1];
            playerFlag.GetComponent<Image>().sprite = flagPlayer[1];
            tableNamePlayer.GetComponent<TextMeshProUGUI>().text=" Daga Johar";
            carteleNamePlayer.GetComponent<TextMeshProUGUI>().text="Daga Johar";
        }
        else if (IDPlayer == 3)
        {
            imagePlayer.GetComponent<Image>().sprite = posterPlayer[2];
            cartelPlayer.GetComponent<Image>().sprite = headPlayer[2];
            playerFlag.GetComponent<Image>().sprite = flagPlayer[2];
            tableNamePlayer.GetComponent<TextMeshProUGUI>().text=" Irina Jones";
            carteleNamePlayer.GetComponent<TextMeshProUGUI>().text="Irina Jones";
        }
        else if (IDPlayer == 4)
        {
            imagePlayer.GetComponent<Image>().sprite = posterPlayer[3];
            cartelPlayer.GetComponent<Image>().sprite = headPlayer[3];
            playerFlag.GetComponent<Image>().sprite = flagPlayer[3];
            tableNamePlayer.GetComponent<TextMeshProUGUI>().text=" Angenis Nadai";
            carteleNamePlayer.GetComponent<TextMeshProUGUI>().text="Angenis Nadai";
        }

        //positions
        positions[0].GetComponent<TextMeshProUGUI>().text="1";
        positions[1].GetComponent<TextMeshProUGUI>().text="2";
        positions[2].GetComponent<TextMeshProUGUI>().text="3";
        positions[3].GetComponent<TextMeshProUGUI>().text="4";
        positions[4].GetComponent<TextMeshProUGUI>().text="5";
        positions[5].GetComponent<TextMeshProUGUI>().text="6";
        positions[6].GetComponent<TextMeshProUGUI>().text="7";
        positions[7].GetComponent<TextMeshProUGUI>().text="8";


        playerTable.transform.SetSiblingIndex(PlayerPrefs.GetInt("IDEnemy"));
        //Debug.Log(transform.GetSiblingIndex());

        if(PlayerPrefs.GetInt("IDEnemy")==7)
        {
            fightName.GetComponent<TextMeshProUGUI>().text="Dante Gray";
            cartelEnemy.GetComponent<Image>().sprite = enemy[0];
        }
        else if(PlayerPrefs.GetInt("IDEnemy")==6)
        {
            fightName.GetComponent<TextMeshProUGUI>().text="Kwan Lee";
            cartelEnemy.GetComponent<Image>().sprite = enemy[1];

            positions[6].GetComponent<TextMeshProUGUI>().text="8";
            positions[7].GetComponent<TextMeshProUGUI>().text="7";

            names[6].GetComponent<TextMeshProUGUI>().color= new Color(1f,1f,1f,0.5f);

        }
        else if(PlayerPrefs.GetInt("IDEnemy")==5)
        {
            fightName.GetComponent<TextMeshProUGUI>().text="Black Dwayne";
            cartelEnemy.GetComponent<Image>().sprite = enemy[2];

            positions[5].GetComponent<TextMeshProUGUI>().text="7";
            positions[6].GetComponent<TextMeshProUGUI>().text="8";
            positions[7].GetComponent<TextMeshProUGUI>().text="6";

            names[6].GetComponent<TextMeshProUGUI>().color= new Color(1f,1f,1f,0.5f);
            names[5].GetComponent<TextMeshProUGUI>().color= new Color(1f,1f,1f,0.5f);

        }
        else if(PlayerPrefs.GetInt("IDEnemy")==4)
        {
            fightName.GetComponent<TextMeshProUGUI>().text="Charlotte A";
            cartelEnemy.GetComponent<Image>().sprite = enemy[3];

            positions[4].GetComponent<TextMeshProUGUI>().text="6";
            positions[5].GetComponent<TextMeshProUGUI>().text="7";
            positions[6].GetComponent<TextMeshProUGUI>().text="8";
            positions[7].GetComponent<TextMeshProUGUI>().text="5";

            names[6].GetComponent<TextMeshProUGUI>().color= new Color(1f,1f,1f,0.5f);
            names[5].GetComponent<TextMeshProUGUI>().color= new Color(1f,1f,1f,0.5f);
            names[4].GetComponent<TextMeshProUGUI>().color= new Color(1f,1f,1f,0.5f);

        }
        else if(PlayerPrefs.GetInt("IDEnemy")==3)
        {
            fightName.GetComponent<TextMeshProUGUI>().text="Miguel Ruiz";
            cartelEnemy.GetComponent<Image>().sprite = enemy[4];

            if(PlayerPrefs.GetInt("WinZoneInLose")==1)
            {
                buttonCartel.SetActive(false);
                buttonExit.SetActive(false);
                Classified();
            }

            positions[3].GetComponent<TextMeshProUGUI>().text="5";
            positions[4].GetComponent<TextMeshProUGUI>().text="6";
            positions[5].GetComponent<TextMeshProUGUI>().text="7";
            positions[6].GetComponent<TextMeshProUGUI>().text="8";
            positions[7].GetComponent<TextMeshProUGUI>().text="4";

            names[6].GetComponent<TextMeshProUGUI>().color= new Color(1f,1f,1f,0.5f);
            names[5].GetComponent<TextMeshProUGUI>().color= new Color(1f,1f,1f,0.5f);
            names[4].GetComponent<TextMeshProUGUI>().color= new Color(1f,1f,1f,0.5f);
            names[3].GetComponent<TextMeshProUGUI>().color= new Color(1f,1f,1f,0.5f);

        }
        else if(PlayerPrefs.GetInt("IDEnemy")==2)
        {
            fightName.GetComponent<TextMeshProUGUI>().text="Alex Duran";
            cartelEnemy.GetComponent<Image>().sprite = enemy[5];

            if(PlayerPrefs.GetInt("WinZoneInLose")==1)
            {
                buttonCartel.SetActive(false);
                buttonExit.SetActive(false);
                Classified();
            }

            positions[2].GetComponent<TextMeshProUGUI>().text="4";
            positions[3].GetComponent<TextMeshProUGUI>().text="5";
            positions[4].GetComponent<TextMeshProUGUI>().text="6";
            positions[5].GetComponent<TextMeshProUGUI>().text="7";
            positions[6].GetComponent<TextMeshProUGUI>().text="8";
            positions[7].GetComponent<TextMeshProUGUI>().text="3";

            names[6].GetComponent<TextMeshProUGUI>().color= new Color(1f,1f,1f,0.5f);
            names[5].GetComponent<TextMeshProUGUI>().color= new Color(1f,1f,1f,0.5f);
            names[4].GetComponent<TextMeshProUGUI>().color= new Color(1f,1f,1f,0.5f);
            names[3].GetComponent<TextMeshProUGUI>().color= new Color(1f,1f,1f,0.5f);
            names[2].GetComponent<TextMeshProUGUI>().color= new Color(1f,1f,1f,0.5f);

        }
        else if(PlayerPrefs.GetInt("IDEnemy")==1)
        {
            fightName.GetComponent<TextMeshProUGUI>().text="Korona";
            cartelEnemy.GetComponent<Image>().sprite = enemy[6];

            if(PlayerPrefs.GetInt("WinZoneInLose")==1)
            {
                buttonCartel.SetActive(false);
                buttonExit.SetActive(false);
                Classified();
            }

            positions[1].GetComponent<TextMeshProUGUI>().text="3";
            positions[2].GetComponent<TextMeshProUGUI>().text="4";
            positions[3].GetComponent<TextMeshProUGUI>().text="5";
            positions[4].GetComponent<TextMeshProUGUI>().text="6";
            positions[5].GetComponent<TextMeshProUGUI>().text="7";
            positions[6].GetComponent<TextMeshProUGUI>().text="8";
            positions[7].GetComponent<TextMeshProUGUI>().text="2";

            names[6].GetComponent<TextMeshProUGUI>().color= new Color(1f,1f,1f,0.5f);
            names[5].GetComponent<TextMeshProUGUI>().color= new Color(1f,1f,1f,0.5f);
            names[4].GetComponent<TextMeshProUGUI>().color= new Color(1f,1f,1f,0.5f);
            names[3].GetComponent<TextMeshProUGUI>().color= new Color(1f,1f,1f,0.5f);
            names[2].GetComponent<TextMeshProUGUI>().color= new Color(1f,1f,1f,0.5f);
            names[1].GetComponent<TextMeshProUGUI>().color= new Color(1f,1f,1f,0.5f);

        }
        else if(PlayerPrefs.GetInt("IDEnemy")==0)
        {
            buttonCartel.SetActive(false);
            buttonExit.SetActive(false);

            Classified();

            positions[0].GetComponent<TextMeshProUGUI>().text="2";
            positions[1].GetComponent<TextMeshProUGUI>().text="3";
            positions[2].GetComponent<TextMeshProUGUI>().text="4";
            positions[3].GetComponent<TextMeshProUGUI>().text="5";
            positions[4].GetComponent<TextMeshProUGUI>().text="6";
            positions[5].GetComponent<TextMeshProUGUI>().text="7";
            positions[6].GetComponent<TextMeshProUGUI>().text="8";
            positions[7].GetComponent<TextMeshProUGUI>().text="1";

            names[6].GetComponent<TextMeshProUGUI>().color= new Color(1f,1f,1f,0.5f);
            names[5].GetComponent<TextMeshProUGUI>().color= new Color(1f,1f,1f,0.5f);
            names[4].GetComponent<TextMeshProUGUI>().color= new Color(1f,1f,1f,0.5f);
            names[3].GetComponent<TextMeshProUGUI>().color= new Color(1f,1f,1f,0.5f);
            names[2].GetComponent<TextMeshProUGUI>().color= new Color(1f,1f,1f,0.5f);
            names[1].GetComponent<TextMeshProUGUI>().color= new Color(1f,1f,1f,0.5f);
            names[0].GetComponent<TextMeshProUGUI>().color= new Color(1f,1f,1f,0.5f);

        }
    }

    public void Classified()
    {
        if(wallTable!=null && championship!=null)
        {
            Animator animator = wallTable.GetComponent<Animator>();
            Animator animator2 = championship.GetComponent<Animator>();

            if(animator!=null && animator2!=null)
            {
                animator.SetBool("Championship", true);
                animator2.SetBool("Championship", true);
            }
        }
    }

    public void goCup(string Level)
    {
        PlayerPrefs.SetInt("IDEnemy", 12);
        StartCoroutine(LoadYourAsyncScene(Level));
    }

    public void returnMenu(string Level)
    {
        StartCoroutine(LoadYourAsyncScene(Level));
    }

    IEnumerator LoadYourAsyncScene(string level)
    {
        BlackPanel.SetTrigger("Out");

        yield return new WaitForSeconds(0.5f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(level);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void OpenTutorial()
    {
        if(tutorial!=null && fight!=null)
        {
            Animator animator = fight.GetComponent<Animator>();
            Animator animator2 = tutorial.GetComponent<Animator>();

            if(animator!=null && animator2!=null)
            {
                animator.SetBool("Open2", true);
                animator2.SetBool("Open", true);
            }
        }
    }

    public void CloseTutorial()
    {
        if(tutorial!=null && fight!=null)
        {
            Animator animator = fight.GetComponent<Animator>();
            Animator animator2 = tutorial.GetComponent<Animator>();

            if(animator!=null && animator2!=null)
            {
                animator.SetBool("Open2", false);
                animator2.SetBool("Open", false);
            }
        }
    }

}