using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PortraitScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Transform left = transform.Find("Portrait_Left");
        Transform right = transform.Find("Portrait_Right");

        left.Find("Icon").GetComponent<Image>().sprite = GameManager.Instance.player1Character.Item1;
        left.Find("Name").GetComponent<TextMeshProUGUI>().text = GameManager.Instance.player1Character.Item2;

        right.Find("Icon").GetComponent<Image>().sprite = GameManager.Instance.player2Character.Item1;
        right.Find("Name").GetComponent<TextMeshProUGUI>().text = GameManager.Instance.player2Character.Item2;
    }
}
