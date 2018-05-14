using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ContinuousBloodCollection : UIBase {

    Text tips1;
    Text tips2;
    GameObject _tempTube;
    GameObject tube;
    Slider curBlood;
    Text cur;
    void Awake()
    {
        tips1 = transform.Find("main/Tips1").GetComponent<Text>();
        
    }

    protected override void Handle(EEvet evt, params object[] args)
    {
        base.Handle(evt, args);
        if (evt==EVT)
        {
                tips1.text = "穿刺成功，请连入采血管，开始采集血液标本";
                
        }
        
    }
    protected override void Inst()
    {
        base.Inst();
        title.text = "";
    }

}
