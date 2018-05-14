using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CheckPatientUI : UIBase
{
    GameObject _List;//病人申请单
    GameObject _curList; //当前病人申请单
    Button button;
    Transform main;

    //字典中有这个数据时才查找

    void Awake()
    {
        main = transform.Find("main");
    }
    protected override void Handle(EEvet evt, params object[] args)
    {
        base.Handle(evt, args);
        if (evt==EVT)
        {
            _List = GlobalMgr.Instance.CheckListUIMgr.checkListDic[GlobalMgr.Instance.CheckListUIMgr._curId];
            _curList = Instantiate(_List);
            _curList.transform.SetParent(main);
            _curList.transform.localPosition = Vector3.zero;
            _curList.transform.localScale = Vector3.one;
            _curList.SetActive(true);
        }
    }

    protected override void Inst()
    {
        base.Inst();
        title.text = "核对检验单";
    }

}
