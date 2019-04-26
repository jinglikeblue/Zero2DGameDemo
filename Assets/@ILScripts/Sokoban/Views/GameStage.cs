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
        public RoleUnit RoleUnit { get; private set; }

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
                RoleUnit = unit;
            }
        }

        
    }
}
