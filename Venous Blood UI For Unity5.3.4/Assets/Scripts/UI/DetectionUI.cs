using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DetectionUI : UIBase
{
    Text tips;

    void Awake()
    {
        tips = transform.Find("main/Text").GetComponent<Text>();
        tips.text = "请戴帽子、口罩和手套";
    }

    protected override void Inst()
    {
        base.Inst();
        title.text = "穿戴检测";
    }

}
