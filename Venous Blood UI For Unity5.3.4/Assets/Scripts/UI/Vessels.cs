using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Vessels : UIBase {
    Image image;//血管示意图
    Image timerimage;//
    
    void Awake()
    {
        image = transform.Find("main/DissectionImage").GetComponent<Image>();
        timerimage = transform.parent.Find("TimerImage").GetComponent<Image>();
       
    }

    protected override void Handle(EEvet evt, params object[] args)
    {
        base.Handle(evt, args);
        if (evt==EVT)
        {
            image.gameObject.SetActive(true);
            timerimage.gameObject.SetActive(true);
        }
    }
    protected override void Inst()
    {
        base.Inst();
        title.text = "";
    }

}
