using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DetectionItemUI : UIBase
{
    Transform grid;
    GameObject _tempItem;
    GameObject _curItem;
    Button btn;
    CheckItem cur;

    void Start()
    {
        btn = transform.Find("main/Button").GetComponent<Button>();
        grid = transform.Find("main/Box/Scroll/ScrollView/Grid");

        btn.onClick.AddListener(BtnOnClick);
    }





    void BtnOnClick()
    {
        //发送选中物品的清单
        VClient.instance.ClientSend(EEvet.Choice, GlobalMgr.Instance.CheckList);
    }


    protected override void Handle(EEvet evt, params object[] args)
    {
        base.Handle(evt, args);
        if (evt==EVT)
        {
            foreach (var item in GlobalMgr.Instance.Instrumenttray)
            {
                print("item" + item);
                _tempItem = Resources.Load("UIPrefab/Item/" + item) as GameObject;
                _curItem = Instantiate(_tempItem);
                _curItem.transform.localScale = new Vector3(1, 1, 1);
                for (int i = grid.childCount - 1; i >= 0; i--)
                {
                    if (grid.GetChild(i).childCount == 0)
                    {
                        _curItem.transform.SetParent(grid.GetChild(i));
                        _curItem.transform.localPosition = Vector3.zero;
                        _curItem.transform.localScale = Vector3.one;
                    }
                }
                cur = _curItem.AddComponent<CheckItem>();
            }
        }
     
    }

    protected override void Inst()
    {
        base.Inst();
        title.text = "用物选择";
    }
}
