using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputInfo : MonoBehaviour
{
    private SpriteRenderer image;

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
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSprite(Sprite spt)
    {
        image.sprite = spt;
    }
}
