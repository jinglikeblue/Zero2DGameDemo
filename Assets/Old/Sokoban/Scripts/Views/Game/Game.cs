using Jing.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sokoban
{
    public class Game : MonoBehaviour
    {
        [Header("角色的prefab")]
        [SerializeField]
        GameObject rolePrefab;

        [Header("墙壁的prefab")]
        [SerializeField]
        GameObject blockPrefab;

        [Header("箱子的prefab")]
        [SerializeField]
        GameObject boxPrefab;

        [Header("目标位置的prefab")]
        [SerializeField]
        GameObject targetPrefab;

        [Header("放置元素的容器")]
        [SerializeField]
        Transform _elements;

        [Header("UI可见度")]
        [SerializeField]
        CanvasGroup _uiCG;

        [Header("摇杆")]
        [SerializeField]
        Joystick _stick;

        [Header("分享按钮")]
        public GameObject btnShare;

        LevelModel _lv;

        List<UnitBase> _unitList;
        List<UnitBase> _dynamicUnitList;

        Role _role;

        private void Awake()
        {
            if (DC.ins.controlMode == 2)
            {
                _uiCG.alpha = 0;
            }
        }

        void Start()
        {
            DC.ins.recordStack.Clear();
            _unitList = new List<UnitBase>();
            _dynamicUnitList = new List<UnitBase>();

            InitElementsPos();
            _lv = new LevelModel(DC.ins.selectedLevel);
            CreateTargets();
            CreateBlocks();
            CreateBoxes();
            CreateRole();

            _stick.camera = DC.ins.mainCamera;
            _stick.onStickValueChange += OnStickValueChange;
            _role.checkMoveEnableHandler += CheckMoveEnableHandler;

            DC.ins.save.LastLevel = _lv.id;

            btnShare.SetActive(DC.ins.isShareEnable);
        }

        private void OnEnable()
        {
            if (DC.ins.isThereAD)
            {
                //AdMob.ins.BannerSet(true);
            }
        }

        private void OnDisable()
        {
            if (DC.ins.isThereAD)
            {
                //AdMob.ins.BannerSet(false);
            }
        }

        private void OnDestroy()
        {

        }

        private bool CheckMoveEnableHandler(Vector3 startTile, Vector3 endTile, EDir dir)
        {
            var x = (ushort)endTile.x;
            var y = (ushort)endTile.y;
            if (_lv.IsBlock(x, y))
            {
                return false;
            }

            var box = GetBox(x, y) as Box;
            
            if (null != box)
            {
                if(box.IsMoveing)
                {
                    //有箱子正在移动
                    return false;
                }

                //是箱子，检查箱子后面是否有阻挡
                switch (dir)
                {
                    case EDir.UP:
                        y += 1;
                        break;
                    case EDir.DOWN:
                        y -= 1;
                        break;
                    case EDir.LEFT:
                        x -= 1;
                        break;
                    case EDir.RIGHT:
                        x += 1;
                        break;
                }

                if (_lv.IsBlock(x, y) || null != GetBox(x, y))
                {
                    return false;
                }

                DC.ins.recordStack.Push(new RecordVO(startTile, endTile, dir, _dynamicUnitList.IndexOf(box)));
                //推动了箱子
                box.GetComponent<Box>().Move(dir);
            }

            return true;
        }

        UnitBase GetBox(ushort tileX, ushort tileY)
        {
            foreach (var ub in _dynamicUnitList)
            {
                if (ub.UnitType == EUnitType.BOX)
                {
                    Box box = ub as Box;
                    if (box.Tile.x == tileX && box.Tile.y == tileY)
                    {
                        return ub;
                    }                    
                    else if(box.IsMoveing)
                    {
                        if (box.Tile2.x == tileX && box.Tile2.y == tileY)
                        {
                            return ub;
                        }
                    }
                }
            }
            return null;
        }

        private void OnStickValueChange(Vector2 value)
        {
            EDir newDir;
            if (value == Vector2.zero)
            {
                newDir = EDir.NONE;
            }
            else
            {
                value = value.normalized;
                if (Math.Abs(value.x) > Math.Abs(value.y))
                {
                    //横向移动
                    if (value.x < 0)
                    {
                        newDir = EDir.LEFT;
                    }
                    else
                    {
                        newDir = EDir.RIGHT;
                    }
                }
                else
                {
                    //纵向移动
                    if (value.y < 0)
                    {
                        newDir = EDir.DOWN;
                    }
                    else
                    {
                        newDir = EDir.UP;
                    }
                }
            }
            _role.Move(newDir);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Revoke();
                return;
            }

            if (Input.GetKeyDown(KeyCode.Menu))
            {
                BackMenu();
                return;
            }

            DepthSort();
        }

        void DepthSort()
        {
            var st = new SortTool<UnitBase>();
            foreach (var ub in _unitList)
            {
                st.AddItem((int)(ub.GetSortValue() * 100), ub);
            }
            var sortList = st.Sort(true);
            for (int i = 0; i < sortList.Length; i++)
            {
                sortList[i].SetSortValue(i);
            }
        }

        private void InitElementsPos()
        {
            int halfSize = Define.MAP_SIZE >> 1;
            float off = -halfSize * Define.TILE_SIZE;
            _elements.transform.localPosition = new Vector2(off, off);
        }

        private void CreateTargets()
        {
            var st = new SortTool<UnitBase>();
            foreach (var vo in _lv.targets)
            {
                var ub = AddUnit(targetPrefab, vo.x, vo.y, false);
                st.AddItem((int)(ub.GetSortValue() * 100), ub);
            }

            var sortList = st.Sort(true);
            for (int i = 0; i < sortList.Length; i++)
            {
                sortList[i].SetSortValue(i + 1);
            }
        }

        private void CreateRole()
        {
            foreach (var vo in _lv.roles)
            {
                _role = AddUnit(rolePrefab, vo.x, vo.y).GetComponent<Role>();
                _role.SetTile(vo.x, vo.y);
            }
        }

        private void CreateBoxes()
        {
            foreach (var vo in _lv.boxes)
            {
                var box = AddUnit(boxPrefab, vo.x, vo.y).GetComponent<Box>();
                box.onBoxOnTileHandler += OnBoxOnTileHandler;
                box.SetTile(vo.x, vo.y);
            }
        }

        /// <summary>
        /// 当箱子移动到格子上的事件
        /// </summary>
        /// <param name="box"></param>
        /// <param name="tile"></param>
        /// <returns></returns>
        private bool OnBoxOnTileHandler(Box box, Vector3 tile)
        {
            if (_lv.IsTarget((ushort)tile.x, (ushort)tile.y))
            {
                CheckLevelPass();
                return true;
            }
            return false;
        }

        void CheckLevelPass()
        {
            foreach (var ub in _dynamicUnitList)
            {
                if (ub.UnitType == EUnitType.BOX)
                {
                    if (false == _lv.IsTarget((ushort)ub.Tile.x, (ushort)ub.Tile.y))
                    {
                        return;
                    }
                }
            }

            DC.ins.save.UpdateLevelStepCount(_lv.id, DC.ins.recordStack.Count);
            Main.current.EnterLevel(DC.ins.selectedLevel + 1);
        }

        private void CreateBlocks()
        {
            foreach (var vo in _lv.blocks)
            {
                AddUnit(blockPrefab, vo.x, vo.y);
            }
        }

        /// <summary>
        /// 添加一个单元
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="tileX"></param>
        /// <param name="tileY"></param>
        /// <returns></returns>
        private UnitBase AddUnit(GameObject prefab, ushort tileX, ushort tileY, bool add2List = true)
        {
            var block = GameObject.Instantiate(prefab);
            block.transform.SetParent(_elements);
            UnitBase ub = block.GetComponent<UnitBase>();
            ub.SetTile(tileX, tileY);
            if (add2List)
            {
                _unitList.Add(ub);
            }
            if (ub.UnitType == EUnitType.ROLE || ub.UnitType == EUnitType.BOX)
            {
                _dynamicUnitList.Add(ub);
            }
            return ub;
        }

        /// <summary>
        /// 返回菜单
        /// </summary>
        public void BackMenu()
        {
            Main.current.msgBox.Show("Leave？", () =>
            {
                Main.current.EnterMenu();
            });
        }

        /// <summary>
        /// 悔棋
        /// </summary>
        public void Revoke()
        {
            if (DC.ins.recordStack.Count > 0)
            {
                var vo = DC.ins.recordStack.Pop();
                _role.SetTile((ushort)vo.roleTile.x, (ushort)vo.roleTile.y);
                _role.SetDir(vo.dir);
                _dynamicUnitList[vo.boxIdx].SetTile((ushort)vo.boxTile.x, (ushort)vo.boxTile.y);
            }
        }

        /// <summary>
        /// 分享
        /// </summary>
        public void Share()
        {
            Main.current.ShowShare();
        }
    }
}