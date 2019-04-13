using Jing;
using Jing.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sokoban
{
    public class ShareWin : MonoBehaviour
    {

        public RectTransform cutMark;
        public RectTransform screenCut;
        public Texture2D shareTemp;
        public Canvas canvas;

        /// <summary>
        /// 是否捕获屏幕
        /// </summary>
        bool _isCaptureScreen = false;

        Texture2D _sharePic;

        public BitmapFont textLevel;

        /// <summary>
        /// 分享模式 0：无   1：朋友圈   2：好友
        /// </summary>
        int _shareMode = 0;

        Texture2D _screenShot;

        void Start()
        {

        }

        private void OnGUI()
        {
            if (_isCaptureScreen)
            {
                //先截取屏幕到指定矩形大小
                var r = new Rect(0, 0, 580, 326);
                Texture2D ss = ScreenCaptureUtil.Capture(Camera.main, r);
                screenCut.GetComponent<Image>().sprite = Sprite.Create(ss, r, Vector2.zero);
                _screenShot = ss;
                _isCaptureScreen = false;
            }

            if(_shareMode > 0)
            {
                Texture2D shareImg = CutType2();
                byte[] bytes = shareImg.EncodeToJPG();
                //MultiPlatformUtil.ShareImg(bytes, _shareMode == 1 ? true : false);
                //截取分享的图片
                _shareMode = 0;
            }
        }

        /// <summary>
        /// 将UGUI显示的窗口截图，然后缩放到分享的图片的大小，然后分享出去
        /// </summary>
        /// <returns></returns>
        Texture2D CutType1()
        {
            RectInt shareRect = new RectInt(0, 0, (int)cutMark.sizeDelta.x, (int)cutMark.sizeDelta.y);

            Debug.Log(canvas.scaleFactor);
            //获得需要截取的范围
            //Debug.Log(cutMark.position);
            Rect uiRect = new Rect(cutMark.position.x * canvas.scaleFactor, cutMark.position.y * canvas.scaleFactor, cutMark.sizeDelta.x * canvas.scaleFactor, cutMark.sizeDelta.y * canvas.scaleFactor);
            //Rect uiRect = new Rect(0, 0, 100, 100);
            Texture2D uiShot = ScreenCaptureUtil.Capture(uiRect);
            //uiShot.Resize(shareRect.width, shareRect.height);
            //uiShot.Apply();
            Color[] colors = uiShot.GetPixels();
            Texture2D t = new Texture2D((int)uiRect.width, (int)uiRect.height);
            t.SetPixels(0, 0, uiShot.width, uiShot.height, colors);

            return uiShot;
        }

        /// <summary>
        /// 将屏幕截图直接写到模板图片的矩形范围内分享出去
        /// </summary>
        /// <returns></returns>
        Texture2D CutType2()
        {
            shareTemp.SetPixels(48, 43, _screenShot.width, _screenShot.height, _screenShot.GetPixels());
            return shareTemp;
        }

        private void OnEnable()
        {
            textLevel.text = DC.ins.selectedLevel.ToString();
            GetComponent<WindowTween>().Show(()=> {
                _isCaptureScreen = true;
            });            
        }

        void Update()
        {

        }

        public void Close()
        {
            GetComponent<WindowTween>().Hide();
        }

        public void Share2Friend()
        {
            //TalkingData.ins.OnEvent("share", "friend", "1");
            _shareMode = 2;
        }

        public void Share2Timeline()
        {
            //TalkingData.ins.OnEvent("share", "timeline", "1");
            _shareMode = 1;
        }
    }
}
