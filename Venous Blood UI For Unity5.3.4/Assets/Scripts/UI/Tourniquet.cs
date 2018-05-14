using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Tourniquet : UIBase {

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
            tips.text = "请在穿刺点上方6cm处扎压脉带；注意扎带时长，不能超过1分钟；当血液流入采血管时，即可松开止血带";
        }
    }

    protected override void Inst()
    {
        base.Inst();
        title.text = "";
    }



}
