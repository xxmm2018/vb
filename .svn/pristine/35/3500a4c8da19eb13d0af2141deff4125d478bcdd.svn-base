using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Label : UIBase
{
    GameObject _tempTag;
    GameObject _tag;
    PasteTag _cur;
    GameObject _tempTube;
    GameObject testTube;
    Button completeBtn;
    Transform main;
    void Awake()
    {
        main = transform.Find("main");
        completeBtn = transform.Find("main/Complete").GetComponent<Button>();
        completeBtn.onClick.AddListener(CompleteBtnFunc);
    }
    protected override void Handle(EEvet evt, params object[] args)
    {
        base.Handle(evt, args);
        if (evt==EVT)
        {
            _tempTag = Instantiate(GlobalMgr.Instance.PrintUIMgr.TagDic[0]);
            _tempTag.transform.SetParent(main);
            _tempTag.transform.localPosition = new Vector3(-600, 3, 0);
            _tempTag.transform.localScale = Vector3.one;
            _tag = _tempTag.transform.Find("Tag").gameObject;
            _cur = _tag.AddComponent<PasteTag>();
            InstTestTube();
        }
    }

    void CompleteBtnFunc()
    {
        VClient.instance.ClientSend(EEvet.Label);
    }

    //生成试管
    TestTube tube;
    List<Transform> pos = new List<Transform>();
    void InstTestTube()
    {

        if (GlobalMgr.Instance.CheckListUIMgr.s.Contains(ITEM.白细胞检测))
        {

            _tempTube = Resources.Load("UIPrefab/TestTube1") as GameObject;
            testTube = Instantiate(_tempTube);
            testTube.transform.SetParent(main);
            testTube.transform.localScale = Vector3.one;
            pos.Add(testTube.transform);
        }
        if (GlobalMgr.Instance.CheckListUIMgr.s.Contains(ITEM.葡萄糖鉴定))
        {
            _tempTube = Resources.Load("UIPrefab/TestTube1") as GameObject;
            testTube = Instantiate(_tempTube);
            testTube.transform.SetParent(main);
            testTube.transform.localScale = Vector3.one;
            pos.Add(testTube.transform);
        }
        if (GlobalMgr.Instance.CheckListUIMgr.s.Contains(ITEM.血小板检测))
        {
            _tempTube = Resources.Load("UIPrefab/TestTube") as GameObject;
            testTube = Instantiate(_tempTube);
            testTube.transform.SetParent(main);
            testTube.transform.localScale = Vector3.one;
            pos.Add(testTube.transform);
        }
        if (GlobalMgr.Instance.CheckListUIMgr.s.Contains(ITEM.血常规))
        {
            _tempTube = Resources.Load("UIPrefab/TestTube1") as GameObject;
            testTube = Instantiate(_tempTube);
            testTube.transform.SetParent(main);
            testTube.transform.localScale = Vector3.one;
            pos.Add(testTube.transform);
        }
        if (GlobalMgr.Instance.CheckListUIMgr.s.Contains(ITEM.血脂分析))
        {
            _tempTube = Resources.Load("UIPrefab/TestTube") as GameObject;
            testTube = Instantiate(_tempTube);
            testTube.transform.SetParent(main);
            testTube.transform.localScale = Vector3.one;
            pos.Add(testTube.transform);
        }

        for (int i = 0; i < pos.Count; i++)
        {
            pos[i].localPosition = new Vector3(-200 + i * 200, 0, 0);
        }

    }

    protected override void Inst()
    {
        base.Inst();
        title.text = "粘贴标签";
    }

}
