using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TestButtonScript : MonoBehaviour
{

    TextMeshProUGUI tmpText; //TextMeshPro Text Component

    void Start()
    {
        tmpText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        tmpText.transform.localPosition = new Vector3(0, 2, 0);
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        tmpText.transform.localPosition = new Vector3(0, 0, 0);
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(null);
    }



}
