using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FaceManager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        BaiduFace.instance.OpenFace();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<RawImage>().texture = BaiduFace.instance.Face();
    }
    public void CheckFace()
    {
        Info info = BaiduFace.instance.GetUser();
        if (info.state)
        {
            VEventManager.Instance.SendCommand(EEvet.Load, info.state);
        }
        VMessageOut._instance.OutMessage(info.result);

    }
    private void OnEnable()
    {
        BaiduFace.instance.OpenFace();
    }
    //private void OnDisable()
    //{
    //    BaiduFace.instance.StopFace();
    //}
}
