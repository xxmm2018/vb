using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GlobalMgr : MonoBehaviour
{
   
    List<string > items = new List<string >();//物品列表
    List<string> instrumenttray = new List<string>();//器械盘列表
    List<string> checkList = new List<string>();//器械盘中被选中的物品列表
    public float num { get;  set; }//需要采集的次数

    public List<string> Items//工具箱里的物品
    {
        get { return items; }
        set { items = value;}
    }
    //
    public List<string> Instrumenttray//当前器械盘里的物品
    {
        get { return instrumenttray; }
        set { instrumenttray = value;}
    }
    public List<string> CheckList
    {
        get { return checkList; }
        set { checkList = value;}
    }

    static GlobalMgr Inst;
    public static GlobalMgr Instance
    {
        get
        {
            return Inst;
        }
    }

    private void Awake()
    {


       
         Inst = this;
         UIMgr = gameObject.AddComponent<UIMgr>();
         LoginUIMgr = gameObject.AddComponent<LoginUIMgr>();
        //ModeChooseUIMgr = gameObject.AddComponent<ModeChooseUIMgr>();
         CheckListUIMgr = gameObject.AddComponent<CheckListUIMgr>();
        //CheckItemUIMgr = gameObject.AddComponent<CheckItemUIMgr>();
         WashUIMgr = gameObject.AddComponent<WashUIMgr>();
        //DetectionUIMgr = gameObject.AddComponent<DetectionUIMgr>();
        //DetectionItemUIMgr = gameObject.AddComponent<DetectionItemUIMgr>();
        //CheckPatientUIMgr = gameObject.AddComponent<CheckPatientUIMgr>();
         PrintUIMgr = gameObject.AddComponent<PrintUIMgr>();
        // BloodSampling1UIMgr = gameObject.AddComponent<BloodSampling1UIMgr>();
        // ParameterMgr = gameObject.AddComponent<ParameterMgr>();
        //    //Eventsys = gameObject.AddComponent<Eventsys>();
        //TimerMgr = gameObject.AddComponent<TimerMgr>();
        num = 2;

    }

    void Start()
    {
        VEventManager.Instance.SendCommand(EEvet.Load);
    }





    //UI管理的属性
    public UIMgr UIMgr
    {
        get;
        private set;
    }
    public LoginUIMgr LoginUIMgr
    {
        get;
        private set;
    }
    public ModeChooseUIMgr ModeChooseUIMgr
    {
        get;
        private set;
    }
    public CheckListUIMgr CheckListUIMgr
    {
        get;
        private set;
    }
    public CheckItemUIMgr CheckItemUIMgr
    {
        get;
        private set;
    }

    public WashUIMgr WashUIMgr
    {
        get;
        private set;
    }
    public DetectionUIMgr DetectionUIMgr
    {
        get;
        private set;
    }
    public DetectionItemUIMgr DetectionItemUIMgr
    {
        get;
        private set;
    }


    public BloodSampling1UIMgr BloodSampling1UIMgr
    {
        get;
        private set;
    }
    public CheckPatientUIMgr CheckPatientUIMgr
    {
        get;
        private set;
    }
    public PrintUIMgr PrintUIMgr
    {
        get;
        private set;
    }


 

   
}
