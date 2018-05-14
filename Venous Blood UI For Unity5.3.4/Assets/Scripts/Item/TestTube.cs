using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TestTube : UIBase
{
    Slider slider;
    Text aim;
    Text cur;
    void Awake()
    {
        slider = transform.Find("Slider").GetComponent<Slider>();
        aim = transform.Find("Aim").GetComponent<Text>();
        cur = transform.Find("Cur").GetComponent<Text>();
      
    }

    protected override void Handle(EEvet evt, params object[] args)
    {
        base.Handle(evt, args);
        //if (evt==EEvet.ContinuousBloodCollection)
        //{
        //    gameObject.SetActive(true);
        //}
        //if(evt==EEvet.ShakeUp)
        //{
        //    Destroy(gameObject);
        //}
        if (evt==EEvet.BloodCollection)
        {
            if (args.Length>=2)
            {
                //slider.value = (float)args[1];
            }
          
        }
    }
    protected override void Hide()
    {
        
    }

    protected override void Show()
    {
        
    }
}
