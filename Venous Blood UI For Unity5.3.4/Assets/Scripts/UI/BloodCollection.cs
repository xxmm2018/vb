using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class BloodCollection : UIBase
{
    GameObject _tempTube;
    GameObject tube;
    Text tips;
    Slider slider;

    Text aim;
    Text cur;
     
    bool isFlag = true;
    int id=0;
    Vector3 tubeTargat=new Vector3 (700,0,0);//试管位置
    void Awake()
    {
        tips = transform.Find("main/Tips").GetComponent<Text>();
        aim = transform.Find("main/Aim").GetComponent<Text>();
        cur = transform.Find("main/Cur").GetComponent<Text>();
    }
    float curBlood;
    protected override void Handle(EEvet evt, params object[] args)
    {
    
        if (evt == EVT)
        {
            if (args.Length > 0)
            {
                print((int)args[0]);
                slider = tube.transform.Find("Slider").GetComponent<Slider>();
                curBlood = (float)args[1];
                cur.text = curBlood.ToString("0.00");
                slider.value = curBlood;
                
            }

            tips.text = "正在抽血";
        }
        base.Handle(evt, args);
    }

    protected override void Hide()
    {
        base.Hide();
        isFlag = true;
        if (tube!=null)
        {
            tube.SetActive(false);
        }
    }
    protected override void Show()
    {
        base.Show();
        if (isFlag)
        {
            _tempTube = GlobalMgr.Instance.PrintUIMgr.testtubeList[id];
            tube = Instantiate(_tempTube);
            tube.transform.SetParent(tips.transform);
            tube.transform.localPosition = tubeTargat;
            tube.transform.localScale = Vector3.one;
            id++;
            isFlag = false;

        }
        tube.SetActive(true);
    }

    protected override void Inst()
    {
        base.Inst();
        title.text = "";
    }
}
