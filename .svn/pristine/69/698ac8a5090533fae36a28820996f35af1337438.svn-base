using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class WashUI : UIBase
{
   
    Text tips;
    Image _image;//洗手教学图片
    int _index;
    public Sprite[] sprit;
    void Awake()
    {
        tips = transform.Find("main/Tips").GetComponent<Text>();
        tips.text = "请做操作前准备，请按七步洗手法洗手。";
        _image = transform.Find("main/Image").GetComponent<Image>();
        
    }
    //激活image执行切换图片

 

    protected override void Handle(EEvet evt, params object[] args)
    {
        base.Handle(evt, args);
        if (evt==EVT)
        {
            InvokeRepeating("ChangeIma", 0, 1);
        }
    }


    //改变图片
    void ChangeIma()
    {
        _index++;
        _image.sprite = sprit[_index % sprit.Length];
    }


    protected override void Inst()
    {
        base.Inst();
        title.text = "七步洗手教学";
    }

}
