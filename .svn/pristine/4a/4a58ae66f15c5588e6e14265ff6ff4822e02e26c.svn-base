using UnityEngine;
using System.Collections;
using System;

public abstract class VAniState : VState<VAnimation>
{
    public VAniState(int id, VAnimation owner) : base(id, owner)
    {
    }

    public override void Handle(EEvet evt, params object[] args)
    {

    }

    public override void Update()
    {

    }
}

public class VAniStateIdle : VAniState
{
    public VAniStateIdle(VAnimation owner) : base((int)EStateAni.Idle, owner)
    {
        m_name = "站立";
    }

    public override void Handle(EEvet evt, params object[] args)
    {
        //开始检验的时候，让模型移动到指定的位置
        if (evt == EEvet.Choice)
        {
            Machine.TranslateToState((int)EStateAni.Move);
        }
    }
    public override void Update()
    {
        base.Update();
        if (m_Owner.ani.GetBool("Sit"))
        {
            Machine.TranslateToState((int)EStateAni.Sit);
        }
    }

}
public class VAniStateMove : VAniState
{
    float timer;
    Vector3 targetPos;
    public VAniStateMove(VAnimation owner) : base((int)EStateAni.Move, owner)
    {
        m_name = "移动";
    }
    public override void Enter()
    {
        base.Enter();
        targetPos = GameObject.Find("Sit").transform.position;
        m_Owner.nav.destination = targetPos;
        m_Owner.ani.SetFloat("Speed", m_Owner.speed);
    }
    public override void Exit()
    {
        base.Exit();
        m_Owner.ani.SetFloat("Speed", 0);
        m_Owner.ani.SetBool("Sit", true);
    }
    public override void Update()
    {

        if (Vector3.Distance(m_Owner.transform.position, targetPos) < m_Owner.minDistance)
        {
          
            Machine.TranslateToState((int)EStateAni.Idle);
        }
    }
 
}
public class VAniStateSit : VAniState
{
    public VAniStateSit(VAnimation owner) : base((int)EStateAni.Sit, owner)
    {
        m_name = "坐";
    }
    public override void Enter()
    {
        base.Enter();
        VEventManager.Instance.SendCommand(EEvet.HandOver);
    }
    public override void Exit()
    {
        base.Exit();
        m_Owner.ani.SetBool("Sit", false);
    }
    public override void Handle(EEvet evt, params object[] args)
    {
        if (evt == EEvet.End)
        {
            Machine.TranslateToState((int)EStateAni.Idle);
        }
    }
    public override void Update()
    {
        base.Update();  
        m_Owner.transform.rotation = Quaternion.Euler(0, 180, 0);
        //坐下之后提交


    }

}

public enum EStateAni
{
    Idle,
    Move,
    Sit
}
