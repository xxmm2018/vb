using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LooseTourniquet : UIBase {
    Image timeimage;

    void Awake()
    {
        timeimage = transform.parent.Find("TimerImage").GetComponent<Image>();

    }
    protected override void Handle(EEvet evt, params object[] args)
    {
        base.Handle(evt, args);
        if (evt == EVT)
        {
            //timeimage.gameObject.SetActive(false);
        }
    }


    protected override void Inst()
    {
        base.Inst();
        title.text = "";
    }
}
