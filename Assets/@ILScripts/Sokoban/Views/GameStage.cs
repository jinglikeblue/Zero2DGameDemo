using System;
using System.Collections.Generic;
using IL.Zero;
using UnityEngine;
using Zero;

namespace IL
{
    class GameStage : AView
    {
        LevelModel _lv;
        Transform _contents;
        List<BaseUnit> _unitList;
        RoleUnit _roleUnit;        

        protected override void OnInit()
        {
            _contents = GetChild("Contents");

            float off = Define.MAP_TILE_COUNT_OF_SIDE * Define.TILE_SIZE / -2f;
            _contents.localPosition = new Vector2(off, off);

            _unitList = new List<BaseUnit>();

            _lv = Global.Ins.lv;
            CreateTargets();
            CreateBlocks();
            CreateBoxes();
            CreateRole();
        }

        protected override void OnEnable()
        {            
            ILBridge.Ins.onUpdate += OnUpdate;            
        }

        protected override void OnDisable()
        {
            ILBridge.Ins.onUpdate -= OnUpdate;            
        }        

        private void OnUpdate()
        {
            //进行排序
            DepthSort();
        }

        void DepthSort()
        {
            var st = new SortTool<BaseUnit>();
            foreach (var ub in _unitList)
            {
                st.AddItem((int)(ub.SortValue * 100), ub);
            }
            var sortList = st.Sort(true);
            for (int i = 0; i < sortList.Length; i++)
            {
                sortList[i].SetSortValue(i);
            }
        }

        private void CreateTargets()
        {
            var prefab = ResMgr.Ins.Load<GameObject>("prefabs/game/Target");
            foreach (var vo in _lv.targets)
            {
                var unit = ViewFactory.Create<BaseUnit>(prefab, _contents, EUnitType.TARGET);
                unit.SetTile(vo.x, vo.y);                
            }
        }

        private void CreateBlocks()
        {
            var prefab = ResMgr.Ins.Load<GameObject>("prefabs/game/Block");
            foreach (var vo in _lv.blocks)
            {
                var unit = ViewFactory.Create<BaseUnit>(prefab, _contents, EUnitType.BLOCK);
                unit.SetTile(vo.x, vo.y);
                _unitList.Add(unit);
            }
        }

        private void CreateBoxes()
        {
            var prefab = ResMgr.Ins.Load<GameObject>("prefabs/game/Box");
            foreach (var vo in _lv.boxes)
            {
                var unit = ViewFactory.Create<BoxUnit>(prefab, _contents, EUnitType.BOX);
                unit.SetTile(vo.x, vo.y);
                _unitList.Add(unit);
            }
        }

        private void CreateRole()
        {
            var prefab = ResMgr.Ins.Load<GameObject>("prefabs/game/Role");
            foreach (var vo in _lv.roles)
            {
                var unit = ViewFactory.Create<RoleUnit>(prefab, _contents, EUnitType.ROLE);
                unit.SetTile(vo.x, vo.y);
                _unitList.Add(unit);
                _roleUnit = unit;
            }
        }

        public bool MoveRole(EDir dir)
        {
            if(EDir.NONE == dir)
            {
                return false;
            }

            if(_roleUnit.IsMoving)
            {
                return false;
            }

            var endTile = GetTileMoveTo(_roleUnit.Tile, dir);

            //检查目标位置是否有阻挡
            var block = GetUnitInTile(endTile);
            if(null != block)
            {
                if(EUnitType.BLOCK == block.UnitType)
                {
                    return false;
                }

                if(EUnitType.BOX == block.UnitType)
                {
                    //推动箱子
                    var moveBoxSuccess = MoveBox(block as BoxUnit, dir);
                    if(false == moveBoxSuccess)
                    {
                        //箱子推动失败
                        return false;
                    }
                }
            }

            return _roleUnit.Move(dir, endTile);
        }

        bool MoveBox(BoxUnit box, EDir dir)
        {
            if(null == box)
            {
                return false;
            }

            var endTile = GetTileMoveTo(box.Tile, dir);
            //检查目标位置是否有阻挡
            var block = GetUnitInTile(endTile);
            if (null != block && (EUnitType.BLOCK == block.UnitType || EUnitType.BOX == block.UnitType))
            {
                return false;
            }

            box.onMoveEnd += OnBoxMoveEnd;
            return box.Move(dir, endTile);
        }

        private void OnBoxMoveEnd(MoveableUnit unit)
        {
            unit.onMoveEnd -= OnBoxMoveEnd;

            if(_lv.IsTarget((ushort)unit.Tile.x, (ushort)unit.Tile.y))
            {
                (unit as BoxUnit).SetIsAtTarget(true);
                //播放一个效果
                ViewFactory.Create<BangEffect>("prefabs/game", "BangEffect", unit.gameObject.transform);
                CheckLevelComplete();
            }
            else
            {
                (unit as BoxUnit).SetIsAtTarget(false);
            }
        }

        void CheckLevelComplete()
        {
            foreach (var unit in _unitList)
            {
                if (unit.UnitType == EUnitType.BOX)
                {
                    if (false == _lv.IsTarget((ushort)unit.Tile.x, (ushort)unit.Tile.y))
                    {
                        return;
                    }
                }
            }

            MsgWin.Show("Congratulations!", false, () => {
                //通知关卡完成
                GameEvent.Ins.onLevelComplete?.Invoke();
            });            
        }

        /// <summary>
        /// 得到格子上的单位
        /// </summary>
        /// <param name="tile"></param>
        /// <returns></returns>
        BaseUnit GetUnitInTile(Vector2Int tile)
        {
            foreach(var unit in _unitList)
            {
                if(unit.Tile == tile)
                {
                    return unit;
                }
            }
            return null;
        }

        Vector2Int GetTileMoveTo(Vector2Int startTile, EDir dir)
        {
            Vector2Int endTile = startTile;
            switch (dir)
            {
                case EDir.UP:
                    endTile = startTile + Vector2Int.up;
                    break;
                case EDir.DOWN:
                    endTile = startTile + Vector2Int.down;
                    break;
                case EDir.LEFT:
                    endTile = startTile + Vector2Int.left;
                    break;
                case EDir.RIGHT:
                    endTile = startTile + Vector2Int.right;
                    break;
            }
            return endTile;
        }

        /// <summary>
        /// 是否是箱子的目标格子
        /// </summary>
        /// <param name="tile"></param>
        /// <returns></returns>
        //bool IsTargetTile(Vector2Int tile)
        //{
        //    foreach (var vo in _lv.targets)
        //    {
        //        if (vo.x == tile.x && vo.y == tile.y)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}
    }
}
