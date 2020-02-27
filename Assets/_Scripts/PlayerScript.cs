using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerScript : MonoBehaviour
{
    public GameManager gm;
    // PlayerControls
    //PlayerControls controls;
    // Input stream and String Info
    private string currentString;
    private Stack<char> history;
    private List<string> actionList;
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
        //controls = new PlayerControls();
        print("IMA ALIZER");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
