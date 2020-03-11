using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputInfo : MonoBehaviour
{
    public SpriteRenderer image;

    private string action;
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

    public void SetSprite(Sprite spt)
    {
        image.sprite = spt;
    }
}
