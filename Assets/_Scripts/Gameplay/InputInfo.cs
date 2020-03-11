using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputInfo : MonoBehaviour
{
    public SpriteRenderer image;

    private string action;

    private void Start()
    {
        image.color = new Color(1, 1, 1, 0);
    }

    public string Action
    {
        get
        {
            return action;
        }
        set
        {
            action = value;
        }
    }
}
