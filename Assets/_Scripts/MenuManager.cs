using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    List<Sprite> playerIcons = new List<Sprite>();
    public Image playerLeft;
    public Image playerRight;
    private int indexLeft = 0;
    private int indexRight = 1;

    void Start()
    {
        foreach (Sprite s in Resources.LoadAll<Sprite>("Player_Icon"))
            playerIcons.Add(s); //Loads playerIcons into List
        playerLeft.sprite = playerIcons[indexLeft];
    }

    public void ChangeLeftIcon(int dir)
    {
        if (dir == -1)
            playerLeft.sprite = playerIcons[(++indexLeft%playerIcons.Count)];
        else
            playerLeft.sprite = playerIcons[(++indexLeft % playerIcons.Count)];
    }

    public void ChangeRightIcon(int dir)
    {
        if (dir == -1)
            playerRight.sprite = playerIcons[(++indexRight % playerIcons.Count)];
        else
            playerRight.sprite = playerIcons[(++indexRight % playerIcons.Count)];
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
