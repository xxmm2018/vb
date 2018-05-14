using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class teste : MonoBehaviour {
    public Sprite[] sprit;
    public float time=2;
    Image img;
    int index;

    void Start()
    {
        img = GetComponent<Image>();
    }
	void OnEnable ()
    {
        print("开始调用");
        InvokeRepeating("test",0,time);
	}
  
    void test()
    {
        index++;
        img.sprite =sprit[index%sprit.Length];
    }
}
