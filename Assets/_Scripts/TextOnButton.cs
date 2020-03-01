using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TextOnButton : MonoBehaviour
{

    TextMeshProUGUI tmpText; //TextMeshPro Text Component

    void Start()
    {
        tmpText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        tmpText.transform.localPosition = new Vector3(0, 2, 0);
        Debug.Log("test");
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(gameObject);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        tmpText.transform.localPosition = new Vector3(0, 0, 0);
        Debug.Log("test");
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(null);
    }



}
