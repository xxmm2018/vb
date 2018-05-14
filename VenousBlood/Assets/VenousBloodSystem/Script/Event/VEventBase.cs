using UnityEngine;
using System.Collections;
using System;
using System.Reflection;
using System.Collections.Generic;

public abstract class VEventBase<T> : MonoBehaviour
{

    public VStateMachince m_SM;
    private void OnEnable()
    {
        VEventManager.handle += VEventManager_handle;
    }
    private void OnDisable()
    {
        VEventManager.handle -= VEventManager_handle;
    }

    private void Start()
    {
        InitMachince();

        Init();
    }

    protected void Init()
    {
        Type[] types = Assembly.GetExecutingAssembly().GetTypes();
        List<VStateBase> takes = new List<VStateBase>();

        object[] parms = new object[1];
        parms[0] = this;
        foreach (var item in types)
        {

            if (typeof(T) == item.BaseType)
            {
                VStateBase t = Activator.CreateInstance(item, parms) as VStateBase;
                if (t.ID == 0)
                {
                    m_SM = new VStateMachince(t);
                }
                else
                {
                    takes.Add(t);
                }
            }
        }
        foreach (var item in takes)
        {
            m_SM.RegState(item);
        }
    }
    protected abstract void InitMachince();
    void VEventManager_handle(EEvet evt, object[] args)
    {
        m_SM.Handle(evt, args);
    }

    protected virtual void Update()
    {
        m_SM.Upadte();
    }

}
