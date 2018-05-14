using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

public class BinoDevice : MonoBehaviour
{
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);

    [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr CreateFileMapping(int hFile, IntPtr lpAttributes, uint flProtect, uint dwMaxSizeHi, uint dwMaxSizeLow, string lpName);

    [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr OpenFileMapping(int dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, string lpName);

    [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr MapViewOfFile(IntPtr hFileMapping, uint dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow, uint dwNumberOfBytesToMap);

    [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
    public static extern bool UnmapViewOfFile(IntPtr pvBaseAddress);

    [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
    public static extern bool CloseHandle(IntPtr handle);

    [DllImport("kernel32", EntryPoint = "GetLastError")]
    public static extern int GetLastError();

    const int ERROR_ALREADY_EXISTS = 183;

    const int FILE_MAP_COPY = 0x0001;
    const int FILE_MAP_WRITE = 0x0002;
    const int FILE_MAP_READ = 0x0004;
    const int FILE_MAP_ALL_ACCESS = 0x0002 | 0x0004;

    const int PAGE_READONLY = 0x02;
    const int PAGE_READWRITE = 0x04;
    const int PAGE_WRITECOPY = 0x08;
    const int PAGE_EXECUTE = 0x10;
    const int PAGE_EXECUTE_READ = 0x20;
    const int PAGE_EXECUTE_READWRITE = 0x40;

    const int SEC_COMMIT = 0x8000000;
    const int SEC_IMAGE = 0x1000000;
    const int SEC_NOCACHE = 0x10000000;
    const int SEC_RESERVE = 0x4000000;

    const int INVALID_HANDLE_VALUE = -1;

    IntPtr m_hSharedMemoryFile = IntPtr.Zero;
    IntPtr m_pwData = IntPtr.Zero;
    bool m_bAlreadyExist = false;
    bool m_bInit = false;
    long m_MemSize = 0;
    byte[] _buffer = new byte[100];
    MemoryStream _memStream = null;
    BinaryReader _binaryReader = null;
    string _nativeSwitchKey = "qipc_sharedmemory_BinoInput0afa7eed6a06cc69eab73fe5cf38779789c3dc6f";//TO DO:需要修改
    string _nativeDataKey = "qipc_sharedmemory_BinoData052185813d00e51f3afa5b9eaaf9c43e4259a62a";
    Vector3 _top = new Vector3(0, 0, 0);
    Vector3 _bottom = new Vector3(0, 0, 0);
    /// <summary>
    /// 姿态参数:位置
    /// </summary>
    public Vector3 Position { get; set; }
    /// <summary>
    /// 姿态参数: 方向
    /// </summary>
    public Vector3 Direction { get; set; }
    /// <summary>
    /// 姿态参数:朝向
    /// </summary>
    public Quaternion Rotation { get; set; }
    /// <summary>
    /// 扎针点及半径范围
    /// </summary>
    public Vector3 cententPoint = new Vector3(10, -180, -90);
    public float range = 20;

    // Use this for initialization
    void Start()
    {
        Init(_nativeSwitchKey, 4);
        Write(System.BitConverter.GetBytes((int)1), 0, 4);// 打开获取数据开关
                                                          //Write(System.BitConverter.GetBytes((int)0),0,4);// 关闭获取数据开关
        Close();

        Init(_nativeDataKey, 40);
        _memStream = new MemoryStream(_buffer);
        _binaryReader = new BinaryReader(_memStream);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Read(ref _buffer, 0, 40);
        _memStream.Seek(0, SeekOrigin.Begin);
        _top.x = _binaryReader.ReadSingle();
        _top.y = _binaryReader.ReadSingle();
        _top.z = _binaryReader.ReadSingle();
        _bottom.x = _binaryReader.ReadSingle();
        _bottom.y = _binaryReader.ReadSingle();
        _bottom.z = _binaryReader.ReadSingle();
        Position = _top;
        Direction = (_top - _bottom).normalized;
        Rotation = Quaternion.FromToRotation(Vector3.up, Direction);
        //位置区域
        // Debug.Log(Vector3.Distance(_bottom, cententPoint));
        //Debug.Log("top"+_top);
        //Debug.DrawLine(cententPoint,_top,Color.red);
        //Debug.Log("bottom"+_bottom);
        //Debug.DrawLine(cententPoint, _bottom, Color.green);
        //Debug.Log(Vector3.Distance(_top,_bottom));
        //针是否在血管附近
        if (Vector3.Distance(_top, cententPoint) < range)
        {
            //角度  
            var angle = 90 - Rotation.eulerAngles.y;
            //深度  针尖进入皮肤的长度（标识点到针尖的长度减去标识点到皮肤表面的长度）
            float depth = 20 - Vector3.Distance(_top, cententPoint);
            //回血  ？？？？？？
            VEventManager.Instance.SendCommand(EEvet.Puncture, true, angle, depth, true);

        }
        //Debug.LogFormat("Top:{0} {1} {2}   Bottom:{3} {4} {5}", _top.x, _top.y, _top.z, _bottom.x, _bottom.y, _bottom.z);
    }

    void Update()
    {

    }

    private void OnDestroy()
    {
        Close();
    }

    /// <summary>
    /// 初始化共享内存
    /// </summary>
    /// <param name="strName">共享内存名称</param>
    /// <param name="lngSize">共享内存大小</param>
    /// <returns></returns>
    public int Init(string strName, long lngSize)
    {
        if (lngSize <= 0 || lngSize > 0x00800000) lngSize = 0x00800000;
        m_MemSize = lngSize;
        if (strName.Length > 0)
        {
            //创建内存共享体(INVALID_HANDLE_VALUE)
            //m_hSharedMemoryFile = CreateFileMapping(INVALID_HANDLE_VALUE, IntPtr.Zero, (uint)PAGE_READWRITE, 0, (uint)lngSize, strName);
            m_hSharedMemoryFile = OpenFileMapping(FILE_MAP_ALL_ACCESS, false, strName);
            if (m_hSharedMemoryFile == IntPtr.Zero)
            {
                m_bAlreadyExist = false;
                m_bInit = false;
                return 2; //创建共享体失败
            }
            else
            {
                int a = GetLastError();
                if (a == ERROR_ALREADY_EXISTS)  //已经创建
                {
                    m_bAlreadyExist = true;
                }
                else                                         //新创建
                {
                    m_bAlreadyExist = false;
                }
            }
            //---------------------------------------
            //创建内存映射
            m_pwData = MapViewOfFile(m_hSharedMemoryFile, FILE_MAP_WRITE, 0, 0, (uint)lngSize);
            if (m_pwData == IntPtr.Zero)
            {
                m_bInit = false;
                CloseHandle(m_hSharedMemoryFile);
                return 3; //创建内存映射失败
            }
            else
            {
                m_bInit = true;
                if (m_bAlreadyExist == false)
                {
                    //初始化
                }
            }
            //----------------------------------------
        }
        else
        {
            return 1; //参数错误     
        }

        return 0;     //创建成功
    }
    /// <summary>
    /// 关闭共享内存
    /// </summary>
    public void Close()
    {
        if (m_bInit)
        {
            UnmapViewOfFile(m_pwData);
            CloseHandle(m_hSharedMemoryFile);
            m_bInit = false;
            m_pwData = IntPtr.Zero;
            m_hSharedMemoryFile = IntPtr.Zero;
            m_MemSize = 0;
        }
    }

    /// <summary>
    /// 读数据
    /// </summary>
    /// <param name="bytData">数据</param>
    /// <param name="lngAddr">起始地址</param>
    /// <param name="lngSize">个数</param>
    /// <returns></returns>
    public int Read(ref byte[] bytData, int lngAddr, int lngSize)
    {
        if (lngAddr + lngSize > m_MemSize) return 2; //超出数据区
        if (m_bInit)
        {
            Marshal.Copy(m_pwData, bytData, lngAddr, lngSize);
        }
        else
        {
            return 1; //共享内存未初始化
        }
        return 0;     //读成功
    }

    /// <summary>
    /// 写数据
    /// </summary>
    /// <param name="bytData">数据</param>
    /// <param name="lngAddr">起始地址</param>
    /// <param name="lngSize">个数</param>
    /// <returns></returns>
    public int Write(byte[] bytData, int lngAddr, int lngSize)
    {
        if (lngAddr + lngSize > m_MemSize) return 2; //超出数据区
        if (m_bInit)
        {
            Marshal.Copy(bytData, lngAddr, m_pwData, lngSize);
        }
        else
        {
            return 1; //共享内存未初始化
        }
        return 0;     //写成功
    }
}