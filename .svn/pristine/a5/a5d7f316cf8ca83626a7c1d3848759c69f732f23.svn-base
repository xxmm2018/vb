using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Wit.BaiduAip.Speech;

public class VSpeak : MonoBehaviour
{
    //KEY
    string APIKey = "4HHAYdaKdhRyd0EOnT2jImPq";
    string SecretKey = "246e6ba3dfc4aeb98ab031613ceece72";
    //语音识别
    public Button StartButton;
    public Button StopButton;

    private AudioClip _clipRecord = new AudioClip();
    private Asr _asr;
    string result;
    //语音合成
    private Tts _tts;
    private AudioSource _audioSource;
    public  bool _startPlaying;
    //进度
    public Text DescriptionText;
    EEvet evt;
    public static VSpeak instance;
    // Use this for initialization
    void Start()
    {
        instance = this;
        //识别
        _asr = new Asr(APIKey, SecretKey);
        StartCoroutine(_asr.GetAccessToken());

        StartButton.gameObject.SetActive(true);
        StopButton.gameObject.SetActive(false);
        DescriptionText.text = "";

        StartButton.onClick.AddListener(OnClickStartButton);
        StopButton.onClick.AddListener(OnClickStopButton);

        //合成
        _tts = new Tts(APIKey, SecretKey);
        StartCoroutine(_tts.GetAccessToken());
        _audioSource = gameObject.AddComponent<AudioSource>();

        DescriptionText.text = "";
    }
    //合成
    void Synthesis(string tts_conent)
    {
        DescriptionText.text = "合成中...";

        StartCoroutine(_tts.Synthesis(tts_conent, s =>
        {
            if (s.Success)
            {
                DescriptionText.text = "合成成功，正在播放";
                _audioSource.clip = s.clip;
                _audioSource.Play();
                _startPlaying = true;
            }
            else
            {
                DescriptionText.text = s.err_msg;
            }
        }));
    }

    void Update()
    {
        if (_startPlaying)
        {
            if (!_audioSource.isPlaying)
            {
                _startPlaying = false;
                DescriptionText.text = "播放完毕，可以修改文本继续测试";
                switch (evt)
                {
                    case EEvet.CheckPatient:
                        VEventManager.Instance.SendCommand(EEvet.CheckPatient,true);
                        break; 
                    case EEvet.Control:
                        VEventManager.Instance.SendCommand(EEvet.Control, true);
                        break;
                    default:
                        break;
                }
             
            }
        }
    }
    //识别
    private void OnClickStartButton()
    {
        StartButton.gameObject.SetActive(false);
        StopButton.gameObject.SetActive(true);
        DescriptionText.text = "Listening...";
        _clipRecord = Microphone.Start(null, false, 30, 16000);

    }

    private void OnClickStopButton()
    {
        StartButton.gameObject.SetActive(false);
        StopButton.gameObject.SetActive(false);
        DescriptionText.text = "Recognizing...";
        Microphone.End(null);
        Debug.Log("end record");
        var data = Asr.ConvertAudioClipToPCM16(_clipRecord);
        StartCoroutine(_asr.Recognize(data, s =>
        {
            DescriptionText.text = s.result[0];
            //开始应答
            Speak(s.result[0].Split('，')[0]);
            StartButton.gameObject.SetActive(true);
        }));
    }

    void Speak(string ask)
    {
        
        switch (ask)
        {
            case "你好":
                Synthesis("你好");
                break;
            case "你叫什么名字":
                Synthesis("张三");
                break;
            case "你叫张三":
                Synthesis("是的");
                evt = EEvet.CheckPatient;
                break;
            case "按压五分钟":
                Synthesis("好的谢谢");
                evt = EEvet.Control;
                break;
            default:
                break;
        }
    }
}
