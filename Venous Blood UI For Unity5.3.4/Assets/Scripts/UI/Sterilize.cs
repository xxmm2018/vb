using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Sterilize : UIBase {
    Image image;//消毒示意图
    Text tips;
    void Awake()
    {
        image = transform.Find("main/DisinfectImage").GetComponent<Image>();
        transform.Find("main/Tips").GetComponent<Text>().text = "请消毒皮肤两次，消毒范围直径5cm；以穿刺点为中心，环形消毒；注意不要返回无菌区，不要留白";;
    }
    protected override void Handle(EEvet evt, params object[] args)
    {
        base.Handle(evt, args);
     
    }

    protected override void Inst()
    {
        base.Inst();
        title.text = "";
    }

}
