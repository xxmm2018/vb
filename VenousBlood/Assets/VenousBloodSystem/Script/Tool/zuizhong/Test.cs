using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	int j = new int();
	int[] data = new int[6];
	float[] tempdata = new float[6];
	float[] smoothingdata = new float[6];

	int facecount;

	GameObject Eyes;

	// Use this for initialization
	void Start () {
		j = FaceDetection.instance.FacedetectionAdd(5, 7);

		Eyes = GameObject.Find("Eyes");
	}

	void FixedUpdate()
	{
		//FaceDetection.instance.FacedetectionBiggestFace(data);
	}
	
	// Update is called once per frame
	void Update () {
		facecount = FaceDetection.instance.FacedetectionBiggestFace(data);
	}

	void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 200, 100), "this dll i = 5+7, i is" + j);
		//GUI.Button(new Rect(data[0], data[1], data[2], data[3]), "Hello World!");
		//GUI.Button(new Rect(120, 120, 120, 120), "Hello World!");
		//Debug.Log(data[0] + data[2] / 2);
		//Debug.Log(data[1] + data[3] / 2);

		//Eyes.transform.SetPositionAndRotation(new Vector3(-(data[0] + data[2] / 2) / 10f, -(data[1] + data[3] / 2) / 10f, 0), new Quaternion(0, 0, 0, 0));

		//未完成:
		//因为摄像头放置位置有偏差，人脸检测结果并不能直接呈现出人脸和屏幕之间的关系。
		//比如摄像头放置在屏幕下方，斜向上45度拍摄人脸，当人脸远离屏幕时，摄像头画面中的人脸会向下移动。
		//所以必须将人脸位置和人脸大小建立关系。

		//滤波
		if(facecount != 0)
		{
			for(int i = 0; i < 6; ++i)
			{
				tempdata[i] = data[i];
			}
			for(int i = 0; i < 6; ++i)
			{
				if(smoothingdata[i] == 0)
				{
					smoothingdata[i] = tempdata[i];
				}
				else
				{
					smoothingdata[i] = smoothingdata[i] + (tempdata[i] - smoothingdata[i])/50;
				}
			}
			//GUI.Button(new Rect(smoothingdata[0], smoothingdata[1], smoothingdata[2], smoothingdata[3]), "Hello World!");
			GUI.Button(new Rect(smoothingdata[0], smoothingdata[1], smoothingdata[2], smoothingdata[3]), facecount.ToString());
			//Eyes.transform.SetPositionAndRotation(new Vector3(-(smoothingdata[0] + smoothingdata[2] / 2f) / 10f, -(smoothingdata[1] + smoothingdata[3] / 2f) / 10f, 0), new Quaternion(0, 0, 0, 0));
		}
		
    }
}
