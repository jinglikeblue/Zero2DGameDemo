using IL.Zero;
using Zero;

namespace IL
{
    public class Global:ASingleton<Global>
    {
        public AudioDevice bgmDevice;
        public AudioDevice effectDevice;

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