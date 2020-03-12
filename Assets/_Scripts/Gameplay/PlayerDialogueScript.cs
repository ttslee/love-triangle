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

    private float textSpeed = 0.05f;
    private float timer = 0f;
    public bool ready = false;
    private string upcomingText = "";
    private bool firstTime = true;
    private Animator imageListAnimator = null;

    void Start()
    {
        if (GameObject.Find("GameManager") != null)
            GameManager.Instance.AssignDialogueBox(gameObject, parent);
    }

    public void DisplayText(Animator imageList)
    {
        if (imageListAnimator == null)
            imageListAnimator = imageList;
        if (!firstTime)
            StartCoroutine(DisplayWait(3f));
        else
        {
            StartCoroutine(DisplayWait(.5f));
            firstTime = false;
        }
    }

    private IEnumerator DisplayWait(float delay)
    {
        yield return new WaitForSeconds(delay);
        textMesh.text = upcomingText;
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

        if (!ready && (textMesh.text.Length <= textMesh.maxVisibleCharacters))
        {
            //check for other player i suppose? then start the game
            if (imageListAnimator != null)
                imageListAnimator.SetTrigger("Start");
            ready = true;
        }
    }

    public void SetText(string txt)
    {
        currentIndex = 0;
        txt = txt.Insert(0, alpha);
        upcomingText = txt;
    }
    public void CorrectInputWord()
    {
        textMesh.text = textMesh.text.Remove(currentIndex, len);
        while (textMesh.text[currentIndex] != ' ' && currentIndex < textMesh.text.Length-1)
            currentIndex++;
        currentIndex++;
        textMesh.text = textMesh.text.Insert(currentIndex, alpha);
    }
    public void IncorrectInputWord()
    {
        textMesh.text = textMesh.text.Remove(currentIndex, len);
        if (currentIndex != 0)
            currentIndex--;
        while (currentIndex != 0 && textMesh.text[currentIndex - 1] != ' ')
            currentIndex--;
        //print(currentIndex);
        textMesh.text = textMesh.text.Insert(currentIndex, alpha);
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
