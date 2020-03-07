using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaifuDialogue : MonoBehaviour
{
    #region Variables

    //Variables used in Say() Function
    TextMeshProUGUI tmpText; //TextMeshPro Text Component
    List<string> texts = new List<string>(); //Each line of texts
    private float timer = 0f;
    public float textSpeed = 0.05f;
    public int charsPerLine = 28; //Configurable WIDTH
    public int totalLines = 6; //Configurable HEIGHT
    private int topLine = 0;
    private int botLine = 0;
    private int typeCount = 0; //Counts how many Letter has been iterated so far
    //int faceExpression = 0; //For future UI effect

    #endregion

    //Delete this later on
    List<string> waifuResponses = new List<string>
    {
        "This is the test waifu message...",
        "This is the test waifu message...",
        "This is the test waifu message...",
        "This is the test waifu message...",
        "This is the test waifu message...",
        "This is the test waifu message...",
        "This is the test waifu message...",
        "This is the test waifu message...",
        "This is the test waifu message...",
        "This is the test waifu message...",
        "This is the test waifu message...",
        "This is the test waifu message...",
        "This is the test waifu message...",
    };
    void Start() 
    {
        //Initialization
        tmpText = gameObject.GetComponent<TextMeshProUGUI>();
        tmpText.text = "";

        //Example of Queuing Text. Delete this later on
    }

    private void Update()
    {
        Say(texts); //If she has something to say, she outright does it!
    }

    #region Completed Functions
    public void QueueText(string text) //Seperates given text into lines and stores in into our list
    {
        int recordingInt = 0;
        string textCopy = text; 
        string line = ""; 

        while (textCopy.Length > charsPerLine)
        {
            for (int i = 0; i <= charsPerLine; i++)
            {

                if (textCopy[recordingInt] == '<')
                {
                    List<string> list = CheckColorString(textCopy, recordingInt);
                    string a = list[0];
                    int b = int.Parse(list[1]);
                    List<string> listStrings = SeprateString(a);
                    for (int j = 0; j < listStrings.Count; j++)
                    {
                        line += listStrings[j]; 
                    }
                    recordingInt += b;
                    i += listStrings.Count;
                }
                else if (textCopy[recordingInt] != ' ')
                {
                    line += textCopy[recordingInt];
                    recordingInt += 1;
                }
                else
                {
                    if (CheckSpace(recordingInt, i, textCopy) == true)
                    {
                        line += textCopy[recordingInt];
                        recordingInt += 1;
                    }
                    else
                        break;
                }
            }
            texts.Add(line);
            textCopy = textCopy.PadLeft(recordingInt + 1).Remove(0, recordingInt + 1);
            recordingInt = 0;
            line = "";
        }
        line = textCopy;
        texts.Add(line);
    }

    #region QueueText Helper Functions
    private bool CheckSpace(int number, int used, string text) //Checks if there's enough spaces for the next word
    {
        int numLeft = charsPerLine - used;
        int count = 0;
        int current = number + 1;
        if (text[current] == '<')
        {
            string temp = CheckColorString(text, current)[0];
            count = SeprateString(temp).Count;
        }
        else 
        {
            while (text[current] != ' ')
            {
                count += 1;
                current += 1;
            }
            

        }
        if (count <= numLeft)
            return true;
        else
            return false;
    }
    private List<string> SeprateString(string a)
    {
        List<string> list = new List<string>();
        string begin = "";
        string target = "";
        string end = "";
        int leftcount = 0;
        int rightcount = 0;
        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] == '<')
            {
                if (leftcount == 0)
                {
                    begin += a[i];
                }
                if (leftcount == 1)
                {
                    end += a[i];
                }
                leftcount += 1;
            }
            else if (a[i] == '>')
            {
                if (rightcount == 0)
                {
                    begin += a[i];
                }
                if (rightcount == 1)
                {
                    end += a[i];
                }
                rightcount += 1;

            }
            else
            {
                if (leftcount == 1 && rightcount == 0)
                {
                    begin += a[i];
                }
                if (leftcount == 1 && rightcount == 1)
                {
                    target += a[i];
                }
                if (leftcount == 2 && rightcount == 1)
                {
                    end += a[i];
                }
            }
        }
        for (int i = 0; i < target.Length; i++)
        {
            list.Add(begin + target[i] + end);
        }
        return list;
    }
    #endregion
    private List<string> CheckColorString(string texts,int typecount) 
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
    private void Say(List<string> texts) //type out texts with type writer effect.
    {
        timer += Time.deltaTime;

        if (timer >= textSpeed)
        {
            if (botLine <= texts.Count - 1)
            {
                if (botLine - topLine >= totalLines)
                {
                    topLine++;
                    tmpText.text = "";
                    for (int i = topLine; i <= botLine - 1; i++)
                    {
                        tmpText.text += texts[i];
                        tmpText.text += "\n";
                    }
                }
                else
                {
                    if (typeCount <= texts[botLine].Length - 1)
                    {
                        if (texts[botLine][typeCount] == '<')
                        {
                            List<string> list = CheckColorString(texts[botLine],typeCount);
                            string a = list[0];
                            int b = int.Parse(list[1]);
                            tmpText.text += a;
                            typeCount += b;
                            timer = 0;
                        }
                        else 
                        {
                            tmpText.text += texts[botLine][typeCount];
                            typeCount++;
                            timer = 0;
                        }
                        
                    }
                    else
                    {
                        tmpText.text += "\n";
                        botLine += 1;
                        typeCount = 0;
                        timer = 0;
                    }
                }
            }
        }
    }

    #endregion

    public void Reply(int player, int msg) //Access this scripts list of replies according to int sent and say it
    {

    }

}
