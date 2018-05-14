using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VEventManager :MonoBehaviour
{
    public static VEventManager Instance;
    public delegate void VGlobeEventHandle(EEvet evt, params object[] args);

    public static event VGlobeEventHandle handle;



    void Awake () {
        Instance = this;
	}
    /// <summary>
    /// 发送事件
    /// </summary>
    /// <param name="evt">事件类型</param>
    /// <param name="args">事件参数</param>
    public void SendCommand(EEvet evt, params object[] args)
    {
        if (handle!=null)
        {
            handle(evt,args);
        }
    }
}
