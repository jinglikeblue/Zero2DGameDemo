﻿using DG.Tweening;
using IL.Zero;
using Jing;
using System.Text;
using UnityEngine;
using Zero;

namespace IL
{
    public class ILMain
    {
        public static void Main()
        {
            DOTween.defaultEaseType = Ease.Linear;
            DOTween.Init();
            Application.targetFrameRate = 30;            
            UIPanelMgr.Ins.Init(GameObject.Find("UIPanel").transform);
            StageMgr.Ins.Init(GameObject.Find("Stage").transform);
            UIWinMgr.Ins.Init(GameObject.Find("UIWin").transform);
            Init18N();
            RegistViews();            
            Global.Ins.menu.ShowMenu();

            AudioPlayer.Ins.PlayBGM(ResMgr.Ins.Load<AudioClip>("audios/bgm"));

            if (Debug.isDebugBuild)
            {
                GUIDeviceInfo.Show();
            }
        }

        static void Init18N()
        {
            var cn = ResMgr.Ins.Load<TextAsset>("configs/i18n/CN");
            CSVFile csv = new CSVFile(cn.bytes, Encoding.UTF8);            
            Global.Ins.i18n.SetData(csv.Data);
        }

        static void RegistViews()
        {
            ViewFactory.Register<MenuPanel>(AssetBundleName.PREFABS, "MenuPanel");
            ViewFactory.Register<LoadingWin>(AssetBundleName.PREFABS, "LoadingWin");
            ViewFactory.Register<GamePanel>(AssetBundleName.PREFABS, "GamePanel");
            ViewFactory.Register<GameStage>(AssetBundleName.PREFABS, "GameStage");
            ViewFactory.Register<CreditsWin>(AssetBundleName.PREFABS, "CreditsWin");
            ViewFactory.Register<LevelSelectWin>(AssetBundleName.PREFABS, "LevelSelectWin");
            ViewFactory.Register<MsgWin>(AssetBundleName.PREFABS, "MsgWin");
        }
    }
}