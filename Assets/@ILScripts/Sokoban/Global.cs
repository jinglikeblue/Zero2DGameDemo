using IL.Zero;

namespace IL
{
    public class Global:ASingleton<Global>
    {
        /// <summary>
        /// 多国语言
        /// </summary>
        readonly public I18n i18n = new I18n();

        /// <summary>
        /// 菜单
        /// </summary>
        readonly public MenuModule menu = new MenuModule();

        /// <summary>
        /// 当前的关卡模型
        /// </summary>
        public LevelModel lv;
    }
}