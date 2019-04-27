using IL.Zero;
using System;

namespace IL
{
    class GameEvent : ASingleton<GameEvent>
    {
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
