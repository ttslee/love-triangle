using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Font_Input subscripts. Order matters! It matches InputOptions list so that loading the dictionary works properly. 
// Playstation : { 3, 0, 2, 1, 17, 19, 16, 18 }
// XBOX        : { 6, 4, 7, 5, 17, 19, 16, 18 }
public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager Instance = null;
    // PlayerControls

    // Player Info
    private GameObject player1;
    private GameObject player2;


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

    //[System.Serializable]
    //public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
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

    public Dictionary<string, Sprite> playerActionDictionary;
    //
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
    public void Start()
    {
        
        playerActionDictionary = new Dictionary<string, Sprite>();
        int[] ps4Keys = { 0, 1, 2, 3, 11, 12, 13, 14 };
        for (int i = 0; i < ps4Keys.Length; i++)    
        {
            playerActionDictionary.Add(inputOptions[ps4Keys[i]], playerActionSprites[i]);
            print(playerActionDictionary[inputOptions[ps4Keys[i]]]);
        }
        
        
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
    public void StartGame()
    {
        SceneManager.LoadScene("Teo"); // *************************************
    }
}
