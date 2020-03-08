﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
// Font_Input subscripts. Order matters! It matches InputOptions list so that loading the dictionary works properly. 
// Playstation : { 3, 0, 2, 1, 17, 19, 16, 18 }
// XBOX        : { 6, 4, 7, 5, 17, 19, 16, 18 }
public class GameManager : MonoBehaviour
{
    // Global Bools
    public bool GameOn { get; set; }
    public bool MainMenuOn { get; set; } = true;
    public bool PauseMenuOn { get; set; } = false;

    //-------------------Player messages & data--------------------
    public Tuple<Sprite, string> player1Character { get; set; } = null;
    public Tuple<Sprite, string> player2Character { get; set; } = null;
    public GameObject DialogueBoxP1 { get; set; } = null;
    public GameObject DialogueBoxP2 { get; set; } = null;
    private int currentMessage = 0;
    List<string> playerMessages = new List<string>
    {
        "This is the test message...1",
        "This is the test message...2",
        "This is the test message...3",
        "This is the test message...4",
        "This is the test message...5",
        "This is the test message...6",
        "This is the test message...7",
        "This is the test message...8",
        "This is the test message...9",
        "This is the test message...10",
        "This is the test message...11",
        "This is the test message...12",
        "This is the test message...13",
    };
    Dictionary<string, int> playerMessageDictionary = new Dictionary<string, int>
    {
        { "This is the test message...1",0 },
        { "This is the test message...2",1 },
        { "This is the test message...3",2 },
        { "This is the test message...4",3 },
        { "This is the test message...5",4 },
        { "This is the test message...6",5 },
        { "This is the test message...7",6 },
        { "This is the test message...8",7 },
        { "This is the test message...9",8 },
        { "This is the test message...10",9 },
        { "This is the test message...11",10 },
        { "This is the test message...12",11 },
        { "This is the test message...13",12 },
    };
    //--------------------Player Message Data End----------------------
    
    // Singleton
    public static GameManager Instance = null;

    // Player Info
    public GameObject player1 { get; set; } = null;
    public GameObject player2 { get; set; } = null;

    //-----------------Input Options------------------
    public List<Sprite> playerActionSprites;
    private List<string> inputOptions = 
        new List<string>
        {
            "X",
            "O",
            "S",
            "T",
            "LT",
            "LB",
            "RT",
            "RB",
            "LeftJoy",
            "RightJoy",
            "Start",
            "Up",
            "Down",
            "Left",
            "Right",
        };

    private List<string> playerActionInputList =
        new List<string>
        {
            "X",
            "O",
            "S",
            "T",
            "Up",
            "Down",
            "Left",
            "Right",
        };
    // -----------------Input Options End-----------------

    public Dictionary<string, Sprite> playerActionDictionary;
    public void Start()
    {
        playerActionDictionary = new Dictionary<string, Sprite>();
        for (int i = 0; i < 8; i++)    
            playerActionDictionary.Add(playerActionInputList[i], playerActionSprites[i]);
        ShuffleList<string>(playerMessages);
    }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            QuitGame();
        if (Input.GetKeyDown(KeyCode.Return))
            StartGame();
    }
    // List Shuffler
    public static void ShuffleList<T>(List<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }

    //----------------------------PLAYER MANAGEMENT----------------------------
    public void NotifyGM(GameObject player)
    {
        if (player1 == null)
        {
            player1 = player;
            player1.name = "Player1";
            player.GetComponent<PlayerScript>().SetPlayer(1);
        }
        else if(player2 == null)
        {
            player2.name = "Player2";
            player2 = player;
            player.GetComponent<PlayerScript>().SetPlayer(2);
        }
    }
    
    public void AbilityCast(int player)
    {

    }
    public void ActionListComplete(int player, string message)
    {
        GameObject.Find("WaifuText").GetComponent<WaifuDialogue>().Reply(player, playerMessageDictionary[message]);
        SetActionList(player);
    }

    public void SetActionList(int p)
    {
        switch(p)
        {
            case 1:
                player1.GetComponent<PlayerScript>().Message = playerMessages[currentMessage];
                player1.GetComponent<PlayerScript>().ActionList = GenerateActionList(playerMessages[currentMessage], 1);
                player1.GetComponent<PlayerScript>().HasActionList = true;
                break;
            case 2:
                player2.GetComponent<PlayerScript>().Message = playerMessages[currentMessage];
                player2.GetComponent<PlayerScript>().ActionList = GenerateActionList(playerMessages[currentMessage], 2);
                player2.GetComponent<PlayerScript>().HasActionList = true;
                break;
        }
        
    }
    private int FindSpaces(string msg)
    {
        int temp = 0;
        foreach (var c in msg)
            temp += (c == ' ') ? 1 : 0;
        return temp;
    }
    public List<string> GenerateActionList(string msg, int p)
    {
        int spaces = FindSpaces(msg);
        List<string> temp = new List<string>();
        for (int i = 0; i < msg.Length - spaces; i++)
        {
            temp.Add(playerActionInputList[UnityEngine.Random.Range(0,8)]);
        }
        switch(p)
        {
            case 1:
                DialogueBoxP1.GetComponent<PlayerDialogueScript>().SetText(msg);
                break;
            case 2:
                DialogueBoxP2.GetComponent<PlayerDialogueScript>().SetText(msg);
                break;
        }
        currentMessage++;
        return temp;
    }
    
    public void AssignDialogueBox(GameObject g, int p)
    {
        switch (p)
        {
            case 1:
                DialogueBoxP1 = g;
                break;
            case 2:
                DialogueBoxP2 = g;
                break;
        }
    }
    //---------------------------Player Management Done-----------------------------

    //---------------------------Menu Options---------------------------------------
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void StartGame()
    {
        //StartCoroutine(LoadAsync()); // *************************************
        SceneManager.LoadScene("Teo");
        MainMenuOn = false;
        if (player1)
            player1.GetComponent<PlayerScript>().DisableMenuActions();
        if(player2)
            player2.GetComponent<PlayerScript>().DisableMenuActions();
        GameOn = true;
    }

    public void Pause()
    {
        if(player1)
            player1.GetComponent<PlayerScript>().EnableMenuActions();
        if(player2)
            player2.GetComponent<PlayerScript>().EnableMenuActions();
        GameObject.Find("PauseMenu").GetComponent<Canvas>().sortingOrder = 1;
        PauseMenuOn = true;
        GameOn = false;
    }

    public void Unpause()
    {
        if (player1)
            player1.GetComponent<PlayerScript>().DisableMenuActions();
        if (player2)
            player2.GetComponent<PlayerScript>().DisableMenuActions();
        GameObject.Find("PauseMenu").GetComponent<Canvas>().sortingOrder = -1;
        PauseMenuOn = false;
        GameOn = true;
    }

    public void NewGame()
    {
        GameOn = false;
        MainMenuOn = true;
        PauseMenuOn = false;
        ShuffleList<string>(playerMessages);
        currentMessage = 0;
        if (player1)
            player1.GetComponent<PlayerScript>().NewGame();
        if (player2)
            player2.GetComponent<PlayerScript>().NewGame();
        SceneManager.LoadScene("Menu");
    }

    //----------------------------Menu Options End-------------------------------
}