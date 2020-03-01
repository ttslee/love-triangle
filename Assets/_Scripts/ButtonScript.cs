using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ButtonScript : MonoBehaviour
{
    public bool player1Allowed = true;
    public bool player2Allowed = true;

    private bool p1Inside = false;
    private bool p2Inside = false;

    private bool HoveringMice()
    {
        return p1Inside || p2Inside;
    }
    public Button button;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Mouse"))
        {
            button.Select();
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if(!HoveringMice())
        {
            GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(null);
        }
    }

    public void Clicked()
    {
        button.onClick.Invoke();
    }
}
