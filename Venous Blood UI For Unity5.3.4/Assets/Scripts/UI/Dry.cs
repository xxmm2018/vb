using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Dry : UIBase
{
    Text text;
    void Awake()
    {
        text = transform.Find("main/Tips").GetComponent<Text>();
    }

    protected override void Handle(EEvet evt, params object[] args)
    {
        base.Handle(evt, args);
        if (evt==EVT)
        {
            text.text = "消毒完成，请等待消毒液体完全自然挥发；否则会影响血样结果或导致微生物污染";
        }
    }

    protected override void Inst()
    {
        base.Inst();
        title.text = "";
    }

}
