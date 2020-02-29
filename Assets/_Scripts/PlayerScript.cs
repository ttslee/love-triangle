using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerScript : MonoBehaviour
{
    // GAME ON BOOL
    public bool gameOn = false;
    // InputImagesList
    public GameObject inputImageList;
    private bool hasImageList = false;

    // Player
    private int player = 0;

    // PlayerControls
    PlayerControls controls;

    // Input stream and String Info
    private bool correctInput = false;
    private string currentString;
    private Stack<string> history;

    private bool hasActionList = false;
    public bool HasActionList
    {
        get
        {
            return hasActionList;
        }
        set
        {
            hasActionList = value;
        }
    }
    private List<string> actionList;
    public List<string> ActionList
    {
        get
        {
            return actionList;
        }
        set
        {
            actionList = value;
        }
    }
    // Ability and Affection Bar Info
    private float affectionBar = 0f;
    private float abilityBar = 0f;
    public float AffectionBar
    {
        get
        {
            return affectionBar;
        }
        set
        {
            affectionBar = value;
        }
    }

    public float AbilityBar
    { 
        get
        {
            return abilityBar;
        }
        set
        {
            abilityBar = value;
        }
    }
    // Start is called before the first frame update
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
    }
    // Update is called once per frame
    void Update()
    {
        if (hasActionList)
            SetActionImages();
        keyBoardInput();
        if (!hasImageList)
            CheckForInputImageList();
    }
    private void keyBoardInput()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            GameManager.Instance.NewGame();
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
        controls.Gameplay.Up.performed += ctx => PlayerAction("UP");            // UP D-PAD
        controls.Gameplay.LeftJoy.performed += ctx => PlayerAction("LeftJoy");  // LeftJoy
        controls.Gameplay.RightJoy.performed += ctx => PlayerAction("RightJoy");// RightJoy
        controls.Gameplay.Start.performed += ctx => PlayerAction("Start");      // Options/Start
        controls.Gameplay.RT.performed += ctx => PlayerAction("RT");            // Right trigger
        controls.Gameplay.RB.performed += ctx => PlayerAction("RB");            // Right Bumper
        controls.Gameplay.LT.performed += ctx => PlayerAction("LT");            // Left Trigger
        controls.Gameplay.LB.performed += ctx => PlayerAction("LB");            // Left Bumper
    }

    private void PlayerAction(string action)
    {
        switch(action)
        {
            case "RT":
            case "RB":
            case "LT":
            case "LB":
                print("ability");
                if(abilityBar >= 100f)
                    GameManager.Instance.AbilityCast(player);
                break;
            case "Start":
                SceneManager.LoadScene("Teo");  // *********************************************************************
                break;
            case "LeftJoy":
            case "RightJoy":
                break;
            default:
                if (!hasActionList)
                    break;
                if (action == actionList[0])
                {
                    correctInput = true;
                    history.Push(action);
                    actionList.RemoveAt(0);
                    CheckActionListComplete();
                    if (hasActionList)
                        SetActionImages();
                }
                else
                {
                    actionList.Insert(0, history.Pop());
                }
                break;
        }
    }
    private void CheckActionListComplete()
    {
        if(actionList.Count == 0)
        {
            GameManager.Instance.ActionListComplete(player);
            hasActionList = false;
        }
    }

    private void CheckForInputImageList()
    {
        GameObject list = null; 
        switch (player)
        {
            case 1:
                list = GameObject.Find("InputListLeft");
                //print(list);
                break;
            case 2:
                list = GameObject.Find("InputListRight");
                break;
        }
        if (list)
            inputImageList = list;
        hasImageList = true;
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
        for (int i = 0; i < 5; i++)
        {
            //print(inputImageList.transform.GetChild(i));
            //inputImageList.transform.GetChild(i).gameObject.GetComponent<InputInfo>().SetSprite(GameManager.Instance.playerActionDictionary[ActionList[i]]);
            inputImageList.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = GameManager.Instance.playerActionDictionary[ActionList[i]];
        }
    }
}
