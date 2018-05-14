using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

//声音对象池  
public class VAudioObjectPool
{
    //要生成的对象池预设    
    private GameObject prefab;
    //对象池列表    
    private List<GameObject> pool;
    //构造函数    
    public VAudioObjectPool(GameObject prefab, int initialSize)
    {
        this.prefab = prefab;
        this.pool = new List<GameObject>();
        for (int i = 0; i < initialSize; i++)
        {
            AlllocateInstance();
        }
    }
    // 获取实例      
    public GameObject GetInstance()
    {
        if (pool.Count == 0)
        {
            AlllocateInstance();
        }
        GameObject instance = pool[0];
        pool.RemoveAt(0);
        instance.SetActive(true);
        return instance;
    }
    // 释放实例    
    public void ReleaseInstance(GameObject instance)
    {
        instance.SetActive(false);
        pool.Add(instance);
    }
    // 生成本地实例    
    private GameObject AlllocateInstance()
    {
        GameObject instance = (GameObject)GameObject.Instantiate(prefab);
        instance.transform.SetParent(VAudioMgr._instance.transform);
        instance.SetActive(false);
        pool.Add(instance);
        return instance;
    }
}

public class VAudioMgr : MonoBehaviour
{
    //单例模式  
    public static VAudioMgr _instance;

    //audioClip列表  
    public List<AudioClip> audioList;
    //初始声音预设数量  
    public int initAudioPrefabCount = 5;
    //记录静音前的音量大小  
    [HideInInspector]
    public float tempVolume = 0;
    //是否静音  
    private bool isMute = false;
    public bool IsMute
    {
        set
        {
            isMute = value;
            if (isMute)
            {
                tempVolume = AudioListener.volume;
                AudioListener.volume = 0;
            }
            else
            {
                AudioListener.volume = tempVolume;
            }
        }
        private get { return isMute; }
    }

    //声音大小系数  
    private float volumeScale = 1;
    public float VolumeScale
    {
        set
        {
            volumeScale = Mathf.Clamp01(value);
            if (!IsMute)
            {
                AudioListener.volume = value;
            }
        }
        private get
        {
            return volumeScale;
        }
    }
    //audio字典  
    private Dictionary<string, AudioClip> audioDic = new Dictionary<string, AudioClip>();
    //声音对象池  
    private VAudioObjectPool audioObjectPool;

    void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this);
    }

    void Start()
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).hideFlags != HideFlags.HideInHierarchy)
            {
                InitAudioSource(transform.GetChild(i).GetComponent<AudioSource>());
                audioObjectPool.ReleaseInstance(transform.GetChild(i).gameObject);
            }
        }

        GameObject audioPrefab = new GameObject("AudioObjectPool");
        audioPrefab.AddComponent<AudioSource>();
        audioPrefab.GetComponent<AudioSource>().playOnAwake = false;
        audioObjectPool = new VAudioObjectPool(audioPrefab, initAudioPrefabCount);
        audioPrefab.hideFlags = HideFlags.HideInHierarchy;
        audioPrefab.transform.SetParent(this.transform);

        foreach (AudioClip ac in audioList)
        {
            audioDic.Add(ac.name, ac);
        }
    }
    //暂停播放  
    public void PauseAudio(AudioSource audioSource)
    {
        audioSource.Pause();
    }
    //继续播放  
    public void ResumeAudio(AudioSource audioSource)
    {
        audioSource.UnPause();
    }
    //停止播放  
    public void StopAudio(AudioSource audioSource)
    {
        audioSource.Stop();
    }
    //播放声音  
    public void PlayAudio(AudioSource audioSource, string audioName, float volume = 1, bool isLoop = false)
    {
        if (IsMute)
        {
            return;
        }
        AudioClip audioClip;
        if (audioDic.TryGetValue(audioName, out audioClip))
        {
            audioSource.loop = isLoop;
            audioSource.clip = audioClip;
            audioSource.volume = volume;
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.Play();
        }
    }
    public void PlayAudio(Vector3 audioPos, string audioName, float volume = 1, bool isLoop = false)
    {
        if (IsMute)
        {
            return;
        }
        AudioClip audioClip;
        if (audioDic.TryGetValue(audioName, out audioClip))
        {
            GameObject audioGo = audioObjectPool.GetInstance();
            audioGo.transform.position = audioPos;
            audioGo.name = audioName;
            AudioSource audioSource = audioGo.GetComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.volume = volume;
            audioSource.loop = isLoop;
            audioSource.Play();
            StartCoroutine(DestroyAudioGo(audioSource, audioClip.length));
        }
    }
    //初始化AudioSource  
    private void InitAudioSource(AudioSource audioSource)
    {
        if (audioSource == null)
        {
            return;
        }
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.volume = 1;
        audioSource.clip = null;
        audioSource.name = "AudioObjectPool";
    }
    //销毁声音  
    IEnumerator DestroyAudioGo(AudioSource audioSource, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        InitAudioSource(audioSource);
        audioObjectPool.ReleaseInstance(audioSource.gameObject);
    }
    void Destroy()
    {
        StopAllCoroutines();
    }
}
