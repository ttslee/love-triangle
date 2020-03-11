using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaifuDialogue : MonoBehaviour
{
    #region Variables

    //Variables used in Say() Function
    TextMeshProUGUI tmpText; //TextMeshPro Text Component
    List<string> texts = new List<string>(); //Each line of texts
    private float timer = 0f;
    public float textSpeed = 0.5f;
    private bool paused = false; //Used for pausing in speech
    public int charsPerLine = 28; //Configurable WIDTH
    public int totalLines = 6; //Configurable HEIGHT
    private int topLine = 0; //The very top line, used for deleting
    private int botLine = 0; //Current line you are on
    private int typeCount = 0; //Counts how many Letters has been iterated so far per line
    private int totalChars = 0; //Total chars
    private int sayCount = 0; //Chars that have actually been said

    //Expressions
    public Image waifu;
    private List<Sprite> waifuSprites = new List<Sprite>(); 
    private bool canMad = true;

    //Sound Effect
    public AudioClip clip;
    private float soundTimer = .056f;
    #endregion

    List<string> waifuResponses = new List<string>
    {
        "2Oh, thank you so much! I've been carrying them around all day my arms were getting tired!",
        "1Really player? Wintermelon milk tea, 50% sweet, and less ice please!",
        "2Thanks! I wasn't too sure about this shirt but I feel better about it now!",
        "1That is definitely the most original thing I've heard in my entire life — tell me more.",
        "2Is that a danish? Yeah, I want it!",
        "3While we what? Have a… d-date?",
        "5You're not the first to tell me that.",
        "1Oh my gosh, yes please! I don't know anyone in that class!",
        "1Depends, what kind of movie were you thinking of? I'm a big fan of horror!",
        "1Gosh, I've been so busy I actually haven’t. I'm down.",
        "1Yeah? Maybe once you're done, you can lend it to me and we can talk about it.", //#10
        "5I can't believe you think I'm more beautiful than Miss Mother Nature herself.",
        "3You're such a lifesaver and I'm so stupid. Thank you!",
        "4As long as you don't mind returning the favor some other time.",
        "1Can you get me some cucumbers? The little persian ones!",
        "4You did? That's really thoughtful of you player!",
        "5It's not my first choice of fast food, but if it's free, why not!",
        "1I barely know my plans for next week, let alone next month, but we'll see.",
        "2Fun fact: I LOVE carne asada fries.",
        "4Thanks, it's been exhausting lately. I'm glad you understand player.",
        "1What does that have to do with anything? We can hang out and you don't have to lose ramen. Win-win.", //#20
        "2Thanks, I tend to have that effect on people.",
        "4I think you've found the way to my heart, honestly.",
        "2I did! You noticed player? It was only three inches off, but my hair feels way healthier now.",
        "5You really think so? There's still so much more for me to learn about you player.",
        "5It's the middle of July, you're so lame. But it's cute, I guess.",
        "1Is it one of those picture-taking ones? I'm so down for that.",
        "2Really? You're a lifesaver!",
        "1Yes please, it's forty-five degrees out right now!",
        "3As if you couldn't have been any more obvious!", //#29
    };

    Dictionary<string, string> Character_Color = new Dictionary<string, string>
    {
        {"Sanic", "<color=#3f47cc>"},
        {"Pika",  "<color=#fff624>"},
        {"Pepe", "<color=#1e4d25>" }
    };
    void Start()
    {
        //Initialization
        tmpText = gameObject.GetComponent<TextMeshProUGUI>();
        tmpText.text = "";
        //Init Expressions
        Sprite[] sprites = Resources.LoadAll<Sprite>("Waifu");
        foreach (Sprite s in sprites)
            waifuSprites.Add(s);
        //StartGame Dialogue
        QueueText("1Really?8 Wintermelon milk tea, 50 % sweet, and less ice please!9"); //After doing these ones, all the numbers that were previously hidden are now showing up.
        QueueText("1That is definitely the most original thing I've heard in my entire life — tell me more.");
    }

    private void Update()
    {
        if (!paused)
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
                if (recordingInt < textCopy.Length && System.Char.IsDigit(textCopy[recordingInt])) //added Expressions Code here. NEED HELP JING ;-;
                {
                    line += textCopy[recordingInt];
                    totalChars += 1;
                    recordingInt += 1;
                } //ended here. thats all i put in this function. I want the NUM to not affect the charsPerLine.
                else if (recordingInt < textCopy.Length && textCopy[recordingInt] == '<')
                {
                    List<string> list = CheckColorString(textCopy, recordingInt);
                    string a = list[0];
                    int b = int.Parse(list[1]);
                    List<string> listStrings = SeprateString(a);
                    for (int j = 0; j < listStrings.Count; j++)
                    {
                        line += listStrings[j];
                        totalChars += 1;
                    }
                    recordingInt += b;
                    i += listStrings.Count;
                }
                else if ( recordingInt < textCopy.Length && textCopy[recordingInt] != ' ')
                {
                    line += textCopy[recordingInt];
                    totalChars += 1;
                    recordingInt += 1;
                }
                else
                {
                    if (recordingInt < textCopy.Length && CheckSpace(recordingInt, i, textCopy) == true)
                    {
                        line += textCopy[recordingInt];
                        totalChars += 1;
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
        totalChars += textCopy.Length;
        texts.Add(line);
    }

    #region QueueText Helper Functions
    private bool CheckSpace(int number, int used, string text) //Checks if there's enough spaces for the next word
    {
        int numLeft = charsPerLine - used;
        int count = 0;
        int current = number + 1;
        if (current < text.Length && text[current] == '<')
        {
            string temp = CheckColorString(text, current)[0];
            count = SeprateString(temp).Count;
        }
        else 
        {
            while (current < text.Length && text[current] != ' ')
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

    private IEnumerator Pause(float delay)
    {
        paused = true;
        yield return new WaitForSeconds(delay);
        paused = false;
    }

    private void Say(List<string> texts) //type out texts with typewriter effect.
    {
        //Timer for typewriter effect
        timer += Time.deltaTime;
        soundTimer += Time.deltaTime;
        //Debug
        //Debug.Log(sayCount);
        //Debug.Log(totalChars);

        //Expression handler
        if (botLine <= texts.Count - 1 && typeCount <= texts[botLine].Length - 1 && System.Char.IsDigit(texts[botLine][typeCount])) //NEED HELP JING ;-; I think this part is okay though. Check QueueText
        {
            if (texts[botLine][typeCount] == '9') { //#9 = GAMESTART
                GameManager.Instance.GameOn = true;
            } else if (texts[botLine][typeCount] == '8') //#8 = INITIATE PAUSE DELAY
            {
                Debug.Log("??");
                StartCoroutine(Pause(1f)); //Waifu will hesitate for 1 seconds.
            } else
                waifu.sprite = waifuSprites[texts[botLine][typeCount] - 48]; //-48 bc its an ascii value character. Changes Face #0-5
            typeCount++; //Skip the number without adding it to text. We only need to check it to change expression.
            sayCount++;
        }

        //Expression Reset and Madness Enabler //You can ignore this
        if (sayCount == totalChars)
        {
            if (canMad != true) { //May change this later, but we'll see
                waifu.sprite = waifuSprites[0];
            }
            canMad = true;
        } else canMad = false;

        //Typewrite and color conversion
        if (timer >= textSpeed)
        {
            if(sayCount != totalChars && soundTimer >= .056f)
            {
                AudioSource.PlayClipAtPoint(clip, GameObject.Find("DialogueBox_Waifu").transform.position);
                soundTimer = 0f;
            }
                
            if (botLine <= texts.Count - 1)
            {
                if (botLine - topLine >= totalLines)
                {
                    topLine++;
                    tmpText.text = "";
                    
                    for (int i = topLine; i <= botLine - 1; i++)
                    {
                        int i2 = 0;
                        char[] tmpstring = new char[999];
                        for (int j = 0; j < texts[i].Length; j++)
                        {
                            if (!System.Char.IsDigit(texts[i][j]))
                            {
                                tmpstring[i2] = texts[i][j];
                                i2++;
                            }
                        }
                        
                        string a = new string(tmpstring, 0, i2);
                        tmpText.text += new string(tmpstring,0,i2);
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
                            sayCount += 1;
                        }
                        else 
                        {
                            tmpText.text += texts[botLine][typeCount];
                            typeCount++;
                            timer = 0;
                            sayCount += 1;
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

    public void Reply(int player, int msg) //Access this scripts list of replies according to int sent and say it
    {
        string Raw_Text = waifuResponses[msg];
        string Set_Color = "";
        if (player == 1)
        {
            Set_Color = Character_Color[GameManager.Instance.player1Character.Item2];
        }
        else
        {
            Set_Color = Character_Color[GameManager.Instance.player2Character.Item2];
        }
        string End_Color = "</color>";
        string Color_Name = "";
        string[] Array = Raw_Text.Split(' ');
        for (int i = 0; i < Array.Length; ++i)
        {
            if (i == Array.Length - 1)
            {
                if (Array[i] == "player" || Array[i] == "player!" || Array[i] == "player." || Array[i] == "player," || Array[i] == "player?")
                {
                    Color_Name += Set_Color;
                    Color_Name += Array[i];
                    Color_Name += End_Color;
                }
                else
                {
                    Color_Name += Array[i];
                }
            }
            else
            {
                if (Array[i] == "player" || Array[i] == "player!" || Array[i] == "player." || Array[i] == "player," || Array[i] == "player?")
                {
                    Color_Name += Set_Color;
                    Color_Name += Array[i];
                    Color_Name += End_Color;
                    Color_Name += " ";
                }
                else
                {
                    Color_Name += Array[i];
                    Color_Name += " ";
                }
            }
        }
        if (player == 1)
        {
            Color_Name = Color_Name.Replace("player", GameManager.Instance.player1Character.Item2);
        }
        else
        {
            Color_Name = Color_Name.Replace("player", GameManager.Instance.player2Character.Item2);
        }
        QueueText(Color_Name);
    }

    #endregion

}
