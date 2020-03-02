using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Font_Input subscripts. Order matters! It matches InputOptions list so that loading the dictionary works properly. 
// Playstation : { 3, 0, 2, 1, 17, 19, 16, 18 }
// XBOX        : { 6, 4, 7, 5, 17, 19, 16, 18 }
public class GameManager : MonoBehaviour
{
    // GAME ON BOOL
    public bool GameOn { get; set; }
    public bool MainMenuOn { get; set; } = true;
    public bool PauseMenuOn { get; set; } = false;
    // player and waifu messages && data
    public GameObject DialogueBoxP1 { get; set; } = null;
    public GameObject DialogueBoxP2 { get; set; } = null;
    private int currentMessage = 0;
    List<string> playerMessages = new List<string>
    {
        "This is the test message...",
        "This is the test message...",
        "This is the test message...",
        "This is the test message...",
        "This is the test message...",
        "This is the test message...",
        "This is the test message...",
        "This is the test message...",
        "This is the test message...",
        "This is the test message...",
        "This is the test message...",
        "This is the test message...",
        "This is the test message...",
    }; 
    // Singleton
    public static GameManager Instance = null;
    // PlayerControls

    // Player Info
    //private bool hasPlayer1 = false;
    //private bool hasPlayer2 = false;
    private GameObject player1 = null;
    private GameObject player2 = null;


    // Input Options
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

    //[System.Serializable] public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    //{
    //    [SerializeField]
    //    private List<TKey> keys = new List<TKey>();

    //    [SerializeField]
    //    private List<TValue> values = new List<TValue>();

    //    // save the dictionary to lists
    //    public void OnBeforeSerialize()
    //    {
    //        keys.Clear();
    //        values.Clear();
    //        foreach (KeyValuePair<TKey, TValue> pair in this)
    //        {
    //            keys.Add(pair.Key);
    //            values.Add(pair.Value);
    //        }
    //    }

    //    // load dictionary from lists
    //    public void OnAfterDeserialize()
    //    {
    //        this.Clear();

    //        if (keys.Count != values.Count)
    //            throw new System.Exception(string.Format("there are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable."));

    //        for (int i = 0; i < keys.Count; i++)
    //            this.Add(keys[i], values[i]);
    //    }
    //}
    //[System.Serializable]
    //public class PlayerActionDictionary : SerializableDictionary<string, Sprite> { };

    //public PlayerActionDictionary pActionDict;


    // Prior Sprite loading attempt.... Did not work
    // Turns out I shouldnt have tried this. It was fked... It was a nice idea, but I over complicated the problem.

    //string[] ps4Val = { "_3", "_0", "_2", "_1", "_17", "_19", "_16", "_18" };
    //for (int i = 0; i < ps4Keys.Length; i++)
    //{
    //    string temp = "Font_Inputs" + ps4Val[i];
    //    Texture2D texture = Resources.Load<Texture2D>(temp);
    //    print(texture);
    //    Rect rec = new Rect(0, 0, 8, 9);

    //    Sprite spt = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 100);
    //    playerActionDictionary.Add(inputOptions[ps4Keys[i]], spt);
    //}

    public Dictionary<string, Sprite> playerActionDictionary;
    public void Start()
    {

        // Setup the Dictionary <Input, Sprite>
        playerActionDictionary = new Dictionary<string, Sprite>();
        for (int i = 0; i < 8; i++)    
        {
            playerActionDictionary.Add(playerActionInputList[i], playerActionSprites[i]);
        }
        ShuffleList<string>(playerMessages);
    }
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
        if (Input.GetKeyDown(KeyCode.Return))
            StartGame();
        //print((player1 != null) ? "HAHAHAHA" : "YOYOYOYO");
    }
    public static void ShuffleList<T>(List<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }

    // PLAYER MANAGEMENT-------------------------------------------------
    public void NotifyGM(GameObject player)
    {
        if (player1 == null)
        {
            //hasPlayer1 = true;
            player1 = player;
            player1.name = "Player1";
            player.GetComponent<PlayerScript>().SetPlayer(1);
        }
        else if(player2 == null)
        {
            player2.name = "Player2";
            //hasPlayer2 = true;
            player2 = player;
            player.GetComponent<PlayerScript>().SetPlayer(2);
        }
    }
    
    public void AbilityCast(int player)
    {

    }
    public void ActionListComplete(int player)
    {
        SetActionList(player);
    }

    public void SetActionList(int p)
    {
        switch(p)
        {
            case 1:
                player1.GetComponent<PlayerScript>().MessageIndex = currentMessage;
                player1.GetComponent<PlayerScript>().ActionList = GenerateActionList(playerMessages[currentMessage], 1);
                player1.GetComponent<PlayerScript>().HasActionList = true;
                break;
            case 2:
                player2.GetComponent<PlayerScript>().MessageIndex = currentMessage;
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
        currentMessage++;
        List<string> temp = new List<string>();
        for (int i = 0; i < msg.Length - spaces; i++)
        {
            temp.Add(playerActionInputList[Random.Range(0,8)]);
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
        return temp;
    }
    // --------------------------------------------------------------------

    //Player Dialogue Management
    public void AssignDialogueBox(GameObject g, int p)
    {
        switch(p)
        {
            case 1:
                DialogueBoxP1 = g;
                break;
            case 2:
                DialogueBoxP2 = g;
                break;
        }
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
    IEnumerator LoadAsync()
    {
        // Loads scene in background
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Teo");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
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
}
