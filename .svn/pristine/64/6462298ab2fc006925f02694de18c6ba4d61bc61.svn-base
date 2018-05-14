using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.IO;

public class FaceDetection : MonoBehaviour
{

    public static FaceDetection instance;

    [DllImport("libfacedetection_Test")]
    private static extern int add(int x, int y);

    [DllImport("libfacedetection_Test")]
    private static extern bool OpenCam(int CamSerial);

    [DllImport("libfacedetection_Test")]
    private static extern void CloseCam();

    [DllImport("libfacedetection_Test")]
    private static extern int BiggestFace(int[] data);

    // Use this for initialization

    public int FacedetectionAdd(int x, int y)
    {
        return add(x, y);

        //double[] arrvalue = new double[4];
    }

    public bool FacedetectionOpenCam(int CamSerial)
    {
        return OpenCam(CamSerial);
    }

    public void FacedetectionCloseCam()
    {
        CloseCam();
    }

    public int FacedetectionBiggestFace(int[] data)
    {
        return BiggestFace(data);
    }

    int[] data = new int[6];
    int facecount;
    Transform Eyes;
    RaycastHit hit;
    Vector2 currentPos;
    Vector2 newPos;
    Vector3 pos1;
    /// <summary>
    /// 水平偏移
    /// </summary>
    public float offsetX = 250;
    /// <summary>
    /// 垂直偏移
    /// </summary>
    public float offsetY = 50;

    public float distanceEye = .3f;
    /// <summary>
    /// 抗抖动
    /// </summary>
    public float offset = 20;

    public int maxlimit = 300;
    void Awake()
    {
        instance = this;
        Eyes = GameObject.Find("Eye").transform;
        Eyes.position = transform.position + Vector3.forward * distanceEye;
    }
    void Start()
    {

    }

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.DrawCube(Eyes.position, Vector3.one);
    //}
    public void OpenCam()
    {
        FacedetectionOpenCam(0);
    }
    // Update is called once per frame
    void Update()
    {
        facecount = FacedetectionBiggestFace(data);
        if (facecount == 0)
            return;
        data[0] = Mathf.Clamp(data[0], 0, maxlimit);
        data[1] = Mathf.Clamp(data[1], 0, maxlimit);
        newPos = new Vector2(data[0], data[1]);

        if (Vector2.Distance(currentPos, newPos) > offset)
        {
            currentPos = newPos;
            pos1 = new Vector3(maxlimit - data[0] + data[2] / 2 + offsetX, Screen.height - data[1] - data[3] / 2 - offsetY);
            Ray ray = Camera.main.ScreenPointToRay(pos1);
            Eyes.position = ray.GetPoint(distanceEye);
        }
    }

    void OnApplicationQuit()
    {
        FacedetectionCloseCam();
        Debug.Log("Close Cam");
    }
}


