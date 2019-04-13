using Jing.I18n;
using System.Collections.Generic;
using UnityEngine;

namespace Sokoban
{
    /// <summary>
    /// 数据中心
    /// </summary>
    public class DC
    {
        /// <summary>
        /// 单例模式
        /// </summary>
        public static DC ins = new DC();

        private DC()
        {

        }

        /// <summary>
        /// 多语言
        /// </summary>
        public I18n i18n;

        /// <summary>
        /// 游戏的存档模型
        /// </summary>
        public SaveModel save;

        /// <summary>
        /// 主相机
        /// </summary>
        public Camera mainCamera;

        /// <summary>
        /// 相机尺寸
        /// </summary>
        public Vector2 cameraSize;

        /// <summary>
        /// 选中的关卡
        /// </summary>
        public int selectedLevel = 1;

        /// <summary>
        /// 操作模式 1:触摸 2:键盘
        /// </summary>
        public int controlMode = 0;

        /// <summary>
        /// 操作记录
        /// </summary>
        public Stack<RecordVO> recordStack = new Stack<RecordVO>();

        /// <summary>
        /// 是否有广告
        /// </summary>
        public bool isThereAD = false;

        /// <summary>
        /// 是否所有关卡开放
        /// </summary>
        public bool isAllLevel = false;

        /// <summary>
        /// 是否可以分享
        /// </summary>
        public bool isShareEnable = false;
    }
}
