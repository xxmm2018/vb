using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ModeChooseUI : UIBase  
{
    Button confirmBtn;
    Toggle mode1;
    Toggle mode2;
    Toggle mode3;

    void Awake()
    {
     
        confirmBtn = transform.Find("main/ConfirmBtn").GetComponent<Button>();
        mode1 = transform.Find("main/Mode1").GetComponent<Toggle>();
        mode2 = transform.Find("main/Mode2").GetComponent<Toggle>();
        mode3 = transform.Find("main/Mode3").GetComponent<Toggle>();
        confirmBtn.onClick.AddListener(ConfirmBtnOnClick);
    }



    void ConfirmBtnOnClick()
    {
        VClient.instance.ClientSend(EEvet.Model, 1);
    }

    protected override void Handle(EEvet evt, params object[] args)
    {
        base.Handle(evt, args);
    }

    protected override void Inst()
    {
        base.Inst();
        print(11111111111);
        title.text = "模式选择";
    }


}
