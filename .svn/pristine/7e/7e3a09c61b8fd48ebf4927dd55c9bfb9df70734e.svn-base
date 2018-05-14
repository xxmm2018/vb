using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class MyNetworkManager : MonoBehaviour
{
    /// <summary>
    /// 服务器ip地址
    /// </summary>
    public InputField ip;
    /// <summary>
    /// 服务器端口
    /// </summary>
    public InputField port;
    /// <summary>
    /// 要发送的信息
    /// </summary>
    public InputField send;
    /// <summary>
    /// 显示信息
    /// </summary>
    public Text info;
    /// <summary>
    /// 网络客户端
    /// </summary>
    private NetworkClient myClient;
    /// <summary>
    /// 用户信息分类
    /// </summary>
    private const short userMsg = 64;


    public Text txt;
    void Start()
    {
        ip.text = "127.0.0.1";
        port.text = "800";
        info.text = "Start...";
        myClient = new NetworkClient();
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
            NetworkServer.Listen(int.Parse(port.text));

            if (NetworkServer.active)
            {
                ShowMsg("Server setup ok.");
            }
        }
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
            myClient.Connect(ip.text, int.Parse(port.text));
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
    /// 服务器端错误事件
    /// </summary>
    /// <param name="netMsg">Net message.</param>
    private void OnServerError(NetworkMessage netMsg)
    {
        ServerUnregisterHandler();
        ShowMsg("Server error");
    }

    /// <summary>
    /// 显示信息
    /// </summary>
    /// <param name="Msg">Message.</param>
    private void ShowMsg(string Msg)
    {
        info.text = Msg + "\n\r" + info.text;
        //Debug.Log (Msg);
    }

    /// <summary>
    /// 客户端向服务器端发送信息
    /// </summary>
    public void ClientSend()
    {
        if (myClient.isConnected)
        {
            UserMsg um = new UserMsg();
            um.message = send.text;
            if (myClient.Send(userMsg, um))
            {
                ShowMsg("Client send:" + um.message);
            }
        }
    }
    public object[] args;
    /// <summary>
    /// 客户端接收到服务器端信息事件
    /// </summary>
    /// <param name="netMsg">Net message.</param>
    private void ClientGet(NetworkMessage netMsg)
    {
        //netMsg.reader.ReadInt32()
        //UserMsg Msg = netMsg.ReadMessage<UserMsg>();
        //NetworkReader d = netMsg.reader.;

        MessageToByte ssd = netMsg.ReadMessage<MessageToByte>();
        MainClass s = (MainClass)ssd.Bytes2Object(ssd.buff);

        string ss="";
        foreach (var item in s.aa)
        {
            ss += item;
        }
        ShowMsg("Client get:" +s.message+ ss);
        if (s.message==EEvet.Load)
        {
     txt.text = s.aa[1].ToString();
        }
   
        //object d = ssd.Bytes2Object(ssd.buff);

        //foreach (var item in d)
        //{
        //    print(System.Convert.ToInt32(item));
        //}
    }

    void ssss(params object[] args)
    {
        print((int)args[0]);
    }

    /// <summary>
    /// 服务器端向所有客户端发送信息
    /// </summary>
    public void ServerSend()
    {
        if (NetworkServer.active)
        {
            //UserMsg um = new UserMsg();
            //um.message = send.text;

            MessageToByte t = new MessageToByte();

            MainClass m = new MainClass();
            m.aa = new object[] {1,4,"shjhi",3.5f };
 
            t.buff = t.Object2Bytes(m);           
            if (NetworkServer.SendToAll(userMsg, t))
            {
                ShowMsg("Server send:" + t.buff);
            }
        }
    }


    public void Send(EEvet evt,params object[] args)
    {

        if (NetworkServer.active)
        {
            //UserMsg um = new UserMsg();
            //um.message = send.text;

            MessageToByte t = new MessageToByte();

            MainClass m = new MainClass();
            m.message = evt;
            m.aa = args; 

            t.buff = t.Object2Bytes(m);
            if (NetworkServer.SendToAll(userMsg, t))
            {
                ShowMsg("Server send:" + t.buff);
            }
        }
    }

    /// <summary>
    /// 服务器端收到信息事件
    /// </summary>
    /// <param name="netMsg">Net message.</param>
    private void ServerGet(NetworkMessage netMsg)
    {
        UserMsg Msg = netMsg.ReadMessage<UserMsg>();
        //test ssd= netMsg.ReadMessage<test>();
        ShowMsg("Server get:" + Msg.message);
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
public class UserMsg : MessageBase
{
    public string message;
}
//（eevvt,3，true，“shjh”）
//[Serializable]
//public class MainClass
//{
//    public EEvet message;
//    public object[] aa;
//}

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
    public  object Bytes2Object(byte[] buff)
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
//public class RegisterHostMessage : MessageBase
//{
//    public string gameName;
//    public string comment;
//    public bool passwordProtected;

//}