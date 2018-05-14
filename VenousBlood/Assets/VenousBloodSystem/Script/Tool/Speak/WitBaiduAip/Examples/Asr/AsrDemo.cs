using UnityEngine;
using UnityEngine.UI;
using Wit.BaiduAip.Speech;

public class AsrDemo : MonoBehaviour
{
    public string APIKey = "";
    public string SecretKey = "";
    public Button StartButton;
    public Button StopButton;
    public Text DescriptionText;

    private AudioClip _clipRecord = new AudioClip();
    private Asr _asr;


    ////每一振处理那一帧接收的音频文件  
    //float GetMaxVolume()
    //{
    //    float maxVolume = 0f;
    //    //剪切音频  
    //    float[] volumeData = new float[128];
    //    int offset = Microphone.GetPosition(null) - 128 + 1;
    //    if (offset < 0)
    //    {
    //        return 0;
    //    }
    //    _clipRecord.GetData(volumeData, offset);

    //    for (int i = 0; i < 128; i++)
    //    {
    //        float tempMax = volumeData[i];//修改音量的敏感值  
    //        if (maxVolume < tempMax)
    //        {
    //            maxVolume = tempMax;
    //        }
    //    }
    //    return maxVolume;
    //}
    void Start()
    {
        _asr = new Asr(APIKey, SecretKey);
        StartCoroutine(_asr.GetAccessToken());

        StartButton.gameObject.SetActive(true);
        StopButton.gameObject.SetActive(false);
        DescriptionText.text = "";

        StartButton.onClick.AddListener(OnClickStartButton);
        StopButton.onClick.AddListener(OnClickStopButton);
      //  _clipRecord = Microphone.Start(null, false, 30, 16000);
    }
    private void Update()
    {
        //var data = Asr.ConvertAudioClipToPCM16(_clipRecord);
        //print(data[0]);
        //print(Microphone.GetPosition(null));
        //print(Microphone.IsRecording(null));
        //if (Microphone.GetPosition(null) > 30)
        //{
        //    print("开始录音");
        //}
        //else
        //{
        //    Microphone.End(null);
        //    print("结束录音");
        //    var data = Asr.ConvertAudioClipToPCM16(_clipRecord);
        //    StartCoroutine(_asr.Recognize(data, s =>
        //    {
        //        DescriptionText.text = s.result[0];
        //        StartButton.gameObject.SetActive(true);
        //    }));
        //}


    }
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
            StartButton.gameObject.SetActive(true);
        }));
    }
}