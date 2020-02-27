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
}
