using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public abstract class VExpState : VState<VExpression> {
    protected int m_part;
    protected float m_curValue;

    protected const int NORMAL = 6;   //正常
    protected const int ACHE = 5;     //疼痛
    protected const int TENSION = 7;  //紧张
    protected const int SMILE = 4;    //微笑
    protected const int SAY = 3;      //说话


    //////////////////////////
    protected int min;
    protected int max;
    protected bool IsApproximate;//是否接近表情结束
    protected float timer2 = 0;
    protected float count = 5;
    protected float timer3;
    protected float length;
    /// ////////////////////

    protected float timer;
   protected  float timerOffset = 3;
    public VExpState(EStateId id, VExpression owner) : base((int)id, owner)
    {

    }
    public override void Exit()
    {
        base.Exit();
        m_Owner.skin.SetBlendShapeWeight(m_part, 0);
    }
    public override void Handle(EEvet evt, params object[] args)
    {
       
    }
    public override void Update()
    {
        Exp(length);
        if (Mathf.Abs(timer - timerOffset) < 0.5)
        {
            IsApproximate = true;
        }
        else
        {
            IsApproximate = false;
        }
        if (ID == (int)EStateId.Nomal)
            return;
        timer += Time.deltaTime;
        if (timer>timerOffset)
        {
            Machine.TranslateToState((int)EStateId.Nomal);
            timer = 0;
        }
    }
    protected virtual void Exp(float length)
    {
        if (!IsApproximate)
        {
            m_curValue = Mathf.PingPong(Time.time, length) * min + max;
            m_Owner.skin.SetBlendShapeWeight(m_part, m_curValue);
        }
        else
        {
            for (int i = 0; i < count; i++)
            {
                timer2 += Time.deltaTime;
                if (timer2 >= 0.1)
                {
                    m_curValue = m_curValue - (m_curValue / count);
                    m_Owner.skin.SetBlendShapeWeight(m_part, m_curValue);
                    timer2 = 0;
                }
            }
        }
    }
}

public class VExpStateTension : VExpState
{

    public VExpStateTension(VExpression owner) : base(EStateId.Tension, owner)
    {
        timerOffset = 6;
        min = 100;
        max = 60;
        m_name = "紧张";
        m_part = TENSION;
        length = 0.1f;
    }
    protected override void Exp(float length)
    {
        base.Exp(length);
    }
}
public class VExpStateCry : VExpState
{
    public VExpStateCry(VExpression owner) : base(EStateId.Ache, owner)
    {
        timerOffset = 3;
        min = 260;
        max = 50;
        m_name = "疼痛";
        m_part = ACHE;
        length = 0.3f;
    }
    protected override void Exp(float length)
    {
        base.Exp(length);
    }
}

public class VExpStateHappy : VExpState
{
    public VExpStateHappy(VExpression owner) : base(EStateId.Smile, owner)
    {
        timerOffset = 5;
        min = 25;
        max = 50;
        m_name = "微笑";
        m_part = SMILE;
        length = 2;
    }
    protected override void Exp(float length)
    {
        base.Exp(length);
    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("xixixi");
    }
}

public class VExpStateSay : VExpState
{
    public VExpStateSay(VExpression owner) : base(EStateId.Say, owner)
    {
        min = 180;
        max = 10;
        timerOffset = 5;
        m_name = "说话";
        m_part = SAY;
        length = 0.5f;
    }

    protected override void Exp(float length)
    {
        base.Exp(length);
    }
    public override void Handle(EEvet evt, params object[] args)
    {
        if (evt==EEvet.CheckPatient)
        {
            if (evt == (EEvet)ID)
            {
                if (args.Length < 1)
                    return;
                if ((bool)args[0])
                {
                    Machine.TranslateToState((int)EStateId.Nomal);
                }
            }
        }
    }
}

public class VExpStateNomal : VExpState
{
    public VExpStateNomal(VExpression owner) : base(EStateId.Nomal, owner)
    {
        m_name = "正常";
        m_part = NORMAL;
        min = 50;
        max = 0;
    }
    public override void Enter()
    {
        base.Enter();
        timer3 = 0;
        length = 2;
    }
    protected override void Exp(float length)
    {
        timer3 += Time.deltaTime;
        m_curValue = Mathf.PingPong(timer3, length) * min ;
        m_Owner.skin.SetBlendShapeWeight(m_part, m_curValue);
    }

    public override void Handle(EEvet evt, params object[] args)
    {
        if (evt == EEvet.ContinuousBloodCollection)
        {
            Machine.TranslateToState((int)EStateId.Ache);
        }
        if (evt == EEvet.CheckPatient)
        {
            Machine.TranslateToState((int)EStateId.Say);
        }
        if (evt == EEvet.Pulling)
        {
            Machine.TranslateToState((int)EStateId.Smile);
        }
        if (evt == EEvet.Tourniquet)
        {
            Machine.TranslateToState((int)EStateId.Tension);
        }
    }
}

//状态  正常，微笑，疼痛，紧张，说话
public enum EStateId : int
{    
    Nomal,
    Smile,
    Ache,
    Tension,
    Say
}
