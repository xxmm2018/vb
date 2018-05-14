using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Pulling : UIBase
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
            tips.text ="血液样本采集完成，请拿取棉签按压穿刺点后拔针";
        }
    }
    protected override void Inst()
    {
        base.Inst();
        title.text = "";
    }

}
