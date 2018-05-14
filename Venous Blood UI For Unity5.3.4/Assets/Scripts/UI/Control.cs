using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Control : UIBase
{
    Text tips;
    void Awake()
    {
        tips = transform.Find("main/Tips").GetComponent<Text>();
    }
    protected override void Handle(EEvet evt, params object[] args)
    {
        base.Handle(evt, args);
        if (evt==EVT)
        {
            tips.text = "请嘱咐患者按压，至少五分钟";
        }
    }

    protected override void Inst()
    {
        base.Inst();
        title.text = "";
    }
}
