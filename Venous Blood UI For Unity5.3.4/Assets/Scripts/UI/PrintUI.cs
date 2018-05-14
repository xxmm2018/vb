using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PrintUI : UIBase
{
    PasteTag _cur;
    int a;
    GameObject list;//患者检查单
    GameObject curlist;//对象引用
    Transform checkList;//选中的病人的检验单
    Transform _grid;
    GameObject center;//病人具体检查项目的列表
    Button confirmBtn;
    Image image;//是否打印 界面
    GameObject proof;//对象引用
    GameObject _temp;
    GameObject _tempTag;
    GameObject testTube;
    GameObject _tag;
    GameObject _tempTube;
    RectTransform imageRect;
    Button nextBtn;
    ///////////////////////////////////////////////////////////////////////////////////////////////
    Text _name;
    Text _gender;
    Text _id;
    Text _item;
    Text _date;

    Text _name1;
    Text _gender1;
    Text _id1;
    Text _item1;
    ///////////////////////////////////////////////////////////////////////////////////////////////

    Transform lable;
    RectTransform lableRect;

    void Awake()
    {
        lable = transform.Find("main/Center/Scroll/lable");
        lableRect = lable.GetComponent<RectTransform>();
        //lableRect.localPosition = new Vector3(0, -2484, 0);
         checkList = transform.Find("main/CheckList");
        _grid = transform.Find("main/Center/Scroll/ScrollVive/GameObject");
        imageRect = _grid.GetComponent<RectTransform>();
        nextBtn = transform.Find("main/Center/NextBtn").GetComponent<Button>();
        nextBtn.gameObject.SetActive(false);
        nextBtn.onClick.AddListener(NextBtnFunc);
         center = transform.Find("main/Center").gameObject;
        center.SetActive(false);
       
        confirmBtn = transform.Find("main/Image/ConfirmBtn").GetComponent<Button>();
        image = transform.Find("main/Image").GetComponent<Image>();
        confirmBtn.onClick.AddListener(ConfirmBtnFunc);
    }

    //动态修改病人检查项滑动条大小
    int size=1018;//一个检查项的大小
    void SizeFunc()
    {
        imageRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 918 + (GlobalMgr.Instance.CheckListUIMgr.s.Count-1)*1018);
        lableRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 918 + (GlobalMgr.Instance.CheckListUIMgr.s.Count - 1) * 1018);
    }
 
    void ConfirmBtnFunc()
    {
        center.SetActive(true);
        
        image.gameObject.SetActive(false);
        checkList.gameObject.SetActive(false);
        SizeFunc();
        CheckItem();
        lableRect.anchoredPosition3D = new Vector3(0,  -500-(GlobalMgr.Instance.CheckListUIMgr.s.Count+1 ) * 1018 / 2, 0);//重置最初的位置使其在显示范围外
        InvokeRepeating("PrintTag", 1, 0.5f);
    }

    int i = 100;
    //打印标签
    void PrintTag()
    {
        if (lableRect.anchoredPosition3D.y<=-500+(GlobalMgr.Instance.CheckListUIMgr.s.Count - 1) * 1018/2)//如果y轴不大于自身大小的一半 加上差值500（说明未到达最低端）
        {
            print(-(GlobalMgr.Instance.CheckListUIMgr.s.Count) * 1000);
            print(lableRect.anchoredPosition3D.y);
            lableRect.anchoredPosition3D = new Vector3(0, lableRect.anchoredPosition3D.y+i, 0);
        }
        else if(lableRect.anchoredPosition3D.y > -500 + (GlobalMgr.Instance.CheckListUIMgr.s.Count - 1) * 1018 / 2)
        {
            lable.SetParent(_grid);
            lable.localPosition = Vector3.zero;
            lable.localScale = Vector3.one;
            nextBtn.gameObject.SetActive(true);
            CancelInvoke("PrintTag");
        }
    }
    void NextBtnFunc()
    {
        VClient.instance.ClientSend(EEvet.Certificate);
    }
    //生成相应个数的标签
    void CheckItem()
    {
        proof = Resources.Load("UIPrefab/Proof") as GameObject;
        for (int i = 0; i < GlobalMgr.Instance.CheckListUIMgr.s.Count; i++)
        {
            _temp = Instantiate(proof);
            _temp.transform.SetParent(lable);
            _temp.transform.localPosition = Vector3.zero;
            _temp.transform.localScale = Vector3.one;
            GlobalMgr.Instance.PrintUIMgr.TagDic.Add(i, _temp);
            ProofInst(i);
        }
    }
        
    //标签具体信息初始化
    void ProofInst(int i)
    {

        _name = _temp.transform.Find("GameObject/Name").GetComponent<Text>();
        _gender = _temp.transform.Find("GameObject/Gender").GetComponent<Text>();
        _id = _temp.transform.Find("GameObject/ID").GetComponent<Text>();
        _item = _temp.transform.Find("GameObject/Item").GetComponent<Text>();
        _date= _temp.transform.Find("GameObject/Date").GetComponent<Text>();

        _name1= _temp.transform.Find("Tag/Name").GetComponent<Text>();
        _gender1= _temp.transform.Find("Tag/Gender").GetComponent<Text>();
        _id1= _temp.transform.Find("Tag/ID").GetComponent<Text>();
        _item1 = _temp.transform.Find("Tag/Item").GetComponent<Text>();

        _name.text = GlobalMgr.Instance.CheckListUIMgr.Infos[GlobalMgr.Instance.CheckListUIMgr._curId]._name;
        _gender.text = GlobalMgr.Instance.CheckListUIMgr.Infos[GlobalMgr.Instance.CheckListUIMgr._curId]._gender;
        _id.text = GlobalMgr.Instance.CheckListUIMgr.Infos[GlobalMgr.Instance.CheckListUIMgr._curId]._gender;
        _date.text = System.DateTime.Now.Year.ToString() + "年" + System.DateTime.Now.Month.ToString() + "月" + System.DateTime.Now.Day.ToString() + "日";
        _name1.text= GlobalMgr.Instance.CheckListUIMgr.Infos[GlobalMgr.Instance.CheckListUIMgr._curId]._name;
        _gender1.text= GlobalMgr.Instance.CheckListUIMgr.Infos[GlobalMgr.Instance.CheckListUIMgr._curId]._gender;
        _id.text= GlobalMgr.Instance.CheckListUIMgr.Infos[GlobalMgr.Instance.CheckListUIMgr._curId]._id;


        if (i == 0)
        {
            _item.text = GlobalMgr.Instance.CheckListUIMgr.s[0].ToString();
            _item1.text = _item.text;
        }
        else if (i == 1)
        {
            _item.text = GlobalMgr.Instance.CheckListUIMgr.s[1].ToString();
            _item1.text = _item.text;
        }
        else if (i == 2)
        {
            _item.text = GlobalMgr.Instance.CheckListUIMgr.s[2].ToString();
            _item1.text = _item.text;
        }



    }

    protected override void Handle(EEvet evt, params object[] args)
    {
        base.Handle(evt, args);
        if (evt==EVT)
        {
            list = GlobalMgr.Instance.CheckListUIMgr.checkListDic[GlobalMgr.Instance.CheckListUIMgr._cur._id];
            curlist = Instantiate(list);
            curlist.transform.SetParent(checkList);
            curlist.transform.localPosition = Vector3.zero;
            curlist.transform.localScale = new Vector3(0.63f, 0.63f, 1);
            curlist.SetActive(true);
        }
    }
    protected override void Inst()
    {
        base.Inst();
        title.text = "打印标签";
    }


}
