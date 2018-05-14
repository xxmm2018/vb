using UnityEngine;
using System.Collections;
using System;

public class Eventsys : MonoBehaviour
{
    static Eventsys inst;
    public static Eventsys instance
    {
        get { return inst; }
       
    }

    void Awake()
    {
        inst = this;
    }
    public delegate void MyEventHandler();
    public event MyEventHandler MyEvent;
    public event MyEventHandler Event;
    public event MyEventHandler Event1;
    public event MyEventHandler Event2;
    public event MyEventHandler Event3;


    //等待Instrumenttray添加元素完成后再执行
    public void Func4()
    {
        if (Event!=null)
        {
            Event();
        }
      
    }
    //等待CheckListUIMgr添加元素完成后在执行
    public void EventFunc()
    {
        if (Event1!=null)
        {
            Event1();
        }

    }
  
    public void Func()
    {
        if (MyEvent!=null)
        {
            MyEvent();
        }

    }
    public delegate void MyEventHandler1(int i);
    public event MyEventHandler1 MyEvent2;
    /// <summary>
    /// command  类型  参数  可变参数
    /// </summary>
    /// <param name="i"></param>
    /// send(load)
    /// kehuduan send(load,user,pass)
    /// fuwuduan if(true)send(model)flase  send(load flase)
    public void Func3(EEvet evt,params object[] args)
    {

        //switch (evt)
        //{
        //    case EEvet.Load:
        //        GlobalMgr.Instance.UIMgr.OpenUI(UIMgr.LoginUI);
        //        break;
        //    case EEvet.Model:
        //        GlobalMgr.Instance.UIMgr.OpenUI(UIMgr.ModeChooseUI);
        //        break;
        //    case EEvet.Check:
        //        GlobalMgr.Instance.UIMgr.OpenUI(UIMgr.CheckListUI);
        //        break;
        //    case EEvet.Object:
        //        GlobalMgr.Instance.UIMgr.OpenUI(UIMgr.CheckItemUI);
        //        break;
        //    case EEvet.Wash:
        //        GlobalMgr.Instance.UIMgr.OpenUI(UIMgr.WashUI);
        //        break;
        //    case EEvet.Mask:
        //        GlobalMgr.Instance.UIMgr.OpenUI(UIMgr.DetectionUI);
        //        break;
        //    case EEvet.Choice:
        //        GlobalMgr.Instance.UIMgr.OpenUI(UIMgr.DetectionItemUI);
        //        break;
        //    case EEvet.Inspection:
        //        GlobalMgr.Instance.UIMgr.OpenUI(UIMgr.CheckPatientUI);
        //        break;
        //    case EEvet.CheckPatient:
        //        GlobalMgr.Instance.UIMgr.OpenUI(UIMgr.PrintUI);
        //        break;
        //    case EEvet.Certificate:
        //        break;
        //    case EEvet.Label:
        //        break;
        //    case EEvet.Tourniquet:
        //        GlobalMgr.Instance.UIMgr.OpenUI(UIMgr.BloodSampling1UI);
        //        break;
        //    case EEvet.Vessels:
        //        break;
        //    case EEvet.Sterilize:
        //        break;
        //    case EEvet.Dry:
        //        break;
        //    case EEvet.Puncture:
        //        break;
        //    case EEvet.ContinuousBloodCollection:
        //        break;
        //    case EEvet.LooseTourniquet:
        //        break;
        //    case EEvet.BloodCollection:
        //        break;
        //    case EEvet.ShakeUp:
        //        break;
        //    case EEvet.Pulling:
        //        break;
        //    case EEvet.Control:
        //        break;
        //    case EEvet.BeginVeins:
        //        break;
        //    case EEvet.Veins:
        //        break;
        //    case EEvet.EndVeins:
        //        break;
        //    case EEvet.Say:
        //        break;
        //    case EEvet.Quiet:
        //        break;
        //    case EEvet.Happy:
        //        break;
        //    case EEvet.Idle:
        //        break;
        //    case EEvet.Move:
        //        break;
        //    case EEvet.Sit:
        //        break;
        //    case EEvet.HandOver:
        //        break;
        //    case EEvet.Hi:
        //        break;
        //    case EEvet.Diagnose:
        //        break;
        //    case EEvet.Nod:
        //        break;
        //    case EEvet.Envisage:
        //        break;
        //    case EEvet.End:
        //        break;
        //    default:
        //        break;
        }
    }
   


