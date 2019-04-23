using System;
using UnityEngine;
using UnityEngine.UI;
using Zero;

namespace Sokoban
{
    public class PreloadUI : MonoBehaviour
    {

        public BitmapText textProgress;
        public Preload preload;
        public Text textError;

        void Start()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Application.targetFrameRate = 30;
            preload.onProgress += OnProgress;
            preload.onStateChange += OnStageChange;
            preload.onError += OnError;
            preload.StartPreload(new ILRuntimeGenerics());
        }

        private void OnError(string err)
        {
            textProgress.gameObject.SetActive(false);
            textError.gameObject.SetActive(true);
            textError.text = err;
        }

        private void OnProgress(float progress, long total)
        {
            textProgress.Text = ((int)(progress * 100f)).ToString();
        }

        private void OnStageChange(Preload.EState obj)
        {
            string work = "";
            switch (obj)
            {
                case Preload.EState.UNZIP_PACKAGE:
                    work = "初始化资源...";
                    break;
                case Preload.EState.SETTING_UPDATE:
                    work = "设置更新...";
                    break;
                case Preload.EState.CLIENT_UDPATE:
                    work = "客户端更新...";
                    break;
                case Preload.EState.RES_UPDATE:
                    work = "资源更新...";
                    break;
                case Preload.EState.STARTUP:
                    work = "进入游戏...";
                    break;
            }
            Log.I(work);
        }
    }
}