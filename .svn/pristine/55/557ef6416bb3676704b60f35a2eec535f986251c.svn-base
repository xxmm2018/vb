using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CheckItem : MonoBehaviour
{
    Toggle toggle;
    Text text;
    void Start()
    {
        toggle = transform.Find("Toggle").GetComponent<Toggle>();
        text = transform.Find("Text").GetComponent<Text>();
        toggle.onValueChanged.AddListener((bool value) => OnToggleClick(toggle, value));
    }

    void OnToggleClick(Toggle toggle, bool value)
    {
        if (value)
        {
                GlobalMgr.Instance.CheckList.Add(text.text);   
            
        }
        else 
        {
                GlobalMgr.Instance.CheckList.Remove(text.text);
        }
    }


    void Update()
    {
        print(GlobalMgr.Instance.CheckList.Count);
    }

}
