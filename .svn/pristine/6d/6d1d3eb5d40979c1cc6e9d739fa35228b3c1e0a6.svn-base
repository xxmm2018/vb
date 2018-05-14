using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public abstract class VTakeState : VState<VTake>
{

    public VTakeState(int id, VTake owner) : base(id, owner)
    {

    }

    public override void Handle(EEvet evt, params object[] args)
    {
        if (evt == (EEvet)ID)
        {
            Machine.TranslateToState(ID + 1);
        }
    }
    public override void Update()
    {
        //VMessageOut._instance.OutMessage("当前状态是:"+m_name);
    }
    public override void Enter()
    {
        base.Enter();
        m_Owner.SetModleName(m_name);
        VServer._instance.ServerSend((EEvet)ID);
    }
}

/// <summary>
/// 登陆
/// 开机状态下，红外检测到有人坐在操作位，开启人脸识别
/// 1. 识别认证成功，则登录系统2. 识别失败，再次识别3. 采用其他登录方式登录后，登录系统，取消识别
/// 在平板界面输入用户名密码，确定登录
/// 登录手机app，扫平板中二维码，登录系统
/// 用户密码校验成功，登录系统；验证失败提示错误，重新输入
/// 人脸识别画面
/// 登录界面
/// 登录成功
/// </summary>
public class VTakeStateLoad : VTakeState
{
    public VTakeStateLoad(VTake owner) : base((int)EEvet.Load, owner)
    {
        m_name = "登陆";
    }
    public override void Enter()
    {
        base.Enter();
        //BaiduFace.instance.OpenFace();
    }
    public override void Handle(EEvet evt, params object[] args)
    {

        if (evt == EEvet.Load)
        {
            switch (args.Length)
            {
                case 1:
                    m_Owner.user.text = BaiduFace.instance.User;
                    Check((bool)args[0]);
                    break;
                case 2:
                    Check(args[0].ToString(), args[1].ToString());
                    break;
                default:
                    break;
            }
        }
    }
    void Check(bool state)
    {
        if (state)
        {
            Machine.TranslateToState((int)EEvet.Model);
        }
    }
    void Check(string user, string psw)
    {
        if (user == "1" && psw == "1")
        {
            Machine.TranslateToState((int)EEvet.Model);
        }
        else
        {
            VServer._instance.ServerSend(EEvet.Load, false);
        }

    }

    public override void Update()
    {

    }
    public override void Exit()
    {
        base.Exit();
        BaiduFace.instance.StopFace();
    }
}
/// <summary>
/// 模块选择
/// 选择静脉采血模块，选择训练模式；默认为教学模式
///  进入检验单选择步骤
/// 采血大厅图片+护士站图片和
/// </summary>
public class VTakeStateModel : VTakeState
{
    public VTakeStateModel(VTake owner) : base((int)EEvet.Model, owner)
    {
        m_name = "模块选择";
    }
    public override void Handle(EEvet evt, params object[] args)
    {
        if (evt == EEvet.Model)
        {
            if (args.Length > 0)
            {
                if ((int)args[0] == 1)
                {
                    Machine.TranslateToState((int)EEvet.Check);

                }
            }
        }
    }

