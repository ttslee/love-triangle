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
    public float textSpeed = 0.0005f;
    public bool autoStart = false;
    public AudioClip clip;
    private float clipTimer = 0f;
    #endregion

    void Start()
    {
        tmpText = gameObject.GetComponent<TextMeshProUGUI>();
        if (autoStart)
            StartCoroutine(ResetMessage(tmpText));
    }

    private void Update()
    {
        Say(); //If she has something to say [IN TEXT BOX], she outright does it!
    }

    public IEnumerator ResetMessage(TextMeshProUGUI text)
    {
        if (start == true)
            start = false;
        text.maxVisibleCharacters = 0;
        yield return new WaitForSeconds(startDelay);
        start = true;
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
            clipTimer += Time.deltaTime;
            if (clipTimer >= .05f && tmpText.maxVisibleCharacters < tmpText.text.Length)
            {
                SoundManager.Instance.Play(clip);
                clipTimer = 0f;
            }
            timer = 0f;
        }
    }
}
