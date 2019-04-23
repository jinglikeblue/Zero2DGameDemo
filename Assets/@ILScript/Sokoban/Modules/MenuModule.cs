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
            
        }

        protected override void Dispose()
        {
            
        }

        public void ShowMenu()
        {
            UIPanelMgr.Ins.Switch<MenuPanel>();            
        }
    }
}