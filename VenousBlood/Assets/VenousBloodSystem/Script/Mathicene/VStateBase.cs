using UnityEngine;
using System.Collections;

public abstract class VStateBase {

	public int ID { get; private set; }
    public VStateMachince Machine;
    public string m_name;
 
    public VStateBase(int id)
    {
        ID = id;
    }
    public abstract void Handle(EEvet evt, params object[] args);
    public abstract void Update();
    public virtual void Exit()
    {
       VMessageOut._instance.OutMessage("离开" + m_name);
    }
    public virtual void Enter()
    {
        VMessageOut._instance.OutMessage("进入" + m_name);

    }
  
}

public abstract class VState<T> : VStateBase
{
    protected T m_Owner;
    public VState(int id,T owner) : base(id)
    {
        m_Owner = owner;

    }
}
