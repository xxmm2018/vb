using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Puncture : UIBase {

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
            tips.text = "绷紧皮肤，针尖斜面向上，与皮肤成5度角刺入";
        }
   
    }
    protected override void Inst()
    {
        base.Inst();
        title.text = "";
    }
}
