using Sokoban;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    [Header("单位的类型")]
    [SerializeField]
    EUnitType _unitType;

    /// <summary>
    /// 单位类型
    /// </summary>
    public EUnitType UnitType
    {
        get { return _unitType; }
    }

    /// <summary>
    /// 渲染图片
    /// </summary>
    SpriteRenderer _sr;

    /// <summary>
    /// 所在格子
    /// </summary>
    protected Vector3 _tile;
    public Vector3 Tile
    {
        get { return _tile; }
    }

    protected virtual void Awake()
    {
        _sr = transform.GetChild(0).GetComponent<SpriteRenderer>();

        if (_unitType == EUnitType.TARGET)
        {
            _sr.sortingLayerName = "Ground";
        }
    }

    /// <summary>
    /// 设置所在的格子
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public virtual void SetTile(ushort x, ushort y)
    {
        _tile = new Vector3(x, y);
        transform.localPosition = _tile * Define.TILE_SIZE;
    }

    /// <summary>
    /// 得到排序值
    /// </summary>
    /// <returns></returns>
    public float GetSortValue()
    {
        return this.transform.localPosition.y;
    }

    /// <summary>
    /// 设置排序值
    /// </summary>
    /// <param name="v"></param>
    public void SetSortValue(int v)
    {
        _sr.sortingOrder = v;
    }

}
