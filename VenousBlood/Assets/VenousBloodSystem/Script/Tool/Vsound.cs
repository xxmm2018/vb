using UnityEngine;
using System.Collections;

public class Vsound : MonoBehaviour {
    public SkinnedMeshRenderer skin;
    AudioClip _clipRecord = new AudioClip();
    
    // Use this for initialization
    void Start () {
        _clipRecord = Microphone.Start(null, false, 30, 16000);
    }
	
	// Update is called once per frame
	void Update () {
        skin.SetBlendShapeWeight(8,GetMaxVolume()*100);
	
	}
    //每一振处理那一帧接收的音频文件  
    float GetMaxVolume()
    {
        float maxVolume = 0f;
        //剪切音频  
        float[] volumeData = new float[128];
        int offset = Microphone.GetPosition(null) - 128 + 1;
        if (offset < 0)
        {
            return 0;
        }
        _clipRecord.GetData(volumeData, offset);

        for (int i = 0; i < 128; i++)
        {
            float tempMax = volumeData[i];//修改音量的敏感值  
            if (maxVolume < tempMax)
            {
                maxVolume = tempMax;
            }
        }
        return maxVolume;
    }
}
