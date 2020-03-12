using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MouseScript : MonoBehaviour
{
    public GameObject parent;

    public int Player
    {
        get
        {
            return parent.GetComponent<PlayerScript>().Player;
        }
        set
        {
            Player = value;
        }
    }

    public void Start()
    {
        GetComponent<SpriteRenderer>().color = (Player == 1) ? parent.GetComponent<PlayerScript>().mouseColor1 : parent.GetComponent<PlayerScript>().mouseColor2;
    }
}
