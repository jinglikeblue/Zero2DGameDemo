using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sokoban
{
    /// <summary>
    /// 单位对象
    /// </summary>
    public class AUnitVO
    {
        public EUnitType type;
        public ushort x;
        public ushort y;
    }

    /// <summary>
    /// 操作记录VO
    /// </summary>
    public class RecordVO
    {
        /// <summary>
        /// 箱子移动的方向
        /// </summary>
        public EDir dir;

        /// <summary>
        /// 角色所在位置
        /// </summary>
        public Vector2 roleTile;

        /// <summary>
        /// 箱子所在位置(推动前）
        /// </summary>
        public Vector2 boxTile;

        /// <summary>
        /// 箱子的索引
        /// </summary>
        public int boxIdx;

        public RecordVO(Vector2 roleTile, Vector2 boxTile, EDir dir, int boxIdx)
        {
            this.dir = dir;
            this.roleTile = roleTile;
            this.boxTile = boxTile;
            this.boxIdx = boxIdx;
        }
    }


    /// <summary>
    /// 通过的关卡的信息
    /// </summary>
    [Serializable]
    public class CompleteLevelInfoVO
    {
        /// <summary>
        /// 通过的关卡
        /// </summary>
        public int level;

        /// <summary>
        /// 推动箱子的次数
        /// </summary>
        public int stepCount;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <param name="stepCount"></param>
        public CompleteLevelInfoVO(int level, int stepCount)
        {
            this.level = level;
            this.stepCount = stepCount;
        }
    }

    /// <summary>
    /// 游戏存档数据
    /// </summary>
    [Serializable]
    public class GameSaveVO
    {
        /// <summary>
        /// 最后玩的关卡
        /// </summary>
        public int lastLevel = -1;

        /// <summary>
        /// 已解锁的关卡数(广告模式)
        /// </summary>
        public int unlockLevel = -1;

        /// <summary>
        /// 已通关关卡信息
        /// </summary>
        public List<CompleteLevelInfoVO> completeList = new List<CompleteLevelInfoVO>();
    }


}
