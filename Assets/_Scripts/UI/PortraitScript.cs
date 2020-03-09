using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PortraitScript : MonoBehaviour
{
    GameObject leftPortrait;
    GameObject rightPortrait;
    static private Sprite[] abilitySprites;
    static private Sprite[] loveSprites;
    // Start is called before the first frame update
    void Start()
    {
        abilitySprites = Resources.LoadAll<Sprite>("Ability");
        loveSprites = Resources.LoadAll<Sprite>("Love");
        leftPortrait = GameObject.Find("Portrait_Left");
        rightPortrait = GameObject.Find("Portrait_Right"); 

        leftPortrait.transform.Find("Icon").GetComponent<Image>().sprite = GameManager.Instance.player1Character.Item1;
        leftPortrait.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = GameManager.Instance.player1Character.Item2;

        rightPortrait.transform.Find("Icon").GetComponent<Image>().sprite = GameManager.Instance.player2Character.Item1;
        rightPortrait.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = GameManager.Instance.player2Character.Item2;

        leftPortrait.transform.Find("Love").GetComponent<Image>().enabled = false;
        rightPortrait.transform.Find("Love").GetComponent<Image>().enabled = false;

        leftPortrait.transform.Find("Ability").GetComponent<Image>().enabled = false;
        rightPortrait.transform.Find("Ability").GetComponent<Image>().enabled = false;
    }

    private void Update()
    {
        UpdateLeftBars();
        UpdateRightBars();
    }

    private void UpdateLeftBars()
    {
        int currentLove = GameManager.Instance.player1.GetComponent<PlayerScript>().LoveBar;
        int currentAbility = GameManager.Instance.player1.GetComponent<PlayerScript>().AbilityBar;
        if (currentLove == 0)
        {
            leftPortrait.transform.Find("Love").GetComponent<Image>().enabled = false;
        }
        else
        {
            leftPortrait.transform.Find("Love").GetComponent<Image>().enabled = true;
            leftPortrait.transform.Find("Love").GetComponent<Image>().sprite = loveSprites[currentLove-1];
        }
        if(currentAbility == 0)
        {
            leftPortrait.transform.Find("Ability").GetComponent<Image>().enabled = false;
        }
        else if (currentAbility <= 6)
        {
            leftPortrait.transform.Find("Ability").GetComponent<Image>().enabled = true;
            leftPortrait.transform.Find("Ability").GetComponent<Image>().sprite = abilitySprites[currentAbility-1];
        }
    }

    private void UpdateRightBars()
    {
        int currentLove = GameManager.Instance.player2.GetComponent<PlayerScript>().LoveBar;
        int currentAbility = GameManager.Instance.player2.GetComponent<PlayerScript>().AbilityBar;
        if (currentLove == 0)
        {
            rightPortrait.transform.Find("Love").GetComponent<Image>().enabled = false;
        }
        else
        {
            rightPortrait.transform.Find("Love").GetComponent<Image>().enabled = true;
            rightPortrait.transform.Find("Love").GetComponent<Image>().sprite = loveSprites[currentLove-1];
        }
        if (currentAbility == 0)
        {
            rightPortrait.transform.Find("Ability").GetComponent<Image>().enabled = false;
        }
        else if (currentAbility <= 6)
        {
            rightPortrait.transform.Find("Ability").GetComponent<Image>().enabled = true;
            rightPortrait.transform.Find("Ability").GetComponent<Image>().sprite = abilitySprites[currentAbility-1];
        }
    }
}
