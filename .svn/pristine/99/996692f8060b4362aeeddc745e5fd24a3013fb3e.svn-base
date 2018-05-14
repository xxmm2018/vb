using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Inspection : UIBase
{
    Text tips;
    Button button;
    void Awake()
    {
        tips = transform.Find("main/Tips").GetComponent<Text>();
        tips.text = "请收取检验申请单";
        button = transform.Find("main/Button").GetComponent<Button>();
        button.onClick.AddListener(ButtonFunc);
    }

    void ButtonFunc()
    {
        VClient.instance.ClientSend(EEvet.Inspection);
    }

    protected override void Inst()
    {
        base.Inst();
        title.text = "";
    }

}
