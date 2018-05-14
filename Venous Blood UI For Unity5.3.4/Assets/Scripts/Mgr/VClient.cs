using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections.Generic;

public class VClient : MonoBehaviour {

    public static VClient instance;

    /// <summary>
    /// 服务器ip地址
    /// </summary>
   public  string ip;
    /// <summary>
    /// 服务器端口
    /// </summary>
    public string port;


    /// <summary>
    /// 网络客户端
    /// </summary>
    private NetworkClient myClient;
    /// <summary>
    /// 用户信息分类
    /// </summary>
    private const short userMsg = 64;

    public Dictionary<string, string> myDictionary = new Dictionary<string, string>();
    public string path = "Socket.txt";
    void Awake()
    {
        Read('=');
        ip = GetValue("ip");
        port = GetValue("port");
        if (ip == "" || port == "")
        {
            Debug.LogWarning("canshuweikong");
        }
        instance = this;
        myClient = new NetworkClient();
        SetupClient();

    }
    //读取Assets子目录下StreamingAssets目录下Socket.txt文本文件
    public void Read(char str)
    {
        //path = Path.Combine(Application.streamingAssetsPath, path);
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
    public string GetValue(string key)
    {
        string value = "";
        if (myDictionary.ContainsKey(key))
        {
            value = myDictionary[key];
        }

        return value;
    }
    /// <summary>
    /// 建立客户端
    /// </summary>
    public void SetupClient()
    {
        if (!myClient.isConnected)
        {
            ShowMsg("setup client");
            ClientRegisterHandler();
            myClient.Connect(ip, int.Parse(port));
        }
    }

    /// <summary>
    /// 停止客户端
    /// </summary>
    public void ShutdownClient()
    {
        if (myClient.isConnected)
        {
            ClientUnregisterHandler();
            myClient.Disconnect();

            //NetworkClient.Shutdown()使用后，无法再次连接。
            //This should be done when a client is no longer going to be used.
            //myClient.Shutdown ();
        }
    }
    /// <summary>
    /// 客户端连接到服务器事件
    /// </summary>
    /// <param name="netMsg">Net message.</param>
    private void OnClientConnected(NetworkMessage netMsg)
    {
        ShowMsg("Client connected to server");
    }

    /// <summary>
    ///客户端从服务器断开事件
    /// </summary>
    /// <param name="netMsg">Net message.</param>
    private void OnClientDisconnected(NetworkMessage netMsg)
    {
        ShowMsg("Client disconnected from server");
    }

    /// <summary>
    /// 客户端错误事件
    /// </summary>
    /// <param name="netMsg">Net message.</param>
    private void OnClientError(NetworkMessage netMsg)
    {
        ClientUnregisterHandler();
        ShowMsg("Client error");
    }
    /// <summary>
    /// 显示信息
    /// </summary>
    /// <param name="Msg">Message.</param>
    private void ShowMsg(string Msg)
    {
       // info.text = Msg + "\n\r" + info.text;
        Debug.Log (Msg);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ClientSend(EEvet.Load,true);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            ClientSend(EEvet.Model, 1);
        }
    }
    /// <summary>
    /// 客户端向服务器端发送信息
    /// </summary>
    public void ClientSend(EEvet evt, params object[] args)
    {
        if (myClient.isConnected)
        {
            MessageToByte clientT = new MessageToByte();
            MainClass clientM = new MainClass();
            clientM.message = evt;
            clientM.aa = args;

           clientT.buff = clientT.Object2Bytes(clientM);
            if (myClient.Send(userMsg, clientT))
            {
                ShowMsg("Server send:" + clientT.buff);
            }
        }
    }

    /// <summary>
    /// 客户端接收到服务器端信息事件
    /// </summary>
    /// <param name="netMsg">Net message.</param>
    private void ClientGet(NetworkMessage netMsg)
    {
        MessageToByte ssd = netMsg.ReadMessage<MessageToByte>();
        MainClass s = (MainClass)ssd.Bytes2Object(ssd.buff);
        //接收消息后的处理
        VEventManager.Instance.SendCommand(s.message,s.aa);
        Debug.Log(s.message);
    }

    /// <summary>
    /// 客户端注册事件
    /// </summary>
    private void ClientRegisterHandler()
    {
        myClient.RegisterHandler(MsgType.Connect, OnClientConnected);
        myClient.RegisterHandler(MsgType.Disconnect, OnClientDisconnected);
        myClient.RegisterHandler(MsgType.Error, OnClientError);
        myClient.RegisterHandler(userMsg, ClientGet);
    }

    /// <summary>
    /// 客户端注销事件
    /// </summary>
    private void ClientUnregisterHandler()
    {
        myClient.UnregisterHandler(MsgType.Connect);
        myClient.UnregisterHandler(MsgType.Disconnect);
        myClient.UnregisterHandler(MsgType.Error);
        myClient.UnregisterHandler(userMsg);
    }
}
[Serializable]
public class MainClass
{
    public EEvet message;
    public object[] aa;
}
