using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UIMgr : MonoBehaviour
{
    GameObject _tempUI;//加载UI的引用

    Canvas _mainCanvas;
    public Canvas _mainCanvas_p
    {
        get { return _mainCanvas; }
    }
    EventSystem _eventSys;

    private UIBase _tempUIBase;
    UIBase _curUI;//当前开启UI界面
    public UIBase curUI_p
    {
        get { return _curUI; }
    }
    public Dictionary<string, GameObject> UIGroup = new Dictionary<string, GameObject>();
    //对象池 当前实例化出来的界面的集合
    public Dictionary<string, GameObject> CurOpenUIGroup = new Dictionary<string, GameObject>();


    public const string LoginUI = "LoginUI";
    public const string ModeChooseUI = "ModeChooseUI";
    public const string CheckListUI = "CheckListUI";
    public const string CheckItemUI = "CheckItemUI";
    public const string WashUI = "WashUI";
    public const string DetectionUI = "DetectionUI";
    public const string DetectionItemUI = "DetectionItemUI";
    public const string CheckPatientUI = "CheckPatientUI";
    public const string PrintUI = "PrintUI";
    public const string BloodSampling1UI = "BloodSampling1UI";
    //初始化方法
    public void Start()
    {
        _mainCanvas = transform.Find("MainCanvas").GetComponent<Canvas>();
    }

    //public void OpenUI(EEvet evt,params object[] args)
    //{

    //}
    //开启UI
    //public void OpenUI(string uiname )
    //{
     
        
    //    //先全部移除
    //    foreach (var uidate in UIGroup)
    //    {
    //    MoveUIPos(uidate.Value, false);
          
    //    }
    //    MoveUIPos(UIGroup[uiname], true);
        
    //}
    ////移出UI
    //void MoveUIPos(GameObject ui, bool isLeft)
    //{
    //    if (!isLeft)
    //    {
    //        ui.SetActive(false);
    //    }
    //    else
    //    {
    //        ui.SetActive(true);
    //    }
    //}
    //string curMoveUI_name = "";
    ////关闭UI
    //public void CloseUI(string _uiname)
    //{
    //    //MoveUIPos(UIGroup[_uiname], false);
    //    curMoveUI_name = _uiname;
    //}


}
