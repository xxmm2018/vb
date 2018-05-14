using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class VStateMachince  {

    private SortedList<int, VStateBase> m_states;
    private VStateBase m_curState;

    public VStateBase CurState
    {
        get
        {
            return m_curState;
        }
    }
    /// <summary>
    /// 构造函数初始化
    /// </summary>
    /// <param name="beginState"></param>
    public VStateMachince(VStateBase beginState)
    {
        m_states = new SortedList<int, VStateBase>();
        m_curState = beginState;
        RegState(CurState);
        CurState.Enter();
    }
    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="state"></param>
    public  void RegState(VStateBase state)
    {
        int id = (int)(state.ID);
        if (m_states.ContainsKey(id))
        {
            Debug.Log(id+"已存在！");
            return;
        }
        m_states.Add(id,state);
        state.Machine = this;
    }
    /// <summary>
    /// 设置新的状态
    /// </summary>
    /// <param name="id">目标ID</param>
    /// <param name="args"></param>
    public void TranslateToState(int id)
    {
        int nId = (int)id;
        if (!m_states.ContainsKey(nId))
        {
            //Debug.Log("VStateMachine, 切换状态时没有找到目标状态"+ id.ToString());
            VMessageOut._instance.OutMessage("VStateMachine, 切换状态时没有找到目标状态" + id.ToString());
            return;
        }
        m_curState.Exit();
        m_curState = m_states[nId];
        m_curState.Enter();
    }
    public void Upadte()
    {
        m_curState.Update();
    }
    /// <summary>
    /// 切换条件（事件触发）
    /// </summary>
    /// <param name="evt"></param>
    /// <param name="args"></param>
    public void Handle(EEvet evt, params object[] args)
    {
         m_curState.Handle(evt,args);
    }
}
