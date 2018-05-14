using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class VIKContrl : VEventBase<VIKState>
{

    public Animator ani;
    public List<AvatarIKGoal> lists = new List<AvatarIKGoal>();
    public float weight;
    public List<Transform> hands = new List<Transform>();
    public bool isTrace;
    public Transform head;

    protected override void InitMachince()
    {
        ani = GetComponent<Animator>();
        head = GameObject.Find("Eye").transform;
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (ani)
        {
            if (isTrace)
            {
                ani.SetLookAtPosition(head.position);
                ani.SetLookAtWeight(1, 0.1f, .8f, .2f, 0.2f);
            }
            if (lists.Count == 0)
                return;
            //权重
            for (int i = 0; i < lists.Count; i++)
            {
                ani.SetIKPositionWeight(lists[i], weight);
                ani.SetIKRotationWeight(lists[i], weight);
            }
            //位置
            for (int i = 0; i < lists.Count; i++)
            {
                ani.SetIKPosition(lists[i], hands[i].position);
                ani.SetIKRotation(lists[i], hands[i].rotation);
            }



        }
    }
}
