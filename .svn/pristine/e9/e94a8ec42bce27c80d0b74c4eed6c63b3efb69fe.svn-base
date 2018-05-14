using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Baidu.Aip.Face;
using Newtonsoft.Json.Linq;
using LitJson;
using System;

public class BaiduFace : MonoBehaviour
{
    const string APP_ID = "11208222";
    const string API_KEY = "CtlG2Psxn7s3haU9e6R3Y3xY";
    const string SECRET_KEY = "jcK6XDsr86Pegar52KvFcz6a4nFLzZtY";
    const string imageType = "BASE64";
    Face client;
    WebCamDevice[] devices;
    string cameraName;
    WebCamTexture cameraTexture;
    bool isPlay;
    int mPreviewWidth = 500;
    int mPreviewHeight = 500;
    JObject result;
    string user;
    public static BaiduFace instance;
    /// <summary>
    /// 当前用户的信息
    /// </summary>
    public string User
    {
        get
        {
            return user;
        }
    }

    // Use this for initialization
    private void Awake()
    {
        instance = this;
        System.Net.ServicePointManager.ServerCertificateValidationCallback +=
               delegate (object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                           System.Security.Cryptography.X509Certificates.X509Chain chain,
                           System.Net.Security.SslPolicyErrors sslPolicyErrors)
               {
                   return true; // **** Always accept
               };

    }
    public void OpenFace()
    {
        StartCoroutine("GetCamera");
    }
    public void StopFace()
    {
        cameraTexture.Stop();
        StopCoroutine("GetCamera");
        Debug.Log("2222");
   
    }
    void Start()
    {

        client = new Face(API_KEY, SECRET_KEY);
        client.Timeout = 60000;  // 修改超时时间

    }
    
    /// <summary>
    /// 摄像机图片
    /// </summary>
    /// <returns></returns>
    IEnumerator GetCamera()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            devices = WebCamTexture.devices;
            cameraName = devices[0].name;
            cameraTexture = new WebCamTexture(cameraName, mPreviewWidth, mPreviewHeight, 15);
            cameraTexture.Play();
            isPlay = true;
        }
    }
    public Texture Face()
    {
        return cameraTexture;
    }

    public Info GetUser()
    {
        Info info;
        CheckUser(out info);
        return info;
    }
    /// <summary>
    /// 检查用户
    /// </summary>
    public void CheckUser(out Info info)
    {
        var options = new Dictionary<string, object>{
        {"group_id_list", "1"},
        {"max_user_num", "1"}
    };
        result = client.Search(GetImage(), imageType, options);
        // Debug.Log(result);
        JsonData date = JsonMapper.ToObject(result.ToString());
        //Debug.Log(date["result"]["user_list"][0]["user_info"]);
        if (date["error_msg"].ToString() == "SUCCESS")
        {
            if (float.Parse(date["result"]["user_list"][0]["score"].ToString()) > 90)
            {
                info = new Info(true, "登陆成功");
                user = date["result"]["user_list"][0]["user_info"].ToString();
            }
            else
            {
                info = new Info(false, "您不是本机构人员");
            }
        }
        else
        {
            info = new Info(false, "未检测到人脸");
        }




    }
    /// <summary>
    /// 添加用户
    /// </summary>
    /// <param name="m_name">用户名</param>
    public void AddUser(string m_name)
    {
        try
        {
            var options = new Dictionary<string, object>{
        {"user_info", m_name}
    };
            //获取用户ID

            result = client.UserAdd(GetImage(), imageType, "1", "m_" + GetUserID(), options);
            Debug.Log(result);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

    }
    /// <summary>
    /// 人脸识别
    /// </summary>
    public void Detect()
    {
        try
        {
            var options = new Dictionary<string, object>{
        {"face_field", "age"},
        {"max_face_num", 2},
        {"face_type", "LIVE"}
    };
            // 带参数调用人脸检测
            result = client.Detect(GetImage(), imageType, options);
            Debug.Log(result);
        }
        catch (Exception e)
        {

            print(e);
        }

    }
    /// <summary>
    /// 获取下一个ID
    /// </summary>
    /// <returns></returns>
    int GetUserID()
    {
        var options = new Dictionary<string, object>{
        {"length", "1000"}
    };
        // 带参数调用人脸检测
        result = client.GroupGetusers("1", options);
        JsonData date = JsonMapper.ToObject(result.ToString());
        return date["result"]["user_id_list"].Count + 1;

    }
    /// <summary>
    /// 转换摄像机图片格式
    /// 将图片数据转换为Base64字符串
    /// </summary>
    private string GetImage()
    {
        Texture2D temp = new Texture2D(cameraTexture.width, cameraTexture.height);
        int y = 0;
        while (y < temp.height)
        {
            int x = 0;
            while (x < temp.width)
            {
                Color color = cameraTexture.GetPixel(x, y);
                temp.SetPixel(x, y, color);
                ++x;
            }
            ++y;
        }
        temp.Apply();
        string base64 = Convert.ToBase64String(temp.EncodeToJPG());
        return base64;
    }

    private void OnDestroy()
    {
        StopFace();
    }

}
/// <summary>
/// 反馈的结果
/// </summary>
public struct Info
{
    public string result;
    public bool state;
    public Info(bool state, string result)
    {
        this.state = state;
        this.result = result;
    }
}

