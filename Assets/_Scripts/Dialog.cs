using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog : MonoBehaviour

{
    #region Constant
    private string text1 = "task1: Say() function in Dialogue script. difficulty: a rank. need to be done asap but due end of next week task1: Say() function in Dialogue script. difficulty: a rank. need to be done asap but due end of next week";
    private string text2 = "ABC";
    int charsperline = 19;
    int totalline = 6;
    private float delay = 0.05f;
    #endregion

    #region variable
    private float timer = 0f;
    int topline = 0;
    int botline = 0;
    int typecount = 0;
    List<string> texts = new List<string>();
    TextMeshProUGUI Gtext;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Gtext = gameObject.GetComponent<TextMeshProUGUI>();
        Gtext.text = "";
        updateTexts(text1);
        for (int i = 0; i < texts.Count; i++)
        {
            Debug.Log(texts[i]);
        }
    }

    // Update is called once per frame

    void Update()
    {
        Say(texts);
    }
    #region function
    private void Say(List<string> texts) //type out texts with type writer effect.
    {
        timer += Time.deltaTime;
 
        if (timer >= delay)
        {
            if (botline<=texts.Count-1)
            {
                if (botline - topline >= totalline)
                {
                    topline++;
                    Gtext.text = "";
                    for (int i = topline; i <= botline-1; i++)
                    {
                        Gtext.text += texts[i];
                        Gtext.text += "\n";
                    }
                }
                else 
                {
                    if (typecount <= texts[botline].Length - 1)
                    {
                        Gtext.text += texts[botline][typecount];
                        typecount++;
                        timer = 0;
                    }
                    else
                    {
                        Gtext.text += "\n";
                        botline += 1;
                        typecount = 0;
                        timer = 0;
                    }
                }
                
            }
            
            
            

        }


    }
        
        


    private void updateTexts(string input)
    {
        int recordingInt = 0;
        string copyT = input;
        string line = "";

        while (copyT.Length > charsperline) 
        {
            for (int i = 0; i <= charsperline; i++)
            {
                if (copyT[i] != ' ')
                {
                    line += copyT[i];
                    recordingInt += 1;
                }
                else 
                {
                    if (CheckSpace(i, line.Length, copyT) == true)
                    {
                        line += copyT[i];
                        recordingInt += 1;
                    }

                    else 
                    {
                        break;
                    }
                }
            }
            texts.Add(line);
            copyT = copyT.PadLeft(recordingInt+1).Remove(0, recordingInt+1);
            recordingInt = 0;
            line = "";
        }
        line = copyT;
        texts.Add(line);
    }//Load text and seperate them in line and store in a list.

    private bool CheckSpace(int number, int Used,string text) 
    {
        int left_num = charsperline - Used;
        int count = 0;
        int current = number+1;
        while (text[current] != ' ') 
        {
            count += 1;
            current += 1;
        }

        if (count <= left_num)
        {
            return true;
        }

        else 
        {
            return false;
        }
    }//check if left spaces enough for next word.
    #endregion
}
