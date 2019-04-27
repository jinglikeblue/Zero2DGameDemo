using IL.Zero;
using System;

namespace IL
{
    class GameEvent : ASingleton<GameEvent>
    {
        public Action onScreenSizeChange;

        /// <summary>
        /// 关卡完成
        /// </summary>
        public Action onLevelComplete;

        public void RemoveAllListeners()
        {
            onLevelComplete = null;
        }
    }
}
