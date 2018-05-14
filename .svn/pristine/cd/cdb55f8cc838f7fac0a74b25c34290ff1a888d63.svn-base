using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class PasteTag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    PasteTag _cur;//实例一个待用对象
    Transform oriparent;//记录父物体
    Transform curPos;//记录当前位置
    Vector3 distance;//记录与鼠标位置
    Image item;
    CanvasGroup canvasGroup;
    CanvasGroup _tempCan;
    Transform granparent;//父物体的父物体
    GameObject _temp;//对象引用
    GameObject _tag;//对象引用
    Text aim;//粘贴之后试管显示刻度
    int origina=-310;//标签原先位置的y坐标
    void Start()
    {
        granparent = transform.parent.parent;
        item = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
        oriparent = transform.parent;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        
        print(oriparent.tag);
        item.transform.SetParent(GlobalMgr.Instance.UIMgr._mainCanvas_p.transform);
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

        if (eventData.pointerEnter == null)
        {
            transform.SetParent(oriparent);
            transform.localPosition = Vector3.zero;
            return;
        }
        else
        {
            obj = eventData.pointerEnter.transform;
        }
        //检测下方是否是试管
        switch (obj.tag)
        {
            case "TestTube":
                print(obj.transform.childCount);
                if (obj.transform.childCount<=3)
                {
                    transform.SetParent(obj);
                    transform.localScale = Vector3.one / 2;
                    transform.localPosition = Vector3.zero;
                    canvasGroup.blocksRaycasts = false;
                    NextTagFunc();
                    aim = obj.transform.Find("Aim").GetComponent<Text>();
                    aim.gameObject.SetActive(true);
                   GlobalMgr.Instance.PrintUIMgr. testtubeList.Add(obj.gameObject);
                }
                else
                {
                    transform.SetParent(oriparent);
                    transform.localPosition = new Vector3(0, origina, 0);
                }
                break;
            default:
                transform.SetParent(oriparent);
                transform.localPosition = new Vector3(0, origina, 0);
                break;

        }


    }
    /// <summary>
    /// 显示下一个需要粘贴的标签
    /// </summary>
    void NextTagFunc()
    {
        if (GlobalMgr.Instance.PrintUIMgr.ItemCount < GlobalMgr.Instance.PrintUIMgr.TagDic.Count)
        {
            _temp = Instantiate(GlobalMgr.Instance.PrintUIMgr.TagDic[GlobalMgr.Instance.PrintUIMgr.ItemCount]);
            _temp.transform.SetParent(granparent);
            _temp.transform.localPosition = new Vector3(-600, 3, 0);
            _temp.transform.localScale = Vector3.one;
            _tag = _temp.transform.Find("Tag").gameObject;
            _cur = _tag.AddComponent<PasteTag>();
            GlobalMgr.Instance.PrintUIMgr.ItemCount++;
        }
    }
}
