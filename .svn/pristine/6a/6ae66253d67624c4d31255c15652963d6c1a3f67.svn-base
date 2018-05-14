using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 登录界面
/// </summary>
public class LoginUI : UIBase
{
    Button loginBtn;
    InputField _name;
    InputField _pass;
    Image tips;
    Text tipsText;
    int id;
    float curValue;
    void Awake()
    {
       
        loginBtn = transform.Find("main/LoginBtn").GetComponent<Button>();
        _name = transform.Find("main/UserID").GetComponent<InputField>();
        _pass = transform.Find("main/UserPass").GetComponent<InputField>();
        tipsText = transform.Find("main/Text").GetComponent<Text>();
        loginBtn.onClick.AddListener(LoginBtnOnClick);
    }
    protected override void Inst()
    {
        base.Inst();
        title.text = "登陆";
    }

    public void LoginBtnOnClick()
    {
        VClient.instance.ClientSend(EEvet.Load,new object[] { _name.text, _pass.text });
    }


    protected override void Handle(EEvet evt, params object[] args)
    {
        base.Handle(evt, args);

        if (args.Length>0)
        {
            if (evt == EVT && !(bool)args[0])
            {
                tipsText.text = "登陆失败，重新登陆";
                _name.text = "";
                _pass.text = "";
            }
        }
    }

}
