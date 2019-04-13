using UnityEngine;

namespace Jing
{
    /// <summary>
    /// 屏幕捕获
    /// </summary>
    public class ScreenCaptureUtil
    {
        /// <summary>
        /// 截取相机拍摄画面的大小到指定的矩形范围内（该方法建议在OnGUI事件中调用）
        /// </summary>
        /// <param name="came"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        static public Texture2D Capture(Camera came, Rect r)
        {
            RenderTexture rt = new RenderTexture((int)r.width, (int)r.height, 0);
            came.targetTexture = rt;
            came.Render();
            RenderTexture.active = rt;
            Texture2D screenShot = new Texture2D((int)r.width, (int)r.height, TextureFormat.RGB24, false);
            screenShot.ReadPixels(r, 0, 0);
            screenShot.Apply();
            came.targetTexture = null;
            RenderTexture.active = null;
            GameObject.Destroy(rt);
            return screenShot;
        }

        /// <summary>
        /// 截取屏幕指定范围的内容（该方法建议在OnGUI事件中调用）
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        static public Texture2D Capture(Rect rect)
        {            
            Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
            screenShot.ReadPixels(rect, 0, 0);
            screenShot.Apply();
            return screenShot;
        }

        /// <summary>
        /// 将当前画面保存到文件
        /// </summary>
        static public void Capture(string filePath)
        {
            ScreenCapture.CaptureScreenshot(filePath);
        }

    }
}
