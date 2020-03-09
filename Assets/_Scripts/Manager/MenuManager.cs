using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class MenuManager : MonoBehaviour
{
    public bool PauseMenu = false;
    List<Tuple<Sprite, string>> playerIcons = new List<Tuple<Sprite, string>>();
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
            Sprite[] s = Resources.LoadAll<Sprite>("Player_Icon");
            playerIcons.Add(new Tuple<Sprite,string>(s[0],"Pika"));
            playerIcons.Add(new Tuple<Sprite, string>(s[1], "Sanic"));
            playerIcons.Add(new Tuple<Sprite, string>(s[2], "Pepe"));
            playerLeft.sprite = playerIcons[indexLeft].Item1;
            playerRight.sprite = playerIcons[indexRight].Item1;
        }
        textBox = dialogueBox.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void StartGame()
    {
        GameManager.Instance.player1Character = playerIcons[(indexLeft % playerIcons.Count)];
        GameManager.Instance.player2Character = playerIcons[(indexRight % playerIcons.Count)];
        GameManager.Instance.StartGame();
    }

    public void ResumeGame()
    {
        GameManager.Instance.Unpause();
    }

    public void ReturnToMenu()
    {
        GameManager.Instance.NewGame();
    }

    public void OpenControls()
    {

        OpenDialogueBox("Press the input that shows up on your side! When your blue bar fills up, Press any of the back buttons to use your ability! " +
            "Restart anytime by pressing Start and returning to the main menu! Have Fun!");
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
            playerLeft.sprite = playerIcons[(++indexLeft % playerIcons.Count)].Item1;
        else
            playerLeft.sprite = playerIcons[(++indexLeft % playerIcons.Count)].Item1;
    }

    public void ChangeRightIcon(int dir)
    {
        if (dir == -1)
            playerRight.sprite = playerIcons[(++indexRight % playerIcons.Count)].Item1;
        else
            playerRight.sprite = playerIcons[(++indexRight % playerIcons.Count)].Item1;
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
