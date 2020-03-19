using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class PlayerScript : MonoBehaviour
{
    // Mouse Object Info
    public GameObject mouse;
    public Color32 mouseColor1;
    public Color32 mouseColor2;
    private EventSystem eventSystem;

    // Message Info
    public string Message { get; set; }
    // InputImagesList
    public GameObject inputImageList;
    private bool hasImageList = false;

    // Player
    private int player = 0;
    public int Player { get { return player; } set { player = value; } }

    Vector2 move;

    // Input stream and String Info
    int gamepadType = 0;

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
    public AudioClip winner;
    public AudioSource source;
    void Start()
    {
        history = new Stack<string>();
        actionList = new List<string>();
        if (gameObject.GetComponent<PlayerInput>().currentControlScheme == "Xbox")
            gamepadType = 1;
        GameManager.Instance.NotifyGM(gameObject);
    }

    private void Awake()
    {
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
        MoveMouse();
    }

    public void SetPlayer(int i)
    {
        player = i;
        if (i == 1)
            eventSystem = GameObject.FindGameObjectWithTag("EventP1").GetComponent<EventSystem>();
        else
            eventSystem = GameObject.FindGameObjectWithTag("EventP2").GetComponent<EventSystem>();
    }

    public IEnumerator abilityChange(int amt)
    {
        if (amt < abilityBar)
        {
            for (int i = abilityBar; i >= amt; i--)
            {
                abilityBar = i;
                source.clip = errorSound;
                source.Play();
                GameManager.Instance.AbilityCast(player);
                yield return new WaitForSeconds(.1f);
            }
        } else if (amt > abilityBar && amt < 7)
        {
            for (int i = abilityBar; i <= amt; i++)
            {
                abilityBar = i;
                yield return new WaitForSeconds(.1f);
            }
        }
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
                    StartCoroutine(abilityChange(0));
                }
                break;
            case "Start":
                if (!GameManager.Instance.GameOn && GameManager.Instance.MainMenuOn)
                    GameObject.FindGameObjectWithTag("Canvas").GetComponent<MenuManager>().StartGame();
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
                        //if(AbilityBar < 6)
                            //AbilityBar++;
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
            imageListAnimator.SetTrigger("Hide"); //Hides inputlist right after it resets
            GameManager.Instance.AbilityCheck(player, abilityBar, loveBar);
            LoveBar++;
            if (LoveBar == 4)
            {
                GameManager.Instance.GameFinished = true;
                source.clip = winner;
                source.Play();
            }
            GameManager.Instance.ActionListComplete(player, Message);
        }
    }

    public void SetImageList(int p)
    {
        switch (p)
        {
            case 1:
                inputImageList = GameObject.FindGameObjectWithTag("InputListLeft");
                break;
            case 2:
                inputImageList = GameObject.FindGameObjectWithTag("InputListRight");
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
                {
                    if (gamepadType == 0)
                        inputImageList.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = GameManager.Instance.playerActionDictionary[ActionList[i]];
                    else
                        inputImageList.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = GameManager.Instance.playerActionDictionary2[ActionList[i]];
                }
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
        if (GameManager.Instance.PauseMenuOn || GameManager.Instance.MainMenuOn)
        {
            if (eventSystem.currentSelectedGameObject != null)
                ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, null, ExecuteEvents.submitHandler);
        }
        else if (!GameManager.Instance.PauseMenuOn && !GameManager.Instance.GameFinished)
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

    private IEnumerator DelayedLeftJoy(InputValue iVal)
    {
        yield return new WaitForSeconds(1.5f);
        if (!GameManager.Instance.PauseMenuOn || !GameManager.Instance.MainMenuOn)
            move = Vector2.zero;
    }

    public void OnLeftJoy(InputValue iVal)
    {
        if (GameManager.Instance.PauseMenuOn || GameManager.Instance.MainMenuOn)
            move = iVal.Get<Vector2>() * 2f;
        else
            move = Vector2.zero;
        StopAllCoroutines();
        StartCoroutine(DelayedLeftJoy(iVal));

    }
}
