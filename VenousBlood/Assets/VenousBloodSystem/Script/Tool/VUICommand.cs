using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class VUICommand : MonoBehaviour
{
    public EEvet evt;
    [SerializeField]
    public List<VBase> values = new List<VBase>();
    // Use this for initialization
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate () { VEventManager.Instance.SendCommand(evt, GetArgs()); });
    }

    private object[] GetArgs()
    {
        object[] args = new object[values.Count];
        for (int i = 0; i < values.Count; i++)
        {
            VBase b = values[i];
            switch (b.state)
            {
                case VToolState.Int:
                    int a;
                    if (int.TryParse(b.value, out a))
                    {
                        args[i] = a;
                    }
                    else
                    {
                        ErrorInfo(i, b,"整数");
                    }

                    break;

                case VToolState.Bool:
                    bool value;
                    if (bool.TryParse(b.value, out value))
                    {
                        args[i] = value;
                    }
                    else
                    {
                        ErrorInfo(i, b, "0或1");
                    }

                    break;
                case VToolState.String:
                    args[i] = b.value;
                    break;
                case VToolState.Float:
                    float f;
                    if (float.TryParse(b.value, out f))
                    {
                        args[i] = f;
                    }
                    else
                    {
                        ErrorInfo(i, b, "浮点类型");
                    }
                    break;
                case VToolState.Vector:
                    Vector3 v;
                   string [] temp=b.value.Split(',');
                    if (float.TryParse(temp[0], out v.x)|| float.TryParse(temp[1], out v.y)|| float.TryParse(temp[2], out v.z))
                    {
                        v.x = float.Parse( temp[0]);
                        v.y = float.Parse(temp[1]);
                        v.z = float.Parse(temp[2]);
                        args[i] = v;
                    }
                    else
                    {
                        ErrorInfo(i, b, "浮点类型");
                    }
                    break;
                default:
                    break;
            }
        }
        return args;
    }

    private static void ErrorInfo(int i, VBase b,string s)
    {
        Debug.LogWarning("第" + i + "个参数,转换类型为" + b.state + "的值" + b.value + "存在问题，请更改为合适的值："+s);
    }
}

[System.Serializable]
public struct VBase
{
    [SerializeField]
    public VToolState state;
    [SerializeField]
    public string value;
    public VBase(VToolState state, string value)
    {
        this.state = state;
        this.value = value;
    }
}
public enum VToolState
{
    Int,
    Bool,
    String,
    Float,
    Vector
}
