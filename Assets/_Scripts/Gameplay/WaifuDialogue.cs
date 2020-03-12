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
    private float textSpeed = 0.05f;
    private bool paused = false; //Used for pausing in speech
    public int charsPerLine = 21; //Configurable WIDTH
    public int totalLines = 5; //Configurable HEIGHT
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

    //End with Particles
    public GameObject particles;

    #endregion

    List<string> waifuResponses = new List<string>
    {
        "2Thank you!81 My arms are so tired from carrying them around all day.", //0 //1 Pause
        "3Really player?81 Thai milk tea, half sweet, and less ice please!", //1 //1 Pause
        "2Thanks! 1I wasn't too sure about this shirt but I feel better about it now!", //2 //Takes about 4seconds to say //0 Pause
        "1That is definitely the most original thing I've heard in my entire life, tell me more.", //3 //0 Pause
        "2Is that a brioche?!8 Yeah, I definitely want it if you don't mind!", //4 //1 Pause
        "3While we what?8 Have a d-8date?", //5 //2 Pause
        "5You're not the first to tell me that. That guy over there said the same thing.", //6 //0 Pause
        "1Oh my gosh, yes please! I don't know anyone in that class!", //7 //0 Pause
        "1Depends,8 what movie were you thinking of? 2I'm a big horror fan!", //8 //1 Pause
        "1Gosh, I've been so busy I actually haven’t. I'm down for some California Gogi!", //9
        "1Yeah?84 If you lend it to me, maybe we can talk about it.", //#10 //1 Pause
        "5I can't believe you think I'm more beautiful than Miss Mother Nature herself.", //11 //0 Pause
        "3You are such a lifesaver and I'm so stupid.8 Thank you so much!", //12 //1 Pause
        "4As long as you don't mind returning the favor some other time.", //13 //0 Pause
        "1Can you get me that tiny cactus sitting outside?8 3It's so cute!", //14 //1 Pause
        "4Did you really?8 Wow, that's really thoughtful of you player!", //15 //1 Pause
        "5It's not really my first choice of fast food...2 but if it's free, why not!", //16 //0 Pause
        "1I barely know my plans for next week, let alone next month, but we'll see.", //17 //0 Pause
        "2Fun fact: I LOVE carne asada fries. I'd eat it for breakfast, lunch, and dinner.", //18 //0 Pause
        "4Thanks, it's been exhausting lately.8 I'm glad you understand player.", //19 //1 Pause
        "5What does that have to do with anything?8 1We can do both though! Win win.", //#20 //1 Pause
        "2Haha thanks!8 I guess I have that effect on people.", //21 //1 Pause
        "4Honestly,8 I think you've just found the way to my heart.", //22 //1 Pause
        "2I did! You noticed player?81 My hair feels way healthier now.", //23 //1 Pause
        "5You really think so?8 There's still so much to learn about you player.", //24 //1 Pause
        "5It's the middle of July, you're so lame.8 2But it's cute, I guess.", //25 //1 Pause
        "1Is it one of those picture-taking ones? 2I'm so down for that.", //26 //0 Pause
        "1Really?82 You're a lifesaver! BTS concert here we come!", //27 //1 Pause
        "1Please!8 It's like forty-five degrees out right now!", //28 //1 Pause
        "3As if you couldn't have been any more obvious player!", //#29 //0 Pause
    };

    List<string> waifuStartDialogue = new List<string>
    {
        "2Hey there!8 1Looks like the three of us are going to be partners for this game project. My name's Stella!9",
        "Sorry to bother you guys, but Kurt says you guys are looking for another member for your group.8 I'm Stella. Nice to meet you!",
        "1Excuse me!8 2Can I interest you two in buying some two dollar boba to support my kpop club? Ah,8 thank you for buying!9",
        "2Hey!8 1I'm a transfer student and it's my first day here. The teacher said you guys can show me around after class?9",
        "3Ah!8 2Sorry I bumped into you... Oh hey, you guys have that class too! Want to walk together with me?9",
    };

    List<string> waifuFinishDialogue = new List<string>
    {
        "3.8.8.8 I think I r-8really like you player! Do you want to go out with me?7",
        "4.8.8.8 player, 8I think you're a really amazing person. Do you want to take our relationship a little further?7",
        "4.8.8.8 I really love spending time with you player. Would you be mine?7",
        "3.8.8.8 I-8I think I like you. Will you please go out with me!7",
        "3.8.8.8 I like you player. Whether you're a hedgehod, a pokemon, or a frog, I don't care anymore.",
        "4.8.8.8 Hey player, 8I'd like to try it once. That wicked love.",
        "4.8.8.8 player, 8I'm sorry for looking at that other guy over there.8 Should I apologize.. or should I just kiss you right now?,"
        "3.8.8.8 player, 8I'm not going to lie.8 After hearing you talk four sentences, I fell in love with you."

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
        QueueText(waifuStartDialogue[Random.Range(0,waifuStartDialogue.Count)]);
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
                StartCoroutine(Pause(.8f)); //Waifu will hesitate for 1 seconds.
            }
            else if (texts[botLine][typeCount] == '7') //#7 = GAME FINISHED FUNCTION
            {
                particles.SetActive(true);
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
                SoundManager.Instance.Play(clip);
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
                        string texta = dealingwithline(texts[i]);
                        tmpText.text += texta;
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
    private string dealingwithline(string texta)
    {
        string a = "";
        int i = 0;
        while (i < texta.Length)
        {
            if (texta[i] == '<')
            {
                List<string> L = CheckColorString(texta, i);
                a += L[0];
                i += L[0].Length;
            }
            else if (!System.Char.IsDigit(texta[i]))
            {
                a += texta[i];
                i++;
            }
            else
            {
                i++;
            }
        }
        return a;
    }
    public void Reply(int player, int msg, int fin) //Access this scripts list of replies according to int sent and say it
    {
        string Raw_Text;
        if (fin == 0)
            Raw_Text = waifuResponses[msg];
        else
            Raw_Text = waifuFinishDialogue[Random.Range(0, waifuFinishDialogue.Count)];
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
