using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerScript : MonoBehaviour
{
    // Player
    private int player = 1;
    // PlayerControls
    PlayerControls controls;
    // Input stream and String Info
    private string currentString;
    private Stack<string> history;
    private LinkedList<string> actionList;
    public LinkedList<string> ActionList
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
        actionList = new LinkedList<string>();
        controls = new PlayerControls();
        GameManager.Instance.NotifyGM(gameObject);
        print("IMA ALIZER");
    }

    // Update is called once per frame
    void Update()
    {
        print(player);
    }

    public void SetPlayer(int i)
    {
        player = i;
    }
    private void InputControlBindings()
    {
        controls.Gameplay.X.performed += ctx => PlayerAction("X");      // X button
        controls.Gameplay.O.performed += ctx => PlayerAction("O");      // circle
        controls.Gameplay.S.performed += ctx => PlayerAction("S");      // square
        controls.Gameplay.T.performed += ctx => PlayerAction("T");      // triangle
        controls.Gameplay.Down.performed += ctx => PlayerAction("Down");
        controls.Gameplay.Left.performed += ctx => PlayerAction("Left");      // triangle
        controls.Gameplay.Right.performed += ctx => PlayerAction("Right");      // triangle
        controls.Gameplay.Up.performed += ctx => PlayerAction("UP");      // triangle
        controls.Gameplay.LeftJoy.performed += ctx => PlayerAction("LeftJoy");      // triangle
        controls.Gameplay.RightJoy.performed += ctx => PlayerAction("RightJoy");      // triangle
        controls.Gameplay.Start.performed += ctx => PlayerAction("Start");      // triangle
        controls.Gameplay.RT.performed += ctx => PlayerAction("RT");      // triangle
        controls.Gameplay.RB.performed += ctx => PlayerAction("RB");      // triangle
        controls.Gameplay.LT.performed += ctx => PlayerAction("LT");      // triangle
        controls.Gameplay.LB.performed += ctx => PlayerAction("LB");      // triangle
    }

    private void PlayerAction(string action)
    {
        switch(action)
        {
            case "RT":
            case "RB":
            case "LT":
            case "LB":
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
                if (action == actionList.First.Value)
                {
                    history.Push(action);
                    actionList.RemoveFirst();
                    CheckActionListComplete();
                }
                else
                {
                    actionList.AddFirst(history.Pop());
                }
                break;
        }
    }
    private void CheckActionListComplete()
    {
        if(actionList.Count == 0)
        {
            GameManager.Instance.ActionListComplete(player);
        }
    }
}
