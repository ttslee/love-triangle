﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerScript : MonoBehaviour
{
    // Mouse Object Info
    public GameObject mouse;
    public Color32 mouseColor1;
    public Color32 mouseColor2;

    // Message Info
    public string Message { get; set; }
    // InputImagesList
    private GameObject inputImageList;
    private bool hasImageList = false;

    // Player
    private int player = 0;
    public int Player { get { return player; } set { player = value; } }
    Vector2 move;
    // PlayerControls
    public PlayerControls controls { get; set; }
    
    // Input stream and String Info
    private Stack<string> history;
    private bool hasActionList = false;
    public bool HasActionList{ get{ return hasActionList; } set{ hasActionList = value; } }
    private List<string> actionList;
    public List<string> ActionList{ get{ return actionList; }   set{ actionList = value; }}
    private int loveBar = 0;
    private int abilityBar = 0;
    public int LoveBar { get { return loveBar; } set { loveBar = value; } }
    public int AbilityBar { get { return abilityBar; } set { abilityBar = value; } }

    void Start()
    {
        history = new Stack<string>();
        actionList = new List<string>();
        GameManager.Instance.NotifyGM(gameObject);
    }

    private void Awake()
    {
        controls = new PlayerControls();
        InputControlBindings();
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Johnny")
        {
            GameManager.Instance.MainMenuOn = false;
            GameManager.Instance.GameOn = true;
        }

        if (SceneManager.GetActiveScene().name == "Teo" || SceneManager.GetActiveScene().name == "Johnny")
        {
            if (!hasImageList)
                SetImageList(player);
            if (!hasActionList && GameManager.Instance.GameOn)
            {
                GameManager.Instance.SetActionList(player);
                
            }
            else if(GameManager.Instance.GameOn)
            {
                CheckActionListComplete();
                SetActionImages();
            }
        }
        keyBoardInput();
        MoveMouse();
    }
    private void keyBoardInput()
    {
        //if (Input.GetKeyDown(KeyCode.Return))
        //    GameManager.Instance.NewGame();
    }
    public void SetPlayer(int i)
    {
        player = i;
    }
    private void InputControlBindings()
    {
        controls.Gameplay.X.performed += ctx => PlayerAction("X");              // X button
        controls.Gameplay.O.performed += ctx => PlayerAction("O");              // circle
        controls.Gameplay.S.performed += ctx => PlayerAction("S");              // square
        controls.Gameplay.T.performed += ctx => PlayerAction("T");              // triangle
        controls.Gameplay.Down.performed += ctx => PlayerAction("Down");        // Down D-PAD
        controls.Gameplay.Left.performed += ctx => PlayerAction("Left");        // Left D-PAD
        controls.Gameplay.Right.performed += ctx => PlayerAction("Right");      // Right D-PAD
        controls.Gameplay.Up.performed += ctx => PlayerAction("Up");            // UP D-PAD
        controls.Gameplay.Start.performed += ctx => PlayerAction("Start");      // Options/Start
        controls.Gameplay.RT.performed += ctx => PlayerAction("RT");            // Right trigger
        controls.Gameplay.RB.performed += ctx => PlayerAction("RB");            // Right Bumper
        controls.Gameplay.LT.performed += ctx => PlayerAction("LT");            // Left Trigger
        controls.Gameplay.LB.performed += ctx => PlayerAction("LB");            // Left Bumper
        controls.Gameplay.LeftJoy.performed += ctx => move = ctx.ReadValue<Vector2>() * 1.2f;  // LeftJoy
        controls.Gameplay.LeftJoy.canceled += ctx => move = Vector2.zero;
    }

    private void PlayerAction(string action)
    {
        switch(action)
        {
            case "RT":
            case "RB":
            case "LT":
            case "LB":
                if (!GameManager.Instance.GameOn)
                    break;
                if(abilityBar >= 100f)
                    GameManager.Instance.AbilityCast(player);
                break;
            case "Start":
                if (!GameManager.Instance.GameOn)
                    GameManager.Instance.StartGame();  // *********************************************************************
                else if (GameManager.Instance.GameOn && !GameManager.Instance.PauseMenuOn)
                    GameManager.Instance.Pause();
                else if (GameManager.Instance.GameOn && GameManager.Instance.PauseMenuOn)
                    GameManager.Instance.Unpause();
                break;
            default:
                if (!GameManager.Instance.GameOn)
                    break;
                if (!hasActionList)
                    break;
                if (action == actionList[0])
                {
                    switch(player)
                    {
                        case 1:
                            GameManager.Instance.DialogueBoxP1.GetComponent<PlayerDialogueScript>().CorrectInput();
                            break;
                        case 2:
                            GameManager.Instance.DialogueBoxP2.GetComponent<PlayerDialogueScript>().CorrectInput();
                            break;
                    }
                    history.Push(action);
                    actionList.RemoveAt(0);
                }
                else
                {
                    if(history.Count != 0)
                    {
                        AbilityBar++;
                        switch (player)
                        {
                            case 1:
                                GameManager.Instance.DialogueBoxP1.GetComponent<PlayerDialogueScript>().IncorrectInput();
                                break;
                            case 2:
                                GameManager.Instance.DialogueBoxP2.GetComponent<PlayerDialogueScript>().IncorrectInput();
                                break;
                        }
                        actionList.Insert(0, history.Pop());
                    }
                        
                }
                break;
        }
    }

    
    
    
    private void CheckActionListComplete()
    {
        if(actionList.Count == 0)
        {
            history.Clear();
            hasActionList = false;
            LoveBar++;
            GameManager.Instance.ActionListComplete(player, Message);
        }
    }

    public void SetImageList(int p)
    {
        switch (p)
        {
            case 1:
                inputImageList = GameObject.Find("InputListLeft");
                //print(list);
                break;
            case 2:
                inputImageList = GameObject.Find("InputListRight");
                break;
        }
        if(inputImageList != null)
            hasImageList = true;
    }

    public void EnableMenuActions()
    {
        //controls.Gameplay.Disable();
        transform.Find("Mouse").gameObject.SetActive(true);
        //controls.Gameplay.LeftJoy.Enable();
        //controls.Gameplay.RightJoy.Enable();
        //controls.Gameplay.Start.Enable();
    }
    public void DisableMenuActions()
    {
        //controls.Gameplay.Enable();
        transform.Find("Mouse").gameObject.SetActive(false);
        //controls.Gameplay.LeftJoy.Disable();
        //controls.Gameplay.RightJoy.Disable();
    }
    private void MoveMouse()
    {
        Vector2 m = new Vector2(move.x, move.y) * Time.deltaTime;
        transform.Translate(m, Space.World);
    }
    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    private void SetActionImages()
    {
        if(hasActionList && GameManager.Instance.GameOn)
            for (int i = 0; i < 5; i++)
            {
                if(actionList.Count <= i)
                {
                    inputImageList.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = null;
                }
                else
                    inputImageList.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = GameManager.Instance.playerActionDictionary[ActionList[i]];
            }
    }

    public void NewGame()
    {
        history.Clear();
        HasActionList = false;
        actionList.Clear();
        LoveBar = 0;
        AbilityBar = 0;
    }
}