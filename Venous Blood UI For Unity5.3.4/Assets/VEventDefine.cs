using System;


public enum EEvet
{
    #region 步骤
    Load,           //登录
    Model,          //模块选择
    Check,          //检验选择
    Object,         //用物状态检测
    Wash,         //洗手
    Mask,            //戴口罩、手套
    Choice,             //用物选择
    Inspection, //收取检查申请单
    CheckPatient,   //核对患者
    Certificate,              //打印检查凭证
    Label,      //粘贴标签
    Tourniquet,  //扎止血带
    Vessels,    //确认血管
    Sterilize,  //消毒
    Dry,        //待干
    Puncture,   //穿刺
    ContinuousBloodCollection, //连入采血管
    LooseTourniquet,        // 松止血带
    BloodCollection,        //采集血液
    ShakeUp,                    //摇匀
    Pulling,            //拔针
    Control,                //按压
    #endregion

    //#region 表情事件
    //BeginVeins,          // 静脉注射事件
    //Veins,
    //EndVeins,
    //Say,
    //Quiet,
    //Happy,
    //#endregion

    //#region 动画
    //Idle,          // 动画事件
    //Move,
    //Sit,
    //#endregion

    #region IK事件
    HandOver,       //递交
    Diagnose,     //诊断
    Nod,         //点头
    Envisage,   //正视
    #endregion

    Timer,
    End //结束训练
}

