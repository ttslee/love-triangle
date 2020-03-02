using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    List<Sprite> playerIcons = new List<Sprite>();
    public Image playerLeft;
    public Image playerRight;
    private int indexLeft = 0;
    private int indexRight = 1;
    public GameObject dialogueBox;
    private TextMeshProUGUI textBox;

    void Start()
    {
        foreach (Sprite s in Resources.LoadAll<Sprite>("Player_Icon"))
            playerIcons.Add(s); //Loads playerIcons into List
        playerLeft.sprite = playerIcons[indexLeft];
        textBox = dialogueBox.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OpenControls()
    {
        OpenDialogueBox("You baffoon! Make the Controls yourself");
    }

    public void OpenCredits()
    {
        OpenDialogueBox("Damn Pepega! Do the Credits yourself");
    }

    private void OpenDialogueBox(string text)
    {
        textBox.text = text;
        dialogueBox.SetActive(true);
        StartCoroutine(dialogueBox.GetComponentInChildren<MenuDialogue>().ResetMessage(textBox));
    }

    public void ChangeLeftIcon(int dir)
    {
        if (dir == -1)
            playerLeft.sprite = playerIcons[(++indexLeft % playerIcons.Count)];
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

    public void CloseDialog()
    {
        dialogueBox.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
