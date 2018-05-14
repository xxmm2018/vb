using UnityEngine;
using System.Collections;

public class BloodSampling1UIMgr : MonoBehaviour {
    public bool isBind { get; set; }//是否扎止血带
    public bool isDisinfect { get; set; }//是否完成消毒
    public bool isDrawBlood { get; set; }//是否在抽血
    public float curBlood { get; set; }//当前试管血量
    public bool EndDrawBlood { get; set; }//停止协程
    public int disinfectCount;
    public bool isFirst { get; set; }//是否是第一次接试管
    void Awake()
    {
        curBlood = 0;
        isBind = false;
        isDisinfect = false;
        isDrawBlood = false;
        isFirst = true;
        disinfectCount = 1;

       
    }
    void Update()
    {
        if (isDrawBlood)
        {
            isDrawBlood = false;
            StartCoroutine("DrawBlood");
        }
        if (EndDrawBlood||curBlood>=5)
        {
            StopCoroutine("DrawBlood");
        }
        
    }

    IEnumerator DrawBlood()
    {
        for (float i = 0; i <= 5; i+=0.2f)
        {
            yield return new WaitForSeconds(1);
            curBlood +=(float) 0.2;
        }    
    }
}
