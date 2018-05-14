using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item :MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    Image item;
    Text text;//自身文字信息
    Text childText;//Item上文字信息
    private Transform oriparent;//记录当前父物体的位置
    CanvasGroup canvasGroup;
    private Vector3 distance;//和鼠标的距离

    void Start()
    {
        text = transform.Find("Text").GetComponent<Text>();
        text = transform.Find("Text").GetComponent<Text>();
        GlobalMgr.Instance.Items.Add(text.text);
        item = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        oriparent = transform.parent;
        item.transform.SetParent (GlobalMgr.Instance.UIMgr._mainCanvas_p.transform);
        distance = transform.position - Input.mousePosition;
        canvasGroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition + distance;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Transform obj;
        canvasGroup.blocksRaycasts = true;
        if (eventData.pointerEnter==null)
        {
            transform.SetParent(oriparent);
            transform.localPosition = Vector3.zero;
            return;
        }
        else
        {
            obj = eventData.pointerEnter.transform;
           
            print(obj.gameObject.name);
        }
        switch (obj.tag)
        {
            case "grid":
                //判断格子上是否有物体
                if (obj.childCount==1)
                {
                    Transform childT = obj.GetChild(0);
                    childT.SetParent(oriparent);
                    transform.SetParent(obj);
                    childT.localPosition = Vector3.zero;
                }
                else
                {
                    print("add");
                    transform.SetParent(obj);
                    GlobalMgr.Instance.Items.Remove(text.text);
                    if (!GlobalMgr.Instance.Instrumenttray.Contains(text.text))
                    {
                        GlobalMgr.Instance.Instrumenttray.Add(text.text);
                    }

                    Eventsys.instance.Func();
                    
                }
                break;
            case "Item":
                {
                    childText = obj.FindChild("Text").GetComponent<Text>();
                    if (obj.parent.tag=="cell")
                    {
                        Transform trans = obj.parent;
                        print(trans.name);
                        transform.SetParent(trans);
                        obj.SetParent(oriparent);
                        obj.localPosition = Vector3.zero;
                        if (GlobalMgr.Instance.Instrumenttray.Contains(text.text))
                        {
                            GlobalMgr.Instance.Instrumenttray.Remove(text.text);
                        }
                        if (!GlobalMgr.Instance.Items.Contains(text.text))
                        {
                            GlobalMgr.Instance.Items.Add(text.text);
                        }
                        if (!GlobalMgr.Instance.Instrumenttray.Contains(childText.text))
                        {
                            GlobalMgr.Instance.Instrumenttray.Add(childText.text);
                        }
                        if (GlobalMgr.Instance.Items.Contains(childText.text))
                        {
                            GlobalMgr.Instance.Items.Remove(childText.text);
                        }
                        Eventsys.instance.Func();
                    }
                    else if(obj.parent.tag == "grid")
                    {
                        Transform trans = obj.parent;
                        obj.SetParent(oriparent);
                        transform.SetParent(trans);
                        obj.localPosition = Vector3.zero;
                        if (GlobalMgr.Instance.Items.Contains(text.text))
                        {
                            GlobalMgr.Instance.Items.Remove(text.text);
                        }
                        if (!GlobalMgr.Instance.Instrumenttray.Contains(text.text))
                        {
                            GlobalMgr.Instance.Instrumenttray.Add(text.text);
                        }
                        if (!GlobalMgr.Instance.Items.Contains(childText.text))
                        {
                            GlobalMgr.Instance.Items.Add(childText.text);
                        }
                        if (GlobalMgr.Instance.Instrumenttray.Contains(childText.text))
                        {
                            GlobalMgr.Instance.Instrumenttray.Remove(childText.text);
                        }
                        Eventsys.instance.Func();
                    }         
                }
                break;
            case "Backet":
                {
                    for (int i = 0; i < obj.transform.childCount; ++i)
                    {

                        if (obj.GetChild(i).childCount == 0)
                        {
                        transform.SetParent(obj.GetChild(i));
                        if (GlobalMgr.Instance.Items.Contains(text.text))
                         {
                                GlobalMgr.Instance.Items.Remove(text.text);
                         }
                        
                        if (!GlobalMgr.Instance.Instrumenttray.Contains(text.text))
                           {
                                GlobalMgr.Instance.Instrumenttray.Add(text.text);
                           }
                            Eventsys.instance.Func();
                        
                        break;

                        }
                        else
                        {
                            transform.SetParent(oriparent);
                            transform.localPosition = Vector3.zero;

                        }

                    }

                }
                break;
            case "cell":
                {
                    if (obj.childCount == 1)
                    {
                        Transform childT = obj.GetChild(0);
                        childT.SetParent(oriparent);
                        transform.SetParent(obj);
                        childT.localPosition = Vector3.zero;

                    }
                    else
                    {

                        transform.SetParent(obj);
                        if (GlobalMgr.Instance.Instrumenttray.Contains(text.text))
                        {
                        GlobalMgr.Instance.Instrumenttray.Remove(text.text);
                        }
                        if (!GlobalMgr.Instance.Items.Contains(text.text))
                        {
                            GlobalMgr.Instance.Items.Add(text.text);
                        }

                        Eventsys.instance.Func();

                    }
                }
                break;
            case "box":
                {
                    for (int i = 0; i < obj.transform.childCount; ++i)
                    {
                        print(i);
                        if (obj.GetChild(i).childCount == 0)
                        {
                            transform.SetParent(obj.GetChild(i));
                            if (GlobalMgr.Instance.Instrumenttray.Contains(text.text))
                            {
                                GlobalMgr.Instance.Instrumenttray.Remove(text.text);
                            }
                            if (!GlobalMgr.Instance.Items.Contains(text.text))
                            {
                                GlobalMgr.Instance.Items.Add(text.text);
                            }
                            Eventsys.instance.Func();
                            
                            break;

                        }
                        else
                        {
                            transform.SetParent(oriparent);
                            transform.localPosition = Vector3.zero;

                        }

                    }
                }
                break;
               
            default:
                transform.SetParent(oriparent);
                break;
        }
        
        transform.localPosition = Vector3.zero;
    }

}
