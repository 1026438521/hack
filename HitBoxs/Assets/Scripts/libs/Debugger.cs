using System;
using UnityEngine;

class Debugger
{
    public static bool ifLog = true;
    public static bool ifStartUIShowTesting = true;//开始界面测试按钮显示
    public static bool ifShowGUI = true;//是否显示帧率等信息
#if UNITY_EDITOR
    public static Action<object> Log = Debug.Log;
    // public static void Log(object message) { }
#else
    public static void Log(object message)
    {
        if (ifLog)
        {
            Debug.Log(message);
        }
    }
#endif


}
