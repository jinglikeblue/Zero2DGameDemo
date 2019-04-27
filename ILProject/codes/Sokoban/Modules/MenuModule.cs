using System;
using IL.Zero;

namespace IL
{
    /// <summary>
    /// 菜单模块
    /// </summary>
    public class MenuModule : BaseModule
    {
        public MenuModule()
        {
            GameEvent.Ins.onLevelComplete += OnLevelComplete;
        }

        private void OnLevelComplete()
        {
            if (Global.Ins.lv.id == Define.LEVEL_AMOUNT)
            {
                EnterLevel(1);
            }
            else
            {
                EnterLevel(Global.Ins.lv.id + 1);
            }
        }

        protected override void Dispose()
        {
            
        }

        public void ShowMenu(bool isTween = false)
        {
            if (isTween)
            {
                var loading = UIWinMgr.Ins.Open<LoadingWin>();
                loading.onSwitch += () =>
                {                    
                    UIPanelMgr.Ins.Switch<MenuPanel>();
                };
            }
            else
            {                
                UIPanelMgr.Ins.Switch<MenuPanel>();
            }
        }

        public void EnterLevel(int level)
        {
            Global.Ins.lv = new LevelModel(level);
            var loading = UIWinMgr.Ins.Open<LoadingWin>();
            loading.onSwitch += () =>
            {                
                UIPanelMgr.Ins.Switch<GamePanel>();
            };                       
        }
    }
}