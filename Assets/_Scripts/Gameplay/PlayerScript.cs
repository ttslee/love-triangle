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

    // Animator 
    public Animator imageListAnimator { get; set; }

    // Sound
    public AudioClip correctInputSound;
    public AudioClip completeSound;
    public AudioSource source;
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
        controls.Gameplay.LeftJoy.performed += ctx => move = ctx.ReadValue<Vector2>() * 2f;  // LeftJoy
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
                if(abilityBar == 6)
                {
                    GameManager.Instance.AbilityCast(player);
                    AbilityBar = 0;
                }
                break;
            case "Start":
                if (!GameManager.Instance.GameOn && GameManager.Instance.MainMenuOn)
                    GameManager.Instance.StartGame(); 
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
                if (action == actionList[0])
                {
                    source.clip = correctInputSound;
                    source.Play();
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
                        if(AbilityBar < 6)
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
                    imageListAnimator.SetTrigger("Shake");
                }
                break;
        }
    }

    private void CheckActionListComplete()
    {

        if(actionList.Count == 0)
        {
            imageListAnimator.SetTrigger("Start"); //Added this for now;
            source.clip = completeSound;
            source.Play();
            history.Clear();
            hasActionList = false;
            LoveBar++;
            if (LoveBar == 4)
            {
                GameManager.Instance.player1.GetComponent<PlayerScript>().GameFinished();
                GameManager.Instance.player2.GetComponent<PlayerScript>().GameFinished();
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
            imageListAnimator.SetTrigger("Start");
            hasImageList = true;
        }
            
    }

    public void GameFinished()
    {
        controls.Gameplay.X.Disable();
        controls.Gameplay.O.Disable();
        controls.Gameplay.S.Disable();
        controls.Gameplay.T.Disable();
        controls.Gameplay.Down.Disable();
        controls.Gameplay.Left.Disable();
        controls.Gameplay.Right.Disable();
        controls.Gameplay.Up.Disable();
        controls.Gameplay.RT.Disable();
        controls.Gameplay.RB.Disable();
        controls.Gameplay.LT.Disable();
        controls.Gameplay.LB.Disable();
    }

    public void EnableMenuActions()
    {
        controls.Gameplay.X.Disable();
        controls.Gameplay.O.Disable();
        controls.Gameplay.S.Disable();
        controls.Gameplay.T.Disable();
        controls.Gameplay.Down.Disable();
        controls.Gameplay.Left.Disable();
        controls.Gameplay.Right.Disable();
        controls.Gameplay.Up.Disable();
        controls.Gameplay.RT.Disable();
        controls.Gameplay.RB.Disable();
        controls.Gameplay.LT.Disable();
        controls.Gameplay.LB.Disable();
        transform.Find("Mouse").gameObject.SetActive(true);
    }
    public void DisableMenuActions()
    {
        transform.Find("Mouse").gameObject.SetActive(false);
        controls.Gameplay.X.Enable();
        controls.Gameplay.O.Enable();
        controls.Gameplay.S.Enable();
        controls.Gameplay.T.Enable();
        controls.Gameplay.Down.Enable();
        controls.Gameplay.Left.Enable();
        controls.Gameplay.Right.Enable();
        controls.Gameplay.Up.Enable();
        controls.Gameplay.RT.Enable();
        controls.Gameplay.RB.Enable();
        controls.Gameplay.LT.Enable();
        controls.Gameplay.LB.Enable();
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
}
