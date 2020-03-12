using System.Collections;
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
    public bool GameFinished { get; set; } = false;
    //-------------------Player messages & data--------------------
    public Tuple<Sprite, string> player1Character { get; set; } = null;
    public Tuple<Sprite, string> player2Character { get; set; } = null;
    public GameObject DialogueBoxP1 { get; set; } = null;
    public GameObject DialogueBoxP2 { get; set; } = null;
    private int currentMessage = 0;

    List<string> playerMessages = new List<string>
    {
        "Here, let me help you with your bags, they look really heavy!",
        "I'm about to head out to grab some boba, would you like something?",
        "Your outfit looks really pretty today. Those colors suit you!",
        "Do you believe in love at first sight or should I walk by again?",
        "I got a free pastry at this cafe but I'm so full. Want it instead?",
        "Your dog is cute! I have one too, maybe they can have a playdate!",
        "Hey, I have the feeling that I'm probably the right one for you.",
        "We're taking the same 6AM next quarter! do you want to sit together?",
        "Want to watch a movie together sometime? This weekend, maybe?",
        "Have you eaten today? Wanna come and grab lunch with me? Jk! Unless?",
        "I've been reading this book lately and I think you'd like it a lot.",
        "I thought the sunset was the most beautiful thing, but then I saw you.",
        "You left your wallet at home? Don't worry I can spot you for lunch.",
        "Hey, my hand is getting kind of heavy. Do you mind holding it for me?",
        "I'm going to UTC's Trader Joes, do you want me to get you something?",
        "I stopped by that one coffee shop and thought I’d get you something.",
        "Do you like McDonald's? Cuz I'm McLovin you. PS, the food's on me.",
        "My aunt is getting married and I kinda need a date. Busy next month?",
        "Hey, I don't think I can finish these carne asada fries. Wanna share?",
        "I hope you get some good night's sleep. You've been working hard.",
        "I'd give up instant noodles if it meant I got to hang around you more.",
        "I'm really glad you came into my life. You're so important to me.",
        "I memorized your favorite boba: thai tea, 50% sweet, and less ice.",
        "Did you get a haircut? You look different and I can't figure out why.",
        "I think you understand me better than that guy over there does.",
        "Whoa, your hands are so cold! Let me warm them up for you with love!",
        "There's a new pop-up museum that opened downtown. Wanna go with me?",
        "Stella! Want to carpool with me to that concert you wanna go to?",
        "Hey, you look a little cold. Do you want to borrow my jacket?",
        "I have a confession to make. I think I have a huge crush on you."
        //"testing",
        //"testing1",
        //"testing2",
        //"testing3",
        //"testing4",
        //"testing5",
        //"testing6",
        //"testing7"
    };
    Dictionary<string, int> playerMessageDictionary = new Dictionary<string, int>
    {
        {"Here, let me help you with your bags, they look really heavy!", 0 },
        {"I'm about to head out to grab some boba, would you like something?", 1 },
        {"Your outfit looks really pretty today. Those colors suit you!", 2 },
        {"Do you believe in love at first sight or should I walk by again?", 3 },
        {"I got a free pastry at this cafe but I'm so full. Want it instead?", 4 },
        {"Your dog is cute! I have one too, maybe they can have a playdate!", 5 },
        {"Hey, I have the feeling that I'm probably the right one for you.", 6 },
        {"We're taking the same 6AM next quarter! do you want to sit together?", 7 },
        {"Want to watch a movie together sometime? This weekend, maybe?", 8 },
        {"Have you eaten today? Wanna come and grab lunch with me? Jk! Unless?", 9 },
        {"I've been reading this book lately and I think you'd like it a lot.", 10 },
        {"I thought the sunset was the most beautiful thing, but then I saw you.", 11 },
        {"You left your wallet at home? Don't worry I can spot you for lunch.", 12 },
        {"Hey, my hand is getting kind of heavy. Do you mind holding it for me?", 13 },
        {"I'm going to UTC's Trader Joes, do you want me to get you something?", 14 },
        {"I stopped by that one coffee shop and thought I’d get you something.", 15},
        {"Do you like McDonald's? Cuz I'm McLovin you. PS, the food's on me.", 16 },
        {"My aunt is getting married and I kinda need a date. Busy next month?", 17 },
        {"Hey, I don't think I can finish these carne asada fries. Wanna share?", 18 },
        {"I hope you get some good night's sleep. You've been working hard.", 19 },
        {"I'd give up instant noodles if it meant I got to hang around you more.", 20 },
        {"I'm really glad you came into my life. You're so important to me.", 21 },
        {"I memorized your favorite boba: thai tea, 50% sweet, and less ice.", 22 },
        {"Did you get a haircut? You look different and I can't figure out why.", 23 },
        {"I think you understand me better than that guy over there does.", 24 },
        {"Whoa, your hands are so cold! Let me warm them up for you with love!", 25 },
        {"There's a new pop-up museum that opened downtown. Wanna go with me?", 26 },
        {"Stella! Want to carpool with me to that concert you wanna go to?", 27 },
        {"Hey, you look a little cold. Do you want to borrow my jacket?", 28 },
        {"I have a confession to make. I think I have a huge crush on you.", 29 }
        //{"testing", 0 },
        //{"testing1", 1 },
        //{"testing2", 2 },
        //{"testing3", 3 },
        //{"testing4", 4 },
        //{"testing5", 5 },
        //{"testing6", 6 },
        //{"testing7", 7 }
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
        if (player2 && player == 1) //Checks if player exists before being able to use
            ShuffleList<string>(player2.GetComponent<PlayerScript>().ActionList);
        else if (player1 && player == 2)
            ShuffleList<string>(player1.GetComponent<PlayerScript>().ActionList);
    }

    private IEnumerator DisplayWait(float delay, int player)
    {
        yield return new WaitForSeconds(delay);
        SetActionList(player);
    }

    public void ActionListComplete(int player, string message)
    {
        GameObject.Find("WaifuText").GetComponent<WaifuDialogue>().Reply(player, playerMessageDictionary[message], 0);
        if (GameFinished)
            GameObject.Find("WaifuText").GetComponent<WaifuDialogue>().Reply(player, 0, 1);
        SetActionList(player);
        //StartCoroutine(DisplayWait(10f, player)); //Whenever the player finishes the text, the player now must wait 1sec before seeing the new input challenge
    }

    public void SetActionList(int p)
    {
        if (!GameFinished)
        {
            switch (p)
            {
                case 1:
                    player1.GetComponent<PlayerScript>().Message = playerMessages[currentMessage];
                    player1.GetComponent<PlayerScript>().ActionList = GenerateActionList(playerMessages[currentMessage], 1);
                    player1.GetComponent<PlayerScript>().HasActionList = true;
                    DialogueBoxP1.GetComponent<PlayerDialogueScript>().DisplayText(player1.GetComponent<PlayerScript>().imageListAnimator);
                    break;
                case 2:
                    player2.GetComponent<PlayerScript>().Message = playerMessages[currentMessage];
                    player2.GetComponent<PlayerScript>().ActionList = GenerateActionList(playerMessages[currentMessage], 2);
                    player2.GetComponent<PlayerScript>().HasActionList = true;
                    DialogueBoxP2.GetComponent<PlayerDialogueScript>().DisplayText(player2.GetComponent<PlayerScript>().imageListAnimator);
                    break;
            }
        }
    }
    private int FindSpaces(string msg)
    {
        int temp = 0;
        foreach (var c in msg)
            temp += (c == ' ') ? 1 : 0;
        return temp;
    }
    private int FindNumWords(string msg)
    {
        int i = 0;
        int count = 0;
        while(i < msg.Length)
        {
            if (msg[i] == ' ')
                count++;
            i++;
        }
        count++;
        return count;
    }
    public List<string> GenerateActionList(string msg, int p)
    {
        //int spaces = FindSpaces(msg);
        List<string> temp = new List<string>();
        int nWords = FindNumWords(msg);
        //for (int i = 0; i < msg.Length - spaces; i++)
        //{
        //    temp.Add(playerActionInputList[UnityEngine.Random.Range(0,8)]);
        //}
        for (int i = 0; i < nWords; i++)
        {
            temp.Add(playerActionInputList[UnityEngine.Random.Range(0, 8)]);
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
        SceneManager.LoadScene("Main");
        MainMenuOn = false;
        if (player1)
            player1.GetComponent<PlayerScript>().DisableMenuActions();
        if(player2)
            player2.GetComponent<PlayerScript>().DisableMenuActions();
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
        GameFinished = false;
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
