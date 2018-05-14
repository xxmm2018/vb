using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public abstract class VIKState : VState<VIKContrl>
{
    protected List<AvatarIKGoal> iks;

    public VIKState(EStateIK id, VIKContrl owner) : base((int)id, owner)
    {
        iks = new List<AvatarIKGoal>();
    }

    public override void Enter()
    {
        base.Enter();
        m_Owner.weight = 1;
        m_Owner.lists.AddRange(iks);
    }
    public override void Handle(EEvet evt, params object[] args)
    {
        if (evt == EEvet.Envisage)
        {
            Machine.TranslateToState((int)EStateIK.Envisage);
        }
    }
    public override void Update()
    {

    }
    public override void Exit()
    {
        base.Exit();
        m_Owner.lists.Clear();
        m_Owner.weight = 0;
        m_Owner.hands.Clear();
        m_Owner.isTrace = false;
    }
}

public class VIKStateHandOver : VIKState
{
    float timer;
    //递交动画时间
    float handOverTime = 2;
    public VIKStateHandOver(VIKContrl owner) : base(EStateIK.HandOver, owner)
    {
        m_name = "递交";
        iks.Add(AvatarIKGoal.LeftHand);
        iks.Add(AvatarIKGoal.RightHand);
     
    }
    public override void Enter()
    {
        base.Enter();
        m_Owner.hands.Add(GameObject.Find("HandleOverL").transform);
        m_Owner.hands.Add(GameObject.Find("HandleOverR").transform);
        m_Owner.isTrace = false;
    }
    public override void Update()
    {
        base.Update();
        timer += Time.deltaTime;
        if (timer > handOverTime)
        {
            Machine.TranslateToState((int)EStateIK.Envisage);
            VEventManager.Instance.SendCommand(EEvet.Inspection);
            m_Owner.transform.rotation = Quaternion.Euler(0, 140, 0);
            m_Owner.isTrace = true;
            timer = 0;
        }

    }
}

public class VIKStateDiagnose : VIKState
{
 
    public VIKStateDiagnose(VIKContrl owner) : base(EStateIK.Diagnose, owner)
    {
        m_name = "诊断";
        iks.Add(AvatarIKGoal.RightHand);
    }
    public override void Enter()
    {
        base.Enter();
        m_Owner.weight = 0.8f;
        m_Owner.hands.Add(GameObject.Find("SingleHandle").transform);
        m_Owner.isTrace = true;
    }
    public override void Handle(EEvet evt, params object[] args)
    {
        //诊断时控制头的IK动画
        base.Handle(evt, args);
        if (evt == EEvet.Diagnose)
        {
            m_Owner.head = GameObject.Find(args[0].ToString()).transform;

        }
    }
    public override void Update()
    {
  
    }
    public override void Exit()
    {
        base.Exit();
        m_Owner.isTrace = false;
    }

}

public class VIKStateEnvisage : VIKState
{
    public VIKStateEnvisage(VIKContrl owner) : base(EStateIK.Envisage, owner)
    {
        m_name = "注视前面";
    }
    public override void Handle(EEvet evt, params object[] args)
    {
        /*递交命令
         * 格式：类型，手1位置，手2位置，可否转头，追踪位置
         * 
         */
        if (evt == EEvet.HandOver)
        {
            Machine.TranslateToState((int)EStateIK.HandOver);
        }
        if (evt == EEvet.Diagnose)
        {
            Machine.TranslateToState((int)EStateIK.Diagnose);

        }
        if (evt == EEvet.Nod)
        {
            Machine.TranslateToState((int)EStateIK.Nod);
        }
        //诊断时控制头的IK动画
        if (evt == EEvet.Envisage)
        {
            if (args.Length > 0)
            {
                m_Owner.isTrace = (bool)args[0];
                m_Owner.head = GameObject.Find("Eye").transform;
            }

        }
    }



    public override void Exit()
    {
        m_Owner.lists.Clear();
        m_Owner.weight = 0;
    }
    public override void Update()
    {
        base.Update();
        
       
    }

}

public class VIKStateNod : VIKState
{
    float timer;
    float nodTimer = .5f;
    public VIKStateNod(VIKContrl owner) : base(EStateIK.Nod, owner)
    {
        m_name = "点头";
    }
    public override void Enter()
    {
        base.Enter();
        m_Owner.head = GameObject.Find("NodPoint").transform;
        m_Owner.isTrace = true;
    }
    public override void Update()
    {
        base.Update();
        timer += Time.deltaTime;
        if (timer>nodTimer)
        {
            Machine.TranslateToState((int)EStateIK.Envisage);
            VEventManager.Instance.SendCommand(EEvet.End);
            timer = 0;
        }
    }
    public override void Exit()
    {
        base.Exit();
        m_Owner.isTrace = false;
    }
}

//状态  递交，打招呼，诊断，点头，正视
public enum EStateIK : int
{
    Envisage,   //正常
    HandOver,       //递交
    Diagnose,     //诊断
    Nod,         //点头
}