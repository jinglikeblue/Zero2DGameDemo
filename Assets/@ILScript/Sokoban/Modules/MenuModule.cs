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
            ViewFactory.Register<MenuPanel>(AssetBundleName.PREFABS, "MenuPanel");
        }

        protected override void Dispose()
        {
            
        }

        public void ShowMenu()
        {
            Global.Ins.panelLayer.Show<MenuPanel>();
        }
    }
}