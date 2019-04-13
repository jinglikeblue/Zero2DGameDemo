using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokoban
{
    /// <summary>
    /// 方向
    /// </summary>
    public enum EDir
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        NONE
    }

    /// <summary>
    /// 单位的类型
    /// </summary>
    public enum EUnitType
    {
        ROLE,
        BLOCK,
        BOX,
        TARGET
    }

    public class Define
    {
        /// <summary>
        /// 格子大小
        /// </summary>
        static public float TILE_SIZE = 0.48f;

        /// <summary>
        /// 地图大小（MAP_SIZE * MAP_SIZE 格子数）
        /// </summary>
        static public int MAP_SIZE = 16;

        /// <summary>
        /// 关卡总数
        /// </summary>
        static public int LEVEL_AMOUNT = 97;
    }
}
