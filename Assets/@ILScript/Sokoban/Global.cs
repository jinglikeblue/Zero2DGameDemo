using IL.Zero;

namespace IL
{
    public class Global:ASingleton<Global>
    {
        /// <summary>
        /// 菜单
        /// </summary>
        readonly public MenuModule menu = new MenuModule();

        public SingularViewLayer panelLayer;
    }
}