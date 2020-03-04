using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public bool PauseMenu = false;
    List<Sprite> playerIcons = new List<Sprite>();
    public Image playerLeft;
    public Image playerRight;
    private int indexLeft = 0;
    private int indexRight = 2;
    public GameObject dialogueBox;
    public GameObject buttons;
    private TextMeshProUGUI textBox;

    void Start()
    {
        if (!PauseMenu)
        {
            foreach (Sprite s in Resources.LoadAll<Sprite>("Player_Icon"))
                playerIcons.Add(s); //Loads playerIcons into List
            playerLeft.sprite = playerIcons[indexLeft];
        }
        textBox = dialogueBox.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void StartGame()
    {
        Debug.Log("Start!");
    }

    public void ResumeGame()
    {

    }

    public void ReturnToMenu()
    {

    }

    public void OpenControls()
    {

        OpenDialogueBox("You baffoon! Make the Controls yourself");
    }

    public void OpenCredits()
    {
        OpenDialogueBox("Teo Lee - Programming, Design, Sound\nJohnny Ngo - Programming, Design, Art\nJing Hu - Programming, Design\n" +
            "Nam-giao Nguyen - Programming, Design\nAdrienne Caparaz - Design");
    }

    private void OpenDialogueBox(string text)
    {
        textBox.text = text;
        dialogueBox.SetActive(true);
        buttons.SetActive(false);
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
        buttons.SetActive(true);
        dialogueBox.SetActive(false);
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
}
