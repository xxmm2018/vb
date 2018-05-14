using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Reflection;
public class VTake : VEventBase<VTakeState>
{
    Transform modles;
    Transform backs;
    Transform timerui;
    Transform speakUI;
    bool isTimer;
    float timer;
    public Text content;
    public Text user;
    public List<Tube> tubes = new List<Tube>();
    public int curTubeIndex = -1;
    public bool isLastTube;
    protected override void InitMachince()
    {
        //tubes = new List<Tube>();
        tubes.Add(new Tube(1, "试管1"));
        tubes.Add(new Tube(2, "试管2"));
        tubes.Add(new Tube(3, "试管3"));
        modles = transform.FindChild("Buttons");
        backs = transform.FindChild("backs");
        timerui = transform.FindChild("Timer");
        speakUI= transform.FindChild("Speak");
        content = transform.FindChild("Prograp/content").GetComponent<Text>();
    }
    //设置下一个试管
    public void SetTubeState()
    {
        curTubeIndex++;
        if (curTubeIndex > tubes.Count - 2)
        {
            isLastTube = true;
        }
        else
        {
            tubes[curTubeIndex].isCollecting = true;

        }
    }
    //设置进度
    public void SetTubeProgress(float p)
    {
        tubes[curTubeIndex].curProgress = p;
    }


    /// <summary>
    /// 设置当前模块
    /// </summary>
    /// <param name="str">模块名</param>
    public void SetModleName(string str)
    {
        content.text = str;
        Show(modles, str);
        Show(backs, str);
    }
    /// <summary>
    /// 根据不同的模块调用相应的UI
    /// </summary>
    /// <param name="t">模块UI</param>
    /// <param name="str">模块名</param>
    private void Show(Transform t, string str)
    {
        for (int i = 0; i < t.childCount; i++)
        {
            GameObject curObject = t.GetChild(i).gameObject;
            curObject.SetActive(curObject.name == str ? true : false);
            if (t == backs)
                continue;
            curObject.GetComponentInChildren<Text>().text = str;
        }
    }
    protected override void Update()
    {
        base.Update();
        if (isTimer)
        {
            timer += Time.deltaTime;
            string time = (timer / 60).ToString("0") + ":" + (timer % 60).ToString("0");
            timerui.FindChild("time").GetComponent<Text>().text = time;
            VServer._instance.ServerSend(EEvet.Timer, isTimer, time);
        }
    }
    /// <summary>
    /// 计时器开关
    /// </summary>
    /// <param name="state">开关状态</param>
    public void BeginTimer(bool state)
    {
        timer = 0;
        isTimer = state;
        timerui.gameObject.SetActive(state);
        if (!state)
        {
            VServer._instance.ServerSend(EEvet.Timer, false);
        }
    }


    public void StarSpeak(bool state)
    {
        speakUI.gameObject.SetActive(state);
        GetComponent<VSpeak>().DescriptionText.text = "";
    }


}
[Serializable]
public class Tube
{
    public int id;
    public string _name;
    public float curProgress;
    public float targetProgress;
    //是否已采集
    public bool isCollecting;
    public Tube(int id, string _name)
    {
        this.id = id;
        this._name = _name;
        curProgress = 0;
        targetProgress = 1;
        isCollecting = false;
    }
}
