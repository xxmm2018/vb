using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;

public class VMessageOut : MonoBehaviour {
    public static VMessageOut _instance;
    public Text txt;
    public int count;
    VTool tool;
	// Use this for initialization
	void Awake () {
        _instance = this;
        tool = new VTool(count);
	}

    public void OutMessage(string msg)
    {
        Debug.Log(msg);
       tool.SetMsg(msg);
       txt.text = tool.Sb.ToString();
    }
}
/// <summary>
/// 滚动信息显示小工具
///   创建   tool = new Tool(new Queue(), new StringBuilder());
///   使用   tool.SetMsg("content");
///   展示   serverText.text = tool.Sb.ToString();
/// </summary>
public class VTool
{
    Queue txtContent;
    StringBuilder sb;
    int count;
    /// <summary>
    /// 定义一个长度为count的工具
    /// </summary>
    /// <param name="count">文本行数</param>
    public VTool(int count)
    {
        this.txtContent = new Queue();
        this.Sb = new StringBuilder();
        this.count = count;
    }

    public StringBuilder Sb
    {
        get
        {
            return sb;
        }

        set
        {
            sb = value;
        }
    }

    public void SetMsg(string str)
    {
        if (txtContent.Count > count)
        {
            Sb.Remove(0, ((string)txtContent.Peek()).Length + 1);
            txtContent.Dequeue();
        }
        txtContent.Enqueue(str);
        Sb.Append(str + "\n");
    }

}