using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuDialogue : MonoBehaviour
{
    #region Variables

    //Variables used in Say() Function
    TextMeshProUGUI tmpText; //TextMeshPro Text Component
    private float timer = 0f;
    public float startDelay = 0.0f;
    private bool start = false;
    public float textSpeed = 0.05f;

    #endregion

    IEnumerator Start()
    {
        //Initialization
        tmpText = gameObject.GetComponent<TextMeshProUGUI>();
        tmpText.maxVisibleCharacters = 0;
        yield return new WaitForSeconds(startDelay);
        start = true;
    }

    private void Update()
    {
        Say(); //If she has something to say [IN TEXT BOX], she outright does it!
    }

    private void Say() //Types out text with type writer effect.
    {
        timer += Time.deltaTime;

        if (start == true && timer >= textSpeed)
        {
            if (tmpText.text.Length > tmpText.maxVisibleCharacters)
            {
                tmpText.maxVisibleCharacters++;
            }
            timer = 0f;
        }
    }
}
