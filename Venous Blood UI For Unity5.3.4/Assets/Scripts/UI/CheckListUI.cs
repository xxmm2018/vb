using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class CheckListUI : UIBase
{

    Toggle toggle1;
    Toggle toggle2;
    Toggle toggle3;
    Toggle toggle4;
   
    Text _name;//患者姓名
    Text _gender;//患者性别
    Text _id;//患者id
    Text _checkList;//检查项目
    Text doctorName;//申请医生姓名
    Text date;//申请日期（当前系统日期）
    Image checkList;//检验单
    Button nextBtn;//进入下一个界面


    void Start()
    {
        toggle1 = transform.Find("main/Toggle1").GetComponent<Toggle>();
        toggle2 = transform.Find("main/Toggle2").GetComponent<Toggle>();
        toggle3 = transform.Find("main/Toggle3").GetComponent<Toggle>();
        toggle4 = transform.Find("main/Toggle4").GetComponent<Toggle>();
        nextBtn = transform.Find("main/NextBtn").GetComponent<Button>();
        _name = transform.Find("main/CheckList/Name").GetComponent<Text>();
        _gender = transform.Find("main/CheckList/Gender").GetComponent<Text>();
        _id = transform.Find("main/CheckList/ID").GetComponent<Text>();
        _checkList = transform.Find("main/CheckList/CheckItem").GetComponent<Text>();
        doctorName = transform.Find("main/CheckList/DoctorName").GetComponent<Text>();
        date = transform.Find("main/CheckList/Date").GetComponent<Text>();
        GlobalMgr.Instance.CheckListUIMgr.s.Add(ITEM.白细胞检测);   
        GlobalMgr.Instance.CheckListUIMgr.s.Add(ITEM.血小板检测);
        SetInfo(new Person("小明", "男", "10000001", "王医生"));
        GlobalMgr.Instance.CheckListUIMgr._cur = new Person("小明", "男", "10000001", "王医生");
        GlobalMgr.Instance.CheckListUIMgr._curId = GlobalMgr.Instance.CheckListUIMgr._cur._id;
        checkList = transform.Find("main/CheckList").GetComponent<Image>();
        toggle1.onValueChanged.AddListener((bool value) => Toggle1Func(toggle1, value));
        toggle2.onValueChanged.AddListener((bool value) => Toggle2Func(toggle2, value));
        toggle3.onValueChanged.AddListener((bool value) => Toggle3Func(toggle3, value));
        toggle4.onValueChanged.AddListener((bool value) => Toggle4Func(toggle4, value));

        nextBtn.onClick.AddListener(NextBtnOnClick);
    }



    void Toggle1Func(Toggle toggle, bool value)
    {
        GlobalMgr.Instance.CheckListUIMgr.s.Clear();
        GlobalMgr.Instance.CheckListUIMgr.s.Add(ITEM.血小板检测);
        SetInfo(new Person("小明", "男", "10000001", "王医生"));
    }

    private void SetInfo(Person p)
    {
        GlobalMgr.Instance.CheckListUIMgr.Infos.Clear();
        _name.text = p._name;
        _gender.text = p._gender;
        _id.text = p._id;
        _checkList.text = CheckFunc();
        doctorName.text = p.doctorName;
        date.text = p.date;
        GlobalMgr.Instance.CheckListUIMgr._curId = p._id;
        GlobalMgr.Instance.CheckListUIMgr._cur = p;
        GlobalMgr.Instance.CheckListUIMgr.Infos.Add(p._id, p);
    }
    /// <summary>
    /// 便利 检查项
    /// </summary>
    string CheckFunc()
    {
        string text="";
        foreach (var item in GlobalMgr.Instance.CheckListUIMgr.s)
        {
            text += item.ToString();
            text += "\n";
        }
        return text;
    }

    void Toggle2Func(Toggle toggle, bool value)
    {
        GlobalMgr.Instance.CheckListUIMgr.s.Clear();
        GlobalMgr.Instance.CheckListUIMgr.s.Add(ITEM.白细胞检测);
        GlobalMgr.Instance.CheckListUIMgr.s.Add(ITEM.血小板检测);
        GlobalMgr.Instance.CheckListUIMgr.s.Add(ITEM.血脂分析);
        GlobalMgr.Instance.CheckListUIMgr.s.Add(ITEM.血常规);
        SetInfo(new Person("小红", "女", "10000002", "刘医生"));
    }
    void Toggle3Func(Toggle toggle, bool value)
    {
        GlobalMgr.Instance.CheckListUIMgr.s.Clear();
        GlobalMgr.Instance.CheckListUIMgr.s.Add(ITEM.白细胞检测);
        GlobalMgr.Instance.CheckListUIMgr.s.Add(ITEM.血小板检测);
        GlobalMgr.Instance.CheckListUIMgr.s.Add(ITEM.血脂分析);
        SetInfo(new Person("小看", "女", "10000002", "王医生"));
    }
    void Toggle4Func(Toggle toggle, bool value)
    {
        GlobalMgr.Instance.CheckListUIMgr.s.Clear();
        GlobalMgr.Instance.CheckListUIMgr.s.Add(ITEM.白细胞检测);
        GlobalMgr.Instance.CheckListUIMgr.s.Add(ITEM.血脂分析);
        SetInfo(new Person("小路", "女", "22344",  "秦医生"));
    }

    

    void NextBtnOnClick()
    {
        GlobalMgr.Instance.CheckListUIMgr.checkListDic.Add(GlobalMgr.Instance.CheckListUIMgr._cur._id, checkList.gameObject);
        GlobalMgr.Instance.CheckListUIMgr.Info.Add(GlobalMgr.Instance.CheckListUIMgr._cur._id, GlobalMgr.Instance.CheckListUIMgr._cur);
        GlobalMgr.Instance.num = GlobalMgr.Instance.CheckListUIMgr.s.Count;


        VClient.instance.ClientSend(EEvet.Check);
    }

    protected override void Handle(EEvet evt, params object[] args)
    {
        base.Handle(evt, args);
    }

    protected override void Inst()
    {
        base.Inst();
        title.text = "检验选择";
    }

}





















public enum ITEM
{
    血脂分析,
    血常规,
    葡萄糖鉴定,
    白细胞检测,
    血小板检测
}
public class Person
{
  
    public string _name;
    public string _gender;
    public string _id;
    public List<ITEM> _check;
    public string doctorName;
    public string date;
    public Person(string _name, string _gender, string _id, string doctorName)
    {
        this._name = _name;
        this._gender = _gender;
        this._id = _id;
        this.doctorName = "申请医生："+ doctorName;
        this.date = System.DateTime.Now.Year.ToString() + "年" + System.DateTime.Now.Month.ToString() + "月" + System.DateTime.Now.Day.ToString() + "日";
    }
    public Person Get()
    {
        return this;
    }
}