    public override void Update()
    {

    }
}
/// <summary>
/// 检查单
/// 选择一个检验项目，通过标签，切换查看不同检验单。点击确定按钮进入训练
/// 确定后进入用物状态检测步骤
/// 采血大厅图片
/// 检验申请单界面
/// 检验器械盘内器械是否备齐，如果检测用物齐备，则跳过该步骤
/// </summary>
public class VTakeStateCheck : VTakeState
{
    public VTakeStateCheck(VTake owner) : base((int)EEvet.Check, owner)
    {
        m_name = "检查单";
    }

}
/// <summary>
/// 用物检查
/// 系统检测缺少器械
/// 提示器械盘内所缺少的物品
/// 检测器械盘内所有用物是否存在
/// 采血大厅图片
/// 用物检测界面
/// 1. 系统用物检测齐备2. 系统检测到缺少用物，在提示界面点击了“确定”
/// </summary>
public class VTakeStateObject : VTakeState
{
    public VTakeStateObject(VTake owner) : base((int)EEvet.Object, owner)
    {
        m_name = "用物检查";
    }

}
//洗手
public class VTakeStateWash : VTakeState
{
    public VTakeStateWash(VTake owner) : base((int)EEvet.Wash, owner)
    {
        m_name = "洗手";
    }

}
//带口罩手套
public class VTakeStateMask : VTakeState
{
    public VTakeStateMask(VTake owner) : base((int)EEvet.Mask, owner)
    {
        m_name = "带口罩手套";
    }

}
//选择所需用物
public class VTakeStateChoice : VTakeState
{
    public VTakeStateChoice(VTake owner) : base((int)EEvet.Choice, owner)
    {
        m_name = "选择所需用物";
    }


}
//收取检查申请单
public class VTakeStateInspection : VTakeState
{
    public VTakeStateInspection(VTake owner) : base((int)EEvet.Inspection, owner)
    {
        m_name = "检查申请单";
    }
    public override void Enter()
    {
        base.Enter();
    }

}
//核对患者
public class VTakeStateCheckPatient : VTakeState
{
    public VTakeStateCheckPatient(VTake owner) : base((int)EEvet.CheckPatient, owner)
    {
        m_name = "核对患者";
    }

    public override void Enter()
    {
        base.Enter();
        //开启追踪
        FaceDetection.instance.OpenCam();
        VEventManager.Instance.SendCommand(EEvet.Envisage, true);
        m_Owner.StarSpeak(true);
    }
    public override void Handle(EEvet evt, params object[] args)
    {
        if (evt == (EEvet)ID)
        {
            if (args.Length < 1)
                return;
            if ((bool)args[0])
            {
                Machine.TranslateToState((int)EEvet.Certificate);
            }
        }
    }
    public override void Exit()
    {
        base.Exit();
        m_Owner.StarSpeak(false);
    }

}
//打印条形码
public class VTakeStateCertificate : VTakeState
{
    public VTakeStateCertificate(VTake owner) : base((int)EEvet.Certificate, owner)
    {
        m_name = "打印条形码";
    }

}
//粘贴标签
public class VTakeStateLabel : VTakeState
{
    public VTakeStateLabel(VTake owner) : base((int)EEvet.Label, owner)
    {
        m_name = "粘贴标签";
    }


}

/// <summary>
/// 扎止血带
/// 1、触发评估血管提示及页面
/// 2、是否扎压脉带，位置是否在正确范围。 扎止血带的时长
/// 3、患者坐位，看着操作者；视线焦点随操作者移动
/// 4、播报提示语
/// </summary>
public class VTakeStateTourniquet : VTakeState
{
    public VTakeStateTourniquet(VTake owner) : base((int)EEvet.Tourniquet, owner)
    {
        m_name = "扎止血带";
    }
    public override void Enter()
    {
        base.Enter();
        VEventManager.Instance.SendCommand(EEvet.Diagnose);
    }
    public override void Handle(EEvet evt, params object[] args)
    {
        if (evt == EEvet.Tourniquet && (bool)args[0] & (bool)args[1])
        {
            Machine.TranslateToState((int)EEvet.Vessels);
        }
    }

    public override void Exit()
    {
        base.Exit();
        m_Owner.BeginTimer(true);
    }
}
/// <summary>
/// 确认血管
///1、 该步骤持续3s后，自动跳转至消毒步骤
///2、 扎止血带计时，解剖图，易选的静脉名称
/// </summary>
public class VTakeStateVessels : VTakeState
{
    float timer;
    public VTakeStateVessels(VTake owner) : base((int)EEvet.Vessels, owner)
    {
        m_name = "确认血管";
    }

    public override void Handle(EEvet evt, params object[] args)
    {

    }

    public override void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3)
        {
            Machine.TranslateToState((int)EEvet.Sterilize);
            timer = 0;
        }
    }
}
/// <summary>
/// 消毒步骤 
///1、 2次消毒后进入穿刺
///2、 识别1. 是否消毒，2. 消毒位置，3. 消毒面积，4. 消毒次数。
///3、 扎止血带计时，提示语，消毒示意图
///4、进入消毒完成步骤
/// </summary>
public class VTakeStateSterilize : VTakeState
{
    //消毒面积合格的参考值
    private float target = 0.8f;
    private int count = 2;
    public VTakeStateSterilize(VTake owner) : base((int)EEvet.Sterilize, owner)
    {
        m_name = "消毒";
    }
    public override void Enter()
    {
        base.Enter();
        //播放提示语
        VAudioMgr._instance.PlayAudio(Camera.main.transform.position, "shengji_01", 1);
    }

