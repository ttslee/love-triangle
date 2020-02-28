using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager Instance = null;
    // PlayerControls

    // Player Info
    private GameObject player1;
    private GameObject player2;

    // Input Options
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


    private void Awake()
    {
        // If there is not already an instance of SoundManager, set it to this.
        if (Instance == null)
        {
            Instance = this;
        }
        //If an instance already exists, destroy whatever this object is to enforce the singleton.
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            QuitGame();
        print((player1 != null) ? "HAHAHAHA" : "YOYOYOYO");
    }

    public void NotifyGM(GameObject player)
    {
        if (player1 == null)
        {
            player1 = player;
            player.GetComponent<PlayerScript>().SetPlayer(1);
        }
        else
        {
            player2 = player;
            player.GetComponent<PlayerScript>().SetPlayer(2);
        }
            
    }
    
    public void AbilityCast(int player)
    {

    }
    public void ActionListComplete(int player)
    {

    }

    public void NewGame()
    {
        if(player1)
            player1.GetComponent<PlayerScript>().ActionList = GenerateInputList();
        if(player2)
            player2.GetComponent<PlayerScript>().ActionList = GenerateInputList();
    }
    private LinkedList<string> GenerateInputList()
    {
        LinkedList<string> temp = new LinkedList<string>();

        return temp;
    }
    public void QuitGame()
    {
        // save any game data here
        #if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
