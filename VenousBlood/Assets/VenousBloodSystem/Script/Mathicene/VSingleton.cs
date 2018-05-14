using UnityEngine;
using System.Collections;

public  class VSingleton:MonoBehaviour
{
    //private static VSingleton instance;

    ////private VSingleton()     //将构造方法设为private，防止外界利用new创建实例。
    ////{

    ////}

    //public static VSingleton GetInstance()   //此方法用于获得本类实例的唯一全局访问点
    //{
    //    if (instance == null)           //若实例不存在则new一个新实例，否则返回已有实例
    //    {
    //        instance = new VSingleton();
    //    }
    //    return instance;
    //}
}
