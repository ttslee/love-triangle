using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonScript : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    // CLICKED TIMER DELAY AND DATA
    private bool timer = false;
    private float delay = .1f;

    // TEXT MESH DATA
    public bool hasText = false;
    TextMeshProUGUI tmpText = null;

    // META
    public bool player1Allowed = true;
    public bool player2Allowed = true;

    private bool p1Inside = false;
    private bool p2Inside = false;

    void Start()
    {
        if(hasText)
            tmpText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }
    public void Update()
    {
        if (delay <=0)
        {
            tmpText.transform.localPosition = new Vector3(0, 2, 0);
            timer = false;
            delay = .1f;
        }
        if(timer)
            delay -= Time.deltaTime;

    }
    private bool HoveringMice()
    {
        return p1Inside || p2Inside;
    }

    public Button button;
    public void OnTriggerStay2D(Collider2D other)
    {
        if (!GameManager.Instance.MainMenuOn || !GameManager.Instance.PauseMenuOn)
            return;
        if(other.gameObject.CompareTag("Mouse"))
        {
            switch(other.gameObject.GetComponent<MouseScript>().Player)
            {
                case 1:
                    if (!player1Allowed)
                        return;
                    p1Inside = true;
                    break;
                case 2:
                    if (!player2Allowed)
                        return;
                    p2Inside = true;
                    break;
            }
            GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(gameObject);
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (!GameManager.Instance.MainMenuOn || !GameManager.Instance.PauseMenuOn)
            return;
        switch (other.gameObject.GetComponent<MouseScript>().Player)
        {
            case 1:
                p1Inside = false;
                break;
            case 2:
                p2Inside = false;
                break;
        }
        if (!HoveringMice())
        {
            GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(null);
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (!GameManager.Instance.MainMenuOn || !GameManager.Instance.PauseMenuOn)
            return;
        if (hasText)
            tmpText.transform.localPosition = new Vector3(0, 2, 0);
    }
    public void OnDeselect(BaseEventData data)
    {
        if (!GameManager.Instance.MainMenuOn || !GameManager.Instance.PauseMenuOn)
            return;
        if (hasText)
            tmpText.transform.localPosition = new Vector3(0, 0, 0);
    }
    public void Clicked()
    {
        timer = true;
        if (hasText)
            tmpText.transform.localPosition = new Vector3(0, 0, 0);
    }
    //public void Clicked()
    //{
    //    if (!GameManager.Instance.MainMenuOn && !GameManager.Instance.PauseMenuOn)
    //        return;
    //    button.onClick.Invoke();
    //}
}
