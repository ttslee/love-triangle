using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuDialogue : MonoBehaviour
{
    #region Variables

    //Variables used in Say() Function
    TextMeshPro tmpText; //TextMeshPro Text Component
    List<string> texts = new List<string>(); //Each line of texts
    private float timer = 0f;
    private bool start = false;
    public float textSpeed = 0.05f;
    private int typeCount = 0; //Counts how many Letter has been iterated so far
    //int faceExpression = 0; //For future UI effect

    #endregion

    IEnumerator Start()
    {
        //Initialization
        tmpText = gameObject.GetComponent<TextMeshPro>();
        tmpText.text = "";
        //Example of Queuing Text. Delete this later on
        texts.Add("Love Triangle"); //<color=#ff515d>Love</color> want to do this without the instant effect
        yield return new WaitForSeconds(1f);
        start = true;
    }

    private void Update()
    {
        Say(texts); //If she has something to say, she outright does it!
    }

    private List<string> CheckColorString(string texts, int typecount)
    {
        List<string> list = new List<string>();
        string temp = "";
        int countright = 0;
        for (int i = typecount; i < texts.Length; i++)
        {
            if (texts[i] == '>')
            {
                countright++;
                temp += texts[i];
                if (countright == 2)
                {
                    break;
                }
            }
            else
            {
                temp += texts[i];
            }
        }
        string a = temp.Length.ToString();
        list.Add(temp);
        list.Add(a);
        return list;
    }

    private void Say(List<string> texts) //Types out text with type writer effect.
    {
        timer += Time.deltaTime;

        if (start == true && timer >= textSpeed)
        {
            if (typeCount <= texts[0].Length - 1)
            {
                if (texts[0][typeCount] == '<')
                {
                    List<string> list = CheckColorString(texts[0], typeCount);
                    string a = list[0];
                    int b = int.Parse(list[1]);
                    tmpText.text += a;
                    typeCount += b;
                    timer = 0.0f;
                }
                else
                {
                    tmpText.text += texts[0][typeCount];
                    typeCount++;
                    timer = 0.0f;
                }

            }
        }
    }
}