    public override void Handle(EEvet evt, params object[] args)
    {
        //消毒完成条件1、是否消毒，消毒位置，面积，次数  
        //数组args   0代表是否消毒 1代表位置  2代表面积  3代表次数
        if (evt == EEvet.Sterilize)
        {
            if ((bool)args[0])
            {
                if ((bool)args[1])
                {
                    if ((float)args[2] > target)
                    {
                        if ((int)args[3] >= count)
                        {
                            //消毒完成进入待干
                            Machine.TranslateToState((int)EEvet.Dry);
                        }
                        else
                        {
                            VMessageOut._instance.OutMessage("这是第" + args[3] + "次消毒，次数少于" + count + "次，请再次消毒");
                        }
                    }
                    else
                    {
                        VMessageOut._instance.OutMessage("消毒失败，有留白");
                    }
                }
                else
                {
                    VMessageOut._instance.OutMessage("消毒位置不对");
                }
            }
            else
            {
                VMessageOut._instance.OutMessage("没有开始消毒");
            }
        }
    }


}
/// <summary>
/// 待干
/// 进入穿刺步骤
/// 扎止血带计时，提示语
/// </summary>
public class VTakeStateDry : VTakeState
{
    float timer;
    public VTakeStateDry(VTake owner) : base((int)EEvet.Dry, owner)
    {
        m_name = "待干";
    }

    public override void Enter()
    {
        base.Enter();
        VAudioMgr._instance.PlayAudio(Camera.main.transform.position, "shengji_01");
    }

    public override void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3)
        {
            Machine.TranslateToState((int)EEvet.Puncture);
            timer = 0;
        }
        // throw new NotImplementedException();
    }
    public override void Exit()
    {
        base.Exit();
        //关闭追踪
        FaceDetection.instance.FacedetectionCloseCam();
    }
}
/// <summary>
/// 穿刺
/// 回血
/// 识别1. 穿刺针与皮肤的角度，2. 进针位置是否在血管，3. 进针深度，达到某一深度后，刺破血管。回血后，不需要再进针
/// 患者头转向左侧
/// 扎止血带计时，提示语
/// 回血，且穿刺针在穿刺位置，回缩量几乎无变化
/// </summary>
public class VTakeStatePuncture : VTakeState
{
    //进针角度
    private float angle = 10;
    //深度
    private float minDepth = 1;//针尖进入皮肤的底限cm
    private float maxDepth = 5;//针尖进入皮肤的高限cm





    public VTakeStatePuncture(VTake owner) : base((int)EEvet.Puncture, owner)
    {
        m_name = "穿刺";
    }
    public override void Enter()
    {
        base.Enter();
        VEventManager.Instance.SendCommand(EEvet.Diagnose, "leftPoint");

    }
    public override void Handle(EEvet evt, params object[] args)
    {
        //完成条件1.进针位置是否在血管 ，2.穿刺针与皮肤的角度 ，3. 进针深度，达到某一深度后，刺破血管。回血后，不需要再进针
        //args参数说明 0针与皮肤角度，1针是否在血管，2进针深度，3回血
        if (evt == EEvet.Puncture)
        {
            if ((bool)args[0])//针是否在指定的范围
            {
                //VMessageOut._instance.OutMessage(args[0].ToString());
                if ((float)args[1] < angle)//角度是否正确
                {
                    if (minDepth < (float)args[2] && (float)args[2] < maxDepth)//深度是否在范围内
                    {
                        if ((bool)args[3])//回血
                        {
                            Machine.TranslateToState((int)EEvet.ContinuousBloodCollection);
                        }
                        else
                        {
                            VMessageOut._instance.OutMessage("未回血");
                        }
                    }
                    else
                    {
                        VMessageOut._instance.OutMessage("深度不在指定范围" + args[2].ToString());

                    }
                }
                else
                {
                    VMessageOut._instance.OutMessage("角度不正确" + args[1].ToString());
                }
            }
            else
            {
                VMessageOut._instance.OutMessage("针不在血管");
            }
        }
    }

