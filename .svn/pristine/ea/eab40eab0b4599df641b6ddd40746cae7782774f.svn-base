using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections.Generic;

public class VServer : MonoBehaviour {

    public static VServer _instance;

    /// <summary>
    /// 服务器ip地址
    /// </summary>
     string ip;
    /// <summary>
    /// 服务器端口
    /// </summary>
     string  port;

    /// <summary>
    /// 用户信息分类
    /// </summary>
    private const short userMsg = 64;
    public Dictionary<string, string> myDictionary = new Dictionary<string, string>();
    public string path = "Socket.txt";
    void Start()
    {
        _instance = this;
        Read('=');
        ip = GetValue("ip");
        port = GetValue("port");
        if (ip == "" || port == "")
        {
            Debug.LogWarning("Error");
        }
        SetupServer();
    }
    //读取Assets目录Socket.txt文本文件
    void Read(char str)
    {
        string[] items = File.ReadAllLines(path);
        string key;
        string value;
        foreach (var item in items)
        {
            if (!string.IsNullOrEmpty(item))
            {
                string[] main = item.Split(str);
                key = (main[0].Substring(0, main[0].Length)).Trim();
                value = main[1].Trim();
                myDictionary.Add(key, value);
            }
        }
    }
    string GetValue(string key)
    {
        string value = "";
        if (myDictionary.ContainsKey(key))
        {
            value = myDictionary[key];
        }

        return value;
    }
    /// <summary>
    /// 建立服务器
    /// </summary>
    public void SetupServer()
    {
        if (!NetworkServer.active)
        {
            ShowMsg("setup server");
            ServerRegisterHandler();
            NetworkServer.Listen(int.Parse(port));

            if (NetworkServer.active)
            {
                ShowMsg("Server setup ok.");
            }
        }
    }
    /// <summary>
    /// 停止服务器端
    /// </summary>
    public void ShutdownServer()
    {
        if (NetworkServer.active)
        {
            ServerUnregisterHandler();
            NetworkServer.DisconnectAll();
            NetworkServer.Shutdown();

            if (!NetworkServer.active)
            {
                ShowMsg("shut down server");
            }
        }
    }
    /// <summary>
    /// 服务器端有客户端连入事件
    /// </summary>
    /// <param name="netMsg">Net message.</param>
    private void OnServerConnected(NetworkMessage netMsg)
    {
        ShowMsg("One client connected to server");
    }

    /// <summary>
    /// 服务器端有客户端断开事件
    /// </summary>
    /// <param name="netMsg">Net message.</param>
    private void OnServerDisconnected(NetworkMessage netMsg)
    {
        ShowMsg("One client connected from server");
    }
    /// <summary>
    /// 显示信息
    /// </summary>
    /// <param name="Msg">Message.</param>
    private void ShowMsg(string Msg)
    {
        //Debug.Log (Msg);
        VMessageOut._instance.OutMessage(Msg);
    }

    /// <summary>
    /// 服务器端错误事件
    /// </summary>
    /// <param name="netMsg">Net message.</param>
    private void OnServerError(NetworkMessage netMsg)
    {
        ServerUnregisterHandler();
        ShowMsg("Server error");
    }

    public void ServerSend(EEvet evt, params object[] args)
    {
        if (NetworkServer.active)
        {
            MessageToByte t = new MessageToByte();
            MainClass m = new MainClass();
            m.message = evt;
            m.aa = args;
            t.buff = t.Object2Bytes(m);
            if (NetworkServer.SendToAll(userMsg, t))
            {
                ShowMsg("Server send:" + m.message);
            }
        }
    }

    /// <summary>
    /// 服务器端收到信息事件
    /// </summary>
    /// <param name="netMsg">Net message.</param>
    private void ServerGet(NetworkMessage netMsg)
    {
        MessageToByte serverSsd = netMsg.ReadMessage<MessageToByte>();
        MainClass msg = (MainClass)serverSsd.Bytes2Object(serverSsd.buff);
        VEventManager.Instance.SendCommand(msg.message,msg.aa);
    }

    /// <summary>
    /// 服务器端注册事件
    /// </summary>
    private void ServerRegisterHandler()
    {
        NetworkServer.RegisterHandler(MsgType.Connect, OnServerConnected);
        NetworkServer.RegisterHandler(MsgType.Disconnect, OnServerDisconnected);
        NetworkServer.RegisterHandler(MsgType.Error, OnServerError);
        NetworkServer.RegisterHandler(userMsg, ServerGet);
    }
    /// <summary>
    /// 服务器端注销事件
    /// </summary>
    private void ServerUnregisterHandler()
    {
        NetworkServer.UnregisterHandler(MsgType.Connect);
        NetworkServer.UnregisterHandler(MsgType.Disconnect);
        NetworkServer.UnregisterHandler(MsgType.Error);
        NetworkServer.UnregisterHandler(userMsg);
    }
}

[Serializable]
public class MainClass
{
    public EEvet message;
    public object[] aa;
}

public class MessageToByte : MessageBase
{
    public byte[] buff;

    /// <summary>  
    /// 将对象转换为byte数组  
    /// </summary>  
    /// <param name="obj">被转换对象</param>  
    /// <returns>转换后byte数组</returns>  
    public byte[] Object2Bytes(object obj)
    {
        byte[] buff;
        using (MemoryStream ms = new MemoryStream())
        {
            IFormatter iFormatter = new BinaryFormatter();
            iFormatter.Serialize(ms, obj);
            buff = ms.GetBuffer();
        }
        return buff;
    }

    /// <summary>  
    /// 将byte数组转换成对象  
    /// </summary>  
    /// <param name="buff">被转换byte数组</param>  
    /// <returns>转换完成后的对象</returns>  
    public object Bytes2Object(byte[] buff)
    {
        object obj;
        using (MemoryStream ms = new MemoryStream(buff))
        {
            IFormatter iFormatter = new BinaryFormatter();
            obj = iFormatter.Deserialize(ms);
        }
        return obj;
    }
}
