using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TimerMgr : UIBase
{
    Text timer;
    void Awake()
    {
        timer = transform.Find("main/TimeText").GetComponent<Text>();
    }
    protected override void Handle(EEvet evt, params object[] args)
    {
        if (evt == EEvet.Timer)
        {
            if ((bool)args[0])
            {
                Show();
                timer.text = args[1].ToString();
            }
            else
            {
                Hide();
                gameObject.SetActive(false);
            }
        }

      

    }

    protected override void Inst()
    {

    }

}
