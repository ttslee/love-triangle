using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaifuDialogue2 : MonoBehaviour
{
    #region Variables
    TextMeshProUGUI tmpText; //TextMeshPro Text Component
    string texts;
    private float timer = 0f;
    private float textSpeed = 0.05f;
    private bool paused = false; //Used for pausing in speech
    private bool started = false; //When the game begins
    private bool colored = false;

    private int totalChars = 0; //Total chars
    private int sayCount = 0; //Chars that have actually been said

    public ScrollRect scrollRect;
    private float scrollAmt = 1f;
    private float scrollSub = .007064f;
    private int maxLines = 5;

    //Expressions
    public Image waifu;
    private List<Sprite> waifuSprites = new List<Sprite>();

    //Sound Effect
    //public AudioClip clip;
    //private float soundTimer = .056f;

    //Debug
    public bool debugMode = false;

    #endregion

    #region Dialogue

    List<string> waifuResponses = new List<string>
    {
        "2Thank you!81 My arms are so tired from carrying them around all day.", //0 //1 Pause
        "3Really player? 81Thai milk tea, half sweet, and less ice please!", //1 //1 Pause
        "2Thanks! 1I wasn't too sure about this shirt but I feel better about it now!", //2 //Takes about 4seconds to say //0 Pause
        "1That is definitely the most original thing I've heard in my entire life, tell me more.", //3 //0 Pause
        "2Is that a brioche?!8 Yeah, I definitely want it if you don't mind!", //4 //1 Pause
        "3While we what?8 Have a d-8date?", //5 //2 Pause
        "5You're not the first to tell me that. That guy over there said the same thing.", //6 //0 Pause
        "1Oh my gosh, yes please! I don't know anyone in that class!", //7 //0 Pause
        "1Depends,8 what movie were you thinking of? 2I'm a big horror fan!", //8 //1 Pause
        "1Gosh, I've been so busy I actually haven't. I'm down for some California Gogi!", //9
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
        "2I did! You noticed player? 81My hair feels way healthier now.", //23 //1 Pause
        "5You really think so?8 There's still so much to learn about you player.", //24 //1 Pause
        "5It's the middle of July, you're so lame.8 2But it's cute, I guess.", //25 //1 Pause
        "1Is it one of those picture-taking ones? 2I'm so down for that.", //26 //0 Pause
        "1Really?82 You're a lifesaver! BTS concert here we come!", //27 //1 Pause
        "1Please!8 It's like forty-five degrees out right now!", //28 //1 Pause
        "3As if you couldn't have been any more obvious player!", //#29 //0 Pause
    };

    List<string> waifuStartDialogue = new List<string>
    {
        "2Hey there!81 Looks like the three of us are going to be partners for this game project. My name's Stella!9",
        "1Sorry to bother you guys, but Kurt says you guys are looking for another member to join your group.82 I'm Stella, nice to meet you!9",
        "1Excuse me!82 Can I interest you two in buying some two dollar boba to support my kpop club?82 Ah, thank you for buying!9",
        "2Hey!81 I'm a transfer student and it's my first day here. The teacher said you guys can show me around after class?9",
        "3Ah!82 Sorry I bumped into you... Oh hey, you guys have that class too! Want to walk together with me?9",
        "2Hi, I'm Stella!81 I'm a fellow CGS major trying to land an internship, but even a basic at home job will do for me haha.9",
        "1I'd like to introduce a woman with a lot of charm, talent, and wit.8 Unfortunately, she couldn't be here tonight, 2so instead I'm here to take her place!8 Nice to meet you, I'm Stella.9"
    };

    List<string> waifuFinishDialogue = new List<string>
    {
        "3.8.8.8 I think I r-really like you player! 8Do you want to go out with me?7",
        "4.8.8.8 player, 8I think you're a really amazing person.8 Do you want to take our relationship a little further?7",
        "4.8.8.8 I really love spending time with you player. 8Would you be mine?7",
        "3.8.8.8 I-8I think I like you player. 8Will you please go out with me!7",
        "3.8.8.8 I like you player. 8Whether you're a hedgehog, a pokemon, or a frog, I don't care anymore!7",
        "1.8.8.8 player, 8I'm sorry for looking at that other guy over there.8 Should I apologize..3 or should I just kiss you right now?7",
        "1.8.8.8 player, 8I'm not going to lie.8 After hearing you talk four sentences,3 I fell in love with you.7"
    };

    Dictionary<string, string> Character_Color = new Dictionary<string, string>
    {
        {"Sanic", "<color=#3f47cc>"},
        {"Pika",  "<color=#fff624>"},
        {"Pepe", "<color=#1e4d25>" }
    };

     #endregion


    void Start() //Initialization
    {
        tmpText = gameObject.GetComponent<TextMeshProUGUI>();
        tmpText.text = "";
        //tmpText.maxVisibleLines = 5;
        Sprite[] sprites = Resources.LoadAll<Sprite>("Waifu");
        foreach (Sprite s in sprites)
            waifuSprites.Add(s);
        if (!debugMode)
            QueueText(waifuStartDialogue[Random.Range(0, waifuStartDialogue.Count)]);
        else //DEBUG
        {
            //QueueText("averylongword. averylongword");
            //QueueText("Here <color=#1e4d25>there</color> <color=#1e4d25>there</color> <color=#1e4d25>toobiggo</color>");
            QueueText("Here there");
            QueueText("Here there");
            //QueueText("Behold!");
            //QueueText("Here there Here there Here thereHere there Here there Here there");
            //QueueText("Here there Here there Here thereHere there Here there Here there");
            //QueueText("Here there Here there Here thereHere there Here there Here there");
            //QueueText("Here there Here there Here thereHere there Here there Here there");
        }
    }

    private IEnumerator Pause(float delay)
    {
        paused = true;
        yield return new WaitForSeconds(delay);
        paused = false;
    }

    void Update()
    {
        if (!paused)
            Say(); //If she has something to say, she outright does it!
    }

    public void QueueText(string text)
    {
        if (!started) //If its the very starting line, we don't want \n in the front
        {
            texts += text;
            totalChars += text.Length;
            started = true;
        }
        else
        {
            texts += "\n\n" + text;
            totalChars += text.Length + 2; // "\n" counts as +1
        }
    }

    private void Say()
    {
        //Timer for typewriter effect and sound effect
        timer += Time.deltaTime;
        //soundTimer += Time.deltaTime;
        //CheckMaxLines();
        if (timer >= textSpeed)
        {
            //if (sayCount != totalChars && soundTimer >= .056f)
            //{
            //    SoundManager.Instance.Play(clip);
            //    soundTimer = 0f;
            //}
            if (sayCount != totalChars)
            {
                //Line Handler
                CheckMaxLines();

                //Color Handler
                if (texts[sayCount] == '<' && !colored)
                {
                    tmpText.text += texts.Substring(sayCount, 15);
                    sayCount += 15;
                    colored = true;
                }
                else if (texts[sayCount] == '<' && colored)
                {
                    tmpText.text += "</color>"; //sayCount lands on '>'
                    sayCount += 8;
                    colored = false;
                }
                else if (texts[sayCount] == ' ')
                {
                    CheckSpace();
                }
                else
                {
                    tmpText.text += texts[sayCount];
                    sayCount++;
                    timer = 0f;
                }
            }
            //Debug.Log(sayCount);
            //Debug.Log(totalChars);
        }
    }

    private void CheckSpace()
    {
        tmpText.ForceMeshUpdate();
        int lineCount = tmpText.textInfo.lineCount;
        Debug.Log(tmpText.textInfo.lineCount);
        bool tempColored = false;
        bool needSpace = false;
        string tempTexts = tmpText.text;
        tmpText.text += "<alpha=#00>";
        for (int i = sayCount + 1; i < totalChars; i++)
        {
            //Color Handler
            if (texts[i] == '<' && !tempColored)
            {
                tmpText.text += texts.Substring(i, 15);
                i += 15;
                tempColored = true;
            }
            else if (texts[i] == '<' && tempColored)
            {
                tmpText.text += "</color>"; //sayCount lands after '>'
                i += 8;
                tempColored = false;
                if (sayCount == totalChars)
                    break;
            }
            if (i >= totalChars || texts[i] == ' ') //If onto next word before next line
                break;
            //Check Space
            tmpText.text += texts[i]; //Add to original text to check
            tmpText.ForceMeshUpdate(); //Update textInfo
            if (lineCount != tmpText.textInfo.lineCount) //Check if adding this one character moved the text to new line
            {
                tmpText.text = tmpText.text.Substring(0, tmpText.text.Length - 1); //If it did revert text back by one character
                needSpace = true;
                break;
            }
        }
        //Debug.Log(needSpace);
        if (needSpace) //Apply adjustments
        {
            tmpText.text = tempTexts + "\n";
            sayCount++;
            timer = 0f;
            return;
        }
        else
        { //Revert adjustments
            tmpText.text = tempTexts; //Back to original as if nothing happened.
            tmpText.text += texts[sayCount];
            sayCount++;
            timer = 0f;
        }
    }

    private void CheckMaxLines() //Removes first line if true
    {
        tmpText.ForceMeshUpdate(); //Update Information
        int lineCount = tmpText.textInfo.lineCount;
        if (lineCount > maxLines) //lineCount gets the line of the last letter. So if it goes out the textbox, its true
        {
            float tempAmt;
            if (texts[sayCount - 1] == '\n')
            {
                tempAmt = scrollAmt - (scrollSub * 2);
                maxLines++;
            }
            else
                tempAmt = scrollAmt - scrollSub; 
            Canvas.ForceUpdateCanvases();
            scrollRect.verticalNormalizedPosition = tempAmt;
            Canvas.ForceUpdateCanvases();
            scrollAmt = tempAmt;
            maxLines++;
        }
    }
}
