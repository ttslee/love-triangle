using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class PlayerDialogueScript : MonoBehaviour
{
    private string alpha = "<alpha=#70>";
    private int len = 11;
    public int parent = 1;

    private int currentIndex = 0;
    public TextMeshProUGUI textMesh;

    public float textSpeed = 0.05f;
    private float timer = 0f;
    private bool ready = false;

    void Start()
    {
        if (GameObject.Find("GameManager") != null)
            GameManager.Instance.AssignDialogueBox(gameObject, parent);
    }

    public void DisplayText()
    {
        textMesh.maxVisibleCharacters = 0; //Makes text invis and Update function will begin.
        ready = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= textSpeed)
        {
            if (textMesh.text.Length > textMesh.maxVisibleCharacters)
            {
                textMesh.maxVisibleCharacters++;
            }
            timer = 0f;
        }

        if (textMesh.text.Length <= textMesh.maxVisibleCharacters)
        {
            //check for other player i suppose? then start the game
            ready = true;
        }
    }

    public void SetText(string txt)
    {
        currentIndex = 0;
        txt = txt.Insert(0, alpha);
        textMesh.text = txt;
    }
    public void CorrectInput()
    {
        textMesh.text = textMesh.text.Remove(currentIndex, len);
        if (textMesh.text[currentIndex] == ' ')
            currentIndex++;
        currentIndex++;
        textMesh.text = textMesh.text.Insert(currentIndex, alpha);
    }
    public void IncorrectInput()
    {
        textMesh.text = textMesh.text.Remove(currentIndex, len);
        if (textMesh.text[currentIndex-1] == ' ')
            currentIndex--;
        currentIndex--;
        textMesh.text = textMesh.text.Insert(currentIndex, alpha);
    }
}