    public override void Update()
    {
        //throw new NotImplementedException();
    }
}
/// <summary>
/// 连入采血管
/// 连入采血管后，显示血流入试管，虚实同步
/// 检测2个不同的试管接入；检测采血针回缩量，检测穿刺位置是否有大的偏差移动
/// 扎止血带计时，提示语，倾斜的试管，血液流入，速度与实物灯一致
/// 接入试管，试管内有血液流入
/// </summary>
public class VTakeStateContinuous : VTakeState
{

    public VTakeStateContinuous(VTake owner) : base((int)EEvet.ContinuousBloodCollection, owner)
    {
        m_name = "连入采血管";
    }

    public override void Enter()
    {
        base.Enter();
        //向客户端发送显示血流入试管命令
    }
    public override void Handle(EEvet evt, params object[] args)
    {
        // throw new NotImplementedException();
        //接收外部硬件模拟的连接采血管命令
        //采血管是否接入  位置偏差       当前的采血针回缩量  
        //args  0           1                   2
        if (evt == EEvet.ContinuousBloodCollection)
        {
            if ((bool)args[0])
            {
                if ((bool)args[1])
                {
                    if ((float)args[2] > 0)
                    {
                        Machine.TranslateToState((int)EEvet.LooseTourniquet);
                    }
                    else
                    {
                        VMessageOut._instance.OutMessage("采血针回缩量小于零");
                    }
                }
                else
                {
                    VMessageOut._instance.OutMessage("位置有偏差");
                }
            }
            else
            {
                VMessageOut._instance.OutMessage("采血管未接入");
            }
        }
    }

    public override void Update()
    {
        // throw new NotImplementedException();
        //向客户端发送实时数据流

    }
}
/// <summary>
/// 松止血带
/// 松止血带，扎止血带计时消失
/// 检测是否松止血带，检测松止血带时机
/// 患者头转向操作者，看着操作部位
/// 扎止血带计时，提示语，采血管，采集量
/// 接入采血管，
/// </summary>
public class VTakeStateLooseTourniquet : VTakeState
{
    float timer;
    public VTakeStateLooseTourniquet(VTake owner) : base((int)EEvet.LooseTourniquet, owner)
    {
        m_name = "松止血带";
    }
    public override void Enter()
    {
        base.Enter();
        VEventManager.Instance.SendCommand(EEvet.Diagnose, "rightPoint");
    }
    public override void Handle(EEvet evt, params object[] args)
    {
        if (evt == EEvet.LooseTourniquet)
        {
            if ((bool)args[0])
            {
                Machine.TranslateToState((int)EEvet.BloodCollection);
                VMessageOut._instance.OutMessage("松止血带耗时" + timer);
            }
            else
            {

                VMessageOut._instance.OutMessage("未松止血带");
            }
        }
    }

    public override void Update()
    {
        timer += Time.deltaTime;
        // throw new NotImplementedException();
    }

    public override void Exit()
    {
        base.Exit();
        //关闭计时器
        m_Owner.BeginTimer(false);
    }
}
/// <summary>
/// 采集血液
/// 达到采集量，拔出试管
/// 检测接入采血管
/// 有采集量，可以未达到目标量；且拔出试管
/// </summary>
public class VTakeStateBloodCollection : VTakeState
{
    //进度
    float progress;
    //计时器
    float timer;
    float offsetTimer = 3;
    public VTakeStateBloodCollection(VTake owner) : base((int)EEvet.BloodCollection, owner)
    {
        m_name = "采集血液";
    }
    public override void Enter()
    {
        base.Enter();
        progress = 0;
        timer = 0;
        m_Owner.SetTubeState();
    }
    public override void Handle(EEvet evt, params object[] args)
    {
        //throw new NotImplementedException();
        //有采集量，可以未达到目标量；且拔出试管
        //args   0                   true
        if (evt == EEvet.BloodCollection)
        {
            if (progress > 0.1f && (bool)args[0])
            {
                Machine.TranslateToState((int)EEvet.ShakeUp);
            }
        }

    }

