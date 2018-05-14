using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CheckItemUI : UIBase
{

    Text lackText;//缺少物品的文字显示
    Button confirmBtn;
    List<Text> items = new List<Text>();//物品列表
    void Start()
    {
       
        lackText = transform.Find("main/lackIma/lackText").GetComponent<Text>();
        confirmBtn = transform.Find("main/Button").GetComponent<Button>();
        confirmBtn.onClick.AddListener(ConfirmBtnOnClick);
        Eventsys.instance.MyEvent += LackFunc;
        //lackText.text = "1 2 3 4 5 6 7 8 9 10 11 12";
    }


    //显示当前缺少的物品
    void LackFunc()
    {
        print("成功");
        lackText.text = " ";
        for (int i = 0; i < GlobalMgr.Instance.Items.Count; i++)
        {
            lackText.text += GlobalMgr.Instance.Items[i] + " ";
        }
    }

    void ConfirmBtnOnClick()
    {
        VClient.instance.ClientSend(EEvet.Object);
       
    }

    protected override void Handle(EEvet evt, params object[] args)
    {
        base.Handle(evt, args);
        if (evt==EVT)
        {
            LackFunc();
        }
    }

    protected override void Inst()
    {
        base.Inst();
        title.text = "物品选择";
    }

}
