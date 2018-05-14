using UnityEngine;
using System.Collections;
using System;

public class VAnimation : VEventBase<VAniState>
{
    [HideInInspector]
    public Animator ani;
    [HideInInspector]
    public NavMeshAgent nav;
    public float minDistance;
    public float speed = 5;
    protected override void InitMachince()
    {
        ani = GetComponentInChildren<Animator>();
        nav = GetComponent<NavMeshAgent>();
    }

}
