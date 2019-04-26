using IL.Zero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace IL
{
    class BaseUnit : AView
    {
        /// <summary>
        /// 单位类型
        /// </summary>
        public EUnitType UnitType { get; private set; }

        /// <summary>
        /// 所在格子
        /// </summary>
        public Vector2Int Tile { get; protected set; }

        /// <summary>
        /// 渲染图片
        /// </summary>
        SpriteRenderer _sr;

        protected override void OnInit()
        {
            _sr = GetChildComponent<SpriteRenderer>(0);
        }

        protected override void OnData(object data)
        {
            UnitType = (EUnitType)data;
        }

        /// <summary>
        /// 设置所在的格子
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public virtual void SetTile(ushort x, ushort y)
        {
            Tile = new Vector2Int(x, y);
            gameObject.transform.localPosition = new Vector3(Tile.x * Define.TILE_SIZE, Tile.y * Define.TILE_SIZE);
        }

        /// <summary>
        /// 得到排序值
        /// </summary>
        /// <returns></returns>
        public float SortValue
        {
            get
            {
                return gameObject.transform.localPosition.y;
            }
        }

        /// <summary>
        /// 设置排序值
        /// </summary>
        /// <param name="v"></param>
        public void SetSortValue(int v)
        {
            if (null == _sr)
            {
                Debug.Log(Name);
            }
            _sr.sortingOrder = v;            
        }
    }
}
