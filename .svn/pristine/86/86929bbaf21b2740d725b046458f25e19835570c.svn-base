using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTracePoint : MonoBehaviour
{

   // int j = new int();
    int[] data = new int[6];
    //float[] tempdata = new float[6];
    //float[] smoothingdata = new float[6];

    int facecount;

    public GameObject Eyes;
    RaycastHit hit;
    Vector2 currentPos;
    Vector2 newPos;
    Vector3 pos1;
    //public Vector4 v;

    public float offsetX=250;
    public float offsetY=50;

    public float distanceEye=.3f;

    public float offset=20;
    public int maxlimit=300;
    // Use this for initialization
    void Start()
    {
       // j = FaceDetection.instance.FacedetectionAdd(5, 7);

        //Eyes = GameObject.Find("Point");
    }

    void Update()
    {
        facecount = FaceDetection.instance.FacedetectionBiggestFace(data);
        if (facecount == 0)
            return;
        data[0] = Mathf.Clamp(data[0],0, maxlimit);
        data[1] = Mathf.Clamp(data[1], 0, maxlimit);
        newPos = new Vector2(data[0], data[1]);
        
        if (Vector2.Distance(currentPos, newPos) > offset)
        {
            currentPos = newPos;
            pos1 = new Vector3(maxlimit - data[0] + data[2] / 2 + offsetX, Screen.height - data[1] - data[3] / 2 - offsetY);
            Ray ray = Camera.main.ScreenPointToRay(pos1);
            Eyes.transform.position=ray.GetPoint(distanceEye);
        }



    }

}
