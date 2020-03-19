using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonScript : MonoBehaviour, ISelectHandler, IDeselectHandler
{

    // TEXT MESH DATA
    public bool hasText = false;
    TextMeshProUGUI tmpText = null;

    // META
    private bool p1Inside = false;
    private bool p2Inside = false;
    private bool cursorInside = false;

    //Event Systems
    private EventSystem eventSystemP1;
    private EventSystem eventSystemP2;

    void Start()
    {
        if(hasText)
            tmpText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        eventSystemP1 = GameObject.FindGameObjectWithTag("EventP1").GetComponent<EventSystem>();
        eventSystemP2 = GameObject.FindGameObjectWithTag("EventP2").GetComponent<EventSystem>();
    }

    #region Standalone Mouse

    private void OnMouseEnter()
    {
        if (GameManager.Instance.MainMenuOn || GameManager.Instance.PauseMenuOn)
        {
            cursorInside = true;
            if (hasText)
                tmpText.transform.localPosition = new Vector3(0, 2, 0);
        }
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.MainMenuOn || GameManager.Instance.PauseMenuOn)
        {
            if (hasText)
                tmpText.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    private void OnMouseUp()
    {
        if (GameManager.Instance.MainMenuOn || GameManager.Instance.PauseMenuOn)
        {
            if (hasText)
                tmpText.transform.localPosition = new Vector3(0, 2, 0);
        }
    }
    private void OnMouseExit()
    {
        cursorInside = false;
        if (!HoveringMice()) //If theres no other mice inside, unselect the button
        {
            if (GameManager.Instance.MainMenuOn || GameManager.Instance.PauseMenuOn)
            {
                if (hasText)
                    tmpText.transform.localPosition = new Vector3(0, 0, 0);
            }
        }
    }

    #endregion

    private bool HoveringMice() //Checks if any Mouse is hovering over the buttons
    {
        return p1Inside || p2Inside || cursorInside;
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (GameManager.Instance.MainMenuOn || GameManager.Instance.PauseMenuOn)
        {
            if (other.gameObject.CompareTag("Mouse")) //checks if collision is Controller Mouse
            {
                switch (other.gameObject.GetComponent<MouseScript>().Player) //If Controller Mouse enters, update bools
                {
                    case 1:
                        p1Inside = true;
                        eventSystemP1.SetSelectedGameObject(gameObject);
                        break;
                    case 2:
                        p2Inside = true;
                        eventSystemP2.SetSelectedGameObject(gameObject);
                        break;
                }
            }
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (GameManager.Instance.MainMenuOn || GameManager.Instance.PauseMenuOn)
        {
            switch (other.gameObject.GetComponent<MouseScript>().Player) //If Controller Mouse leaves, update bools
            {
                case 1:
                    p1Inside = false;
                    eventSystemP1.SetSelectedGameObject(null);
                    break;
                case 2:
                    p2Inside = false;
                    eventSystemP2.SetSelectedGameObject(null);
                    break;
            }
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (GameManager.Instance.MainMenuOn || GameManager.Instance.PauseMenuOn)
        {
            if (hasText)
                tmpText.transform.localPosition = new Vector3(0, 2, 0);
        }
    }
    public void OnDeselect(BaseEventData data)
    {
        if (GameManager.Instance.MainMenuOn || GameManager.Instance.PauseMenuOn)
        {
            if (hasText && !HoveringMice())
                tmpText.transform.localPosition = new Vector3(0, 0, 0);
        }
    }
    public void Clicked()
    {
        StartCoroutine(Delay());
    }
    private IEnumerator Delay()
    {
        if (hasText)
            tmpText.transform.localPosition = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(.1f);
        if (hasText)
            tmpText.transform.localPosition = new Vector3(0, 2, 0);
    }

}
