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
            Application.targetFrameRate = 60;
            UIPanelMgr.Ins.Init(GameObject.Find("UIPanel").transform);
            StageMgr.Ins.Init(GameObject.Find("Stage").transform);
            UIWinMgr.Ins.Init(GameObject.Find("UIWin").transform);
            Init18N();
            RegistViews();            
            Global.Ins.menu.ShowMenu();
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
            ViewFactory.Register<LoadingPanel>(AssetBundleName.PREFABS, "LoadingPanel");
            ViewFactory.Register<GamePanel>(AssetBundleName.PREFABS, "GamePanel");
            ViewFactory.Register<CreditsWin>(AssetBundleName.PREFABS, "CreditsWin");
            ViewFactory.Register<LevelSelectWin>(AssetBundleName.PREFABS, "LevelSelectWin");
        }
    }
}