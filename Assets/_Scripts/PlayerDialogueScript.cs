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
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("GameManager") != null)
            GameManager.Instance.AssignDialogueBox(gameObject, parent);
    }

    public void SetText(string txt)
    {
        currentIndex = 0;
        txt = txt.Insert(0, alpha);
        textMesh.text = txt;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void CorrectInput()
    {
        textMesh.text = textMesh.text.Remove(currentIndex, len);
        currentIndex++;
        textMesh.text = textMesh.text.Insert(currentIndex, alpha);
    }
    public void IncorrectInput()
    {
        textMesh.text = textMesh.text.Remove(currentIndex, len);
        currentIndex--;
        textMesh.text = textMesh.text.Insert(currentIndex, alpha);
    }
}
