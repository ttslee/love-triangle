using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog : MonoBehaviour

{

    private string text1 = "task1: Say() function in Dialogue script. difficulty: a rank. need to be done asap but due end of next week";
    private string text2 = "ABC";
    int charsperline = 28;
    int totalline = 10;
    private float delay = 0.02f;

    private float timer = 0f;
    int typing = 0;
    int linecount = 0;
    int typed = 0;
    TextMeshProUGUI Gtext;

    // Start is called before the first frame update
    void Start()
    {
        Gtext = gameObject.GetComponent<TextMeshProUGUI>();
        Gtext.text = "";
    }

    // Update is called once per frame

    void Update()
    {
        Say(text1);
    }

    private void Say(string Text)
    {
        int textlength = Text.Length;

        timer += Time.deltaTime;
        if (typing <= textlength - 1)
        {
            if (timer >= delay)
            {

                Gtext.text += Text[typing];
                typed += 1;



                if (Text[typing] == ' ')
                {
                    if (WordRemind(Text, typed, typing) == false)
                    {
                        Gtext.text += "\n";
                        timer = 0;
                        typing++;
                        typed = 0;
                    }
                    else
                    {
                        typing++;
                        timer = 0;
                        if (typed == charsperline)
                        {
                            Gtext.text += "\n";
                            typed = 0;
                        }
                    }
                }
                else
                {
                    typing++;
                    timer = 0;
                    if (typed == charsperline)
                    {
                        Gtext.text += "\n";
                        typed = 0;
                    }
                }
            }

        }
    }
    private bool WordRemind(string text, int typed_line, int typing)
    {
        int char_remained = charsperline - typed_line;
        int checking_int = typing + 1;
        int count = 0;
        while (checking_int < text.Length - 1)
        {
            if (text[checking_int] != ' ')
            {
                count += 1;
                checking_int += 1;
            }
            else
            {
                break;
            }

        }

        if (count <= char_remained)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