    public override void Update()
    {
        //throw new NotImplementedException();
        timer += Time.deltaTime;
        m_Owner.SetTubeProgress(timer / offsetTimer);
        progress = timer / offsetTimer;
        //发送参数progress到硬件

        //发送参数progress到客户端
        VServer._instance.ServerSend((EEvet)ID, m_Owner.curTubeIndex, progress);

    }
}
/// <summary>
/// "摇匀
/// 开始采集下一管（看病例设置），即重复步骤19~22；或者进入拔针按压步骤
/// 提示语，采血管，采集量
/// 根据病例设置，最后一管血液采集，摇匀步骤触发后4s
/// </summary>
public class VTakeStateShakeUp : VTakeState
{
    float timer;
    float timerOffset = 4;
    public VTakeStateShakeUp(VTake owner) : base((int)EEvet.ShakeUp, owner)
    {
        m_name = "摇匀";
    }

    public override void Handle(EEvet evt, params object[] args)
    {
        // throw new NotImplementedException();
    }

    public override void Update()
    {
        timer += Time.deltaTime;
        if (timer > timerOffset)
        {
            if (m_Owner.isLastTube)
            {
                Debug.Log("进入拔针");
                //摇匀步骤触发后4s，已经完成采血
                Machine.TranslateToState((int)EEvet.Pulling);

            }
            else
            {
                //摇匀步骤4s，
                Debug.Log("回到采血");
                //没有完成全部采血回到采血状态
                Machine.TranslateToState((int)EEvet.BloodCollection);
            }
            timer = 0;
        }
    }
}
/// <summary>
/// 拔针
/// 拔出最后一个采血管，摇匀放至试管架后。拿取1个棉签按压穿刺点，然后拔出采血针。嘱咐患者按压
/// 检测1. 按压位置是否在穿刺点，2. 拔针时是否按压
/// 提示语
/// 已拔出针
/// </summary>
public class VTakeStatePulling : VTakeState
{
    public VTakeStatePulling(VTake owner) : base((int)EEvet.Pulling, owner)
    {
        m_name = "拔针";
    }

    public override void Handle(EEvet evt, params object[] args)
    {
        // throw new NotImplementedException();

        if (evt == EEvet.Pulling)
        {
            if (args.Length > 0)
            {
                if ((bool)args[0])
                {
                    Machine.TranslateToState((int)EEvet.Control);
                }
            }
        }
    }

    public override void Update()
    {
        // throw new NotImplementedException();
    }
    public override void Exit()
    {
        base.Exit();
        //拔针IK回到注视前方
        VEventManager.Instance.SendCommand(EEvet.Envisage);
    }

}
/// <summary>
/// 按压
/// 语音交互，嘱咐患者按压穿刺点至少5分钟
/// 针已拔出；语音识别
/// 患者点头
/// 嘱咐患者按压动画播放完成
/// </summary>
public class VTakeStateControl : VTakeState
{

    public VTakeStateControl(VTake owner) : base((int)EEvet.Control, owner)
    {
        m_name = "按压";
    }
    public override void Enter()
    {
        base.Enter();
        m_Owner.StarSpeak(true);
    }
    public override void Handle(EEvet evt, params object[] args)
    {
        // throw new NotImplementedException();
        //语音识别完成进入点头
        if (evt == EEvet.Control)
        {
            if (args.Length > 0)
            {
                if ((bool)args[0])
                {
                    VEventManager.Instance.SendCommand(EEvet.Nod);
                }
            }
        }
        if (evt == EEvet.End)
        {
            Machine.TranslateToState((int)EEvet.End);
        }
    }
    public override void Exit()
    {
        base.Exit();
        m_Owner.StarSpeak(false);
    }

}
/// <summary>
/// 结束训练
/// 显示成绩单，数据记录
/// 查看成绩，退出训练
/// </summary>
public class VTakeStateEnd : VTakeState
{
    public VTakeStateEnd(VTake owner) : base((int)EEvet.End, owner)
    {
        m_name = "结束训练";
    }
    public override void Enter()
    {
        base.Enter();
        Camera.main.transform.position = new Vector3(-1, -.5F, -2);
    }


}




