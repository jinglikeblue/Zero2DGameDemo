using Jing.Notice;
using System;
/// <summary>
/// 游戏通知
/// </summary>
public class GameNotice
{    
    /// <summary>
    /// 原生代码回调
    /// </summary>
    public static Action<string> nativeCallback;

    /// <summary>
    /// 视频广告播放完成
    /// </summary>
    public static Action audioAdPlayCompleted;

    /// <summary>
    /// 视频广告播放中断
    /// </summary>
    public static Action audioAdPlayInterrupted;
}
