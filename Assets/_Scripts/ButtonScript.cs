using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ButtonScript : MonoBehaviour
{
    public bool player1Allowed = true;
    public bool player2Allowed = true;

    public bool p1Inside = false;
    public bool p2Inside = false;

    private bool HoveringMice()
    {
        return p1Inside || p2Inside;
    }
    public Button button;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Mouse"))
        {
            switch(other.gameObject.GetComponent<MouseScript>().Player)
            {
                case 1:
                    print("yo");
                    p1Inside = true;
                    break;
                case 2:
                    p2Inside = true;
                    break;
            }
            GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(gameObject);
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        switch(other.gameObject.GetComponent<MouseScript>().Player)
        {
            case 1:
                p1Inside = false;
                break;
            case 2:
                p2Inside = false;
                break;
        }
        if(!HoveringMice())
            GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(null);
    }

    public void Clicked()
    {
        button.onClick.Invoke();
    }
}
