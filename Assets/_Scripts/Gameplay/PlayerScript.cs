using System.Collections;
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
    public GameObject inputImageList;
    private bool hasImageList = false;

    // Player
    private int player = 0;
    public int Player { get { return player; } set { player = value; } }
    Vector2 move;
    // PlayerControls
    //public PlayerControls controls { get; set; }
    
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

    // Animator 
    public Animator imageListAnimator { get; set; }

    // Sound
    public AudioClip correctInputSound;
    public AudioClip completeSound;
    public AudioClip errorSound;
    public AudioSource source;
    void Start()
    {
        //controls = new PlayerControls();
        history = new Stack<string>();
        actionList = new List<string>();
        GameManager.Instance.NotifyGM(gameObject);
    }

    private void Awake()
    {
        //InputControlBindings();
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            if (!hasImageList && GameManager.Instance.GameOn)
            {
                SetImageList(player);
            }
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
    //private void InputControlBindings()
    //{
    //    controls.Gameplay.X.performed += ctx => PlayerAction("X");              // X button
    //    controls.Gameplay.O.performed += ctx => PlayerAction("O");              // circle
    //    controls.Gameplay.S.performed += ctx => PlayerAction("S");              // square
    //    controls.Gameplay.T.performed += ctx => PlayerAction("T");              // triangle
    //    controls.Gameplay.Down.performed += ctx => PlayerAction("Down");        // Down D-PAD
    //    controls.Gameplay.Left.performed += ctx => PlayerAction("Left");        // Left D-PAD
    //    controls.Gameplay.Right.performed += ctx => PlayerAction("Right");      // Right D-PAD
    //    controls.Gameplay.Up.performed += ctx => PlayerAction("Up");            // UP D-PAD
    //    controls.Gameplay.Start.performed += ctx => PlayerAction("Start");      // Options/Start
    //    controls.Gameplay.RT.performed += ctx => PlayerAction("RT");            // Right trigger
    //    controls.Gameplay.RB.performed += ctx => PlayerAction("RB");            // Right Bumper
    //    controls.Gameplay.LT.performed += ctx => PlayerAction("LT");            // Left Trigger
    //    controls.Gameplay.LB.performed += ctx => PlayerAction("LB");            // Left Bumper
    //    controls.Gameplay.LeftJoy.performed += ctx => move = ctx.ReadValue<Vector2>() * 2f;  // LeftJoy
    //    controls.Gameplay.LeftJoy.canceled += ctx => move = Vector2.zero;
    //}

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
                if(abilityBar == 6)
                {
                    GameManager.Instance.AbilityCast(player);
                    AbilityBar = 0;
                }
                break;
            case "Start":
                if (!GameManager.Instance.GameOn && GameManager.Instance.MainMenuOn)
                    GameObject.Find("Canvas").GetComponent<MenuManager>().StartGame();
                else if (GameManager.Instance.GameOn && !GameManager.Instance.PauseMenuOn)
                    GameManager.Instance.Pause();
                else if (!GameManager.Instance.GameOn && GameManager.Instance.PauseMenuOn)
                    GameManager.Instance.Unpause();
                break;
            default:
                if (!GameManager.Instance.GameOn)
                    break;
                if (!hasActionList)
                    break;
                if (player == 1)
                {
                    if (GameManager.Instance.DialogueBoxP1.GetComponent<PlayerDialogueScript>().ready != true)
                        break;
                } else
                {
                    if (GameManager.Instance.DialogueBoxP2.GetComponent<PlayerDialogueScript>().ready != true)
                        break;
                }
                if (action == actionList[0])
                {
                    source.clip = correctInputSound;
                    source.Play();
                    switch(player)
                    {
                        case 1:
                            GameManager.Instance.DialogueBoxP1.GetComponent<PlayerDialogueScript>().CorrectInputWord();
                            break;
                        case 2:
                            GameManager.Instance.DialogueBoxP2.GetComponent<PlayerDialogueScript>().CorrectInputWord();
                            break;
                    }
                    history.Push(action);
                    actionList.RemoveAt(0);
                }
                else
                {
                    source.clip = errorSound;
                    source.Play();
                    if(history.Count != 0)
                    {
                        if(AbilityBar < 6)
                            AbilityBar++;
                        switch (player)
                        {
                            case 1:
                                GameManager.Instance.DialogueBoxP1.GetComponent<PlayerDialogueScript>().IncorrectInputWord();
                                break;
                            case 2:
                                GameManager.Instance.DialogueBoxP2.GetComponent<PlayerDialogueScript>().IncorrectInputWord();
                                break;
                        }
                        actionList.Insert(0, history.Pop());
                    }
                    imageListAnimator.SetTrigger("Shake");
                }
                break;
        }
    }

    private void CheckActionListComplete()
    {

        if(actionList.Count == 0)
        {
            source.clip = completeSound;
            source.Play(); 
            history.Clear(); 
            hasActionList = false;
            imageListAnimator.SetTrigger("Hide"); //Hides text right after it resets
            LoveBar++;
            if (LoveBar == 4)
            {
                GameManager.Instance.GameFinished = true;
            }
            GameManager.Instance.ActionListComplete(player, Message);
        }
    }

    public void SetImageList(int p)
    {
        switch (p)
        {
            case 1:
                inputImageList = GameObject.Find("InputListLeft");
                break;
            case 2:
                inputImageList = GameObject.Find("InputListRight");
                break;
        }
        if(inputImageList != null)
        {
            imageListAnimator = inputImageList.GetComponent<Animator>(); 
            hasImageList = true;
        }
            
    }

    public void EnableMenuActions()
    {
        transform.Find("Mouse").gameObject.SetActive(true);
    }
    public void DisableMenuActions()
    {
        transform.Find("Mouse").gameObject.SetActive(false);
    }
    private void MoveMouse()
    {
        Vector2 m = new Vector2(move.x, move.y) * Time.deltaTime;
        transform.Translate(m, Space.World);
    }
    private void OnEnable()
    {
        //controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        //controls.Gameplay.Disable();
    }

    private void SetActionImages()
    {
        if(hasActionList && GameManager.Instance.GameOn)
            for (int i = 0; i < 5; i++)
            {
                if (actionList.Count <= i)
                    inputImageList.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = null;
                else
                    inputImageList.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = GameManager.Instance.playerActionDictionary[ActionList[i]];
            }
    }

    public void NewGame()
    {
        hasImageList = false;
        history.Clear();
        HasActionList = false;
        actionList.Clear();
        LoveBar = 0;
        AbilityBar = 0;
    }
  
    public void OnX()
    {
        if(!GameManager.Instance.PauseMenuOn && !GameManager.Instance.GameFinished)
            PlayerAction("X");              // X button
    }

    public void OnO()
    {
        if (!GameManager.Instance.PauseMenuOn && !GameManager.Instance.GameFinished)
            PlayerAction("O");              
    }
    public void OnS()
    {
        if (!GameManager.Instance.PauseMenuOn && !GameManager.Instance.GameFinished)
            PlayerAction("S");              
    }
    public void OnT()
    {
        if (!GameManager.Instance.PauseMenuOn && !GameManager.Instance.GameFinished)
            PlayerAction("T");             
    }
    public void OnUp()
    {
        if (!GameManager.Instance.PauseMenuOn && !GameManager.Instance.GameFinished)
            PlayerAction("Up");             
    }
    public void OnDown()
    {
        if (!GameManager.Instance.PauseMenuOn && !GameManager.Instance.GameFinished)
            PlayerAction("Down");              
    }
    public void OnLeft()
    {
        if (!GameManager.Instance.PauseMenuOn && !GameManager.Instance.GameFinished)
            PlayerAction("Left");              
    }
    public void OnRight()
    {
        if (!GameManager.Instance.PauseMenuOn && !GameManager.Instance.GameFinished)
            PlayerAction("Right");             
    }
    public void OnStart()
    {
        PlayerAction("Start");
    }
    public void OnLB()
    {
        if (!GameManager.Instance.PauseMenuOn && !GameManager.Instance.GameFinished)
            PlayerAction("LB");
    }
    public void OnLT()
    {
        if (!GameManager.Instance.PauseMenuOn && !GameManager.Instance.GameFinished)
            PlayerAction("LT");
    }
    public void OnRB()
    {
        if (!GameManager.Instance.PauseMenuOn && !GameManager.Instance.GameFinished)
            PlayerAction("RB");
    }
    public void OnRT()
    {
        if (!GameManager.Instance.PauseMenuOn && !GameManager.Instance.GameFinished)
            PlayerAction("RT");
    }
    public void OnLeftJoy(InputValue iVal)
    {
        if (GameManager.Instance.PauseMenuOn || GameManager.Instance.MainMenuOn)
            move = iVal.Get<Vector2>() * 2f;
    }
}
