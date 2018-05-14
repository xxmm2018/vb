using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public abstract class UIBase : MonoBehaviour
{    public EEvet EVT;
    [HideInInspector]
    public  Text title;
    void OnEnable()
    {

        VEventManager.handle += Handle;
      
    }

    void OnDistroy()
    {

        VEventManager.handle -= Handle; 
    }

    protected virtual void Handle(EEvet evt,params object[] args)
    {
        //if (evt == EEvet.Timer||evt==EEvet.Happy)
        //    return;
        if (evt>EEvet.End|| evt == EEvet.Timer)   
            return;
        if (evt==EVT)
        {
            
            Show();
        }
        else
        {
            Hide();
        }

    }
    protected virtual void Show()
    {
        Inst();
        
        transform.GetChild(0).gameObject.SetActive(true);
        
       
    }
    protected virtual void Hide()
    {
        
        transform.GetChild(0).gameObject.SetActive(false);
        
    }
    protected virtual void Inst()
    {
        title = transform.parent.Find("Title").GetComponent<Text>();
    }
    
}
