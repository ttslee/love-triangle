using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugUse : MonoBehaviour
{
    // Start is called before the first frame update
    string a = "<color=#005500>name</color>";
    void Start()
    {
        List<string> list = SeprateString(a);
        for (int i = 0; i < list.Count; i++)
        {
            Debug.Log(list[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private List<string> SeprateString(string a)
    {
        List<string> list = new List<string>();
        string begin = "";
        string target = "";
        string end = "";
        int leftcount = 0;
        int rightcount = 0;
        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] == '<')
            {
                if (leftcount == 0)
                {
                    begin += a[i];
                }
                if (leftcount == 1)
                {
                    end += a[i];
                }
                leftcount += 1;
            }
            else if (a[i] == '>')
            {
                if (rightcount == 0)
                {
                    begin += a[i];
                }
                if (rightcount == 1)
                {
                    end += a[i];
                }
                rightcount += 1;

            }
            else
            {
                if (leftcount == 1 && rightcount == 0)
                {
                    begin += a[i];
                }
                if (leftcount == 1 && rightcount == 1)
                {
                    target += a[i];
                }
                if (leftcount == 2 && rightcount == 1)
                {
                    end += a[i];
                }
            }
        }
        for (int i = 0; i < target.Length; i++)
        {
          list.Add(begin + target[i] + end);
        }
        return list;
    }
}
