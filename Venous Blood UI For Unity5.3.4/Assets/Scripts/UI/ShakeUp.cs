using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ShakeUp : UIBase
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
            tips.text = "请上下颠倒摇匀试管";
        }
    }

    protected override void Inst()
    {
        base.Inst();
        title.text = "";
    }
}
