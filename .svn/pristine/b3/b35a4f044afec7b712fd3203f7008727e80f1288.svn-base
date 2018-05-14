using UnityEngine;
using System.Collections;
using System;

public class VExpression : VEventBase<VExpState>
{
    public SkinnedMeshRenderer skin;
    protected override void InitMachince()
    {        
    }


    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.A))
        {
            //正常
            //VEventManager.Instance.SendCommand();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            //疼痛  连入采血管
            VEventManager.Instance.SendCommand(EEvet.ContinuousBloodCollection);

        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            //微笑  按压
            VEventManager.Instance.SendCommand(EEvet.Control);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            //紧张   待干     
            VEventManager.Instance.SendCommand(EEvet.Dry);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            //说话    核对患者 
            VEventManager.Instance.SendCommand(EEvet.CheckPatient);
        }
    }

}
