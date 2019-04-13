using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jing.UI
{
    [RequireComponent(typeof(ScrollRect))]
    public class ZList : MonoBehaviour
    {
        [Header("列表对象")]
        public ScrollRect sr;
        [Header("列表内容组件")]
        public RectTransform content;
        [Header("列表项的prefab")]
        public GameObject itemPrefab;
        [Header("列表项的间距")]
        public Vector2 gap;

        /// <summary>
        /// 可视区域高度
        /// </summary>
        float _viewH;
        /// <summary>
        /// 单个ITEM高度
        /// </summary>
        Vector2 _itemSize;

        int _maxItemCount;

        int _dataAmount;

        List<GameObject> itemList = new List<GameObject>();

        Action<GameObject, int> _onRefresh;

        Vector3 _lastPos;

        private void Awake()
        {
            _viewH = sr.GetComponent<RectTransform>().sizeDelta.y;
            _itemSize = itemPrefab.GetComponent<RectTransform>().sizeDelta;

            _maxItemCount = Mathf.CeilToInt(_viewH / _itemSize.y) + 2;
        }

        void Start()
        {

        }

        private void OnEnable()
        {
            _lastPos = content.localPosition;
        }


        void Update()
        {

        }

        public void OnScroll(Vector2 pos)
        {
            bool isScrollDown = (content.localPosition - _lastPos).y > 0 ? true : false;
            _lastPos = content.localPosition;

            int outRangeCount = 0;
            bool isTopOut = false;

            //首先找出所有屏幕外的item
            for (int i = 0; i < itemList.Count; i++)
            {
                var offY = itemList[i].transform.localPosition.y + content.transform.localPosition.y;
                if (isScrollDown)
                {
                    if (offY >= _itemSize.y)
                    {
                        //Debug.Log("向上超出范围：" + itemList[i].name);
                        outRangeCount++;
                        isTopOut = true;
                    }
                }
                else
                {
                    if (offY < -1 * (_viewH))
                    {
                        //Debug.Log("向下超出范围：" + itemList[i].name);
                        outRangeCount++;
                        isTopOut = false;
                    }
                }
            }



            if (0 == outRangeCount)
            {
                return;
            }

            if (outRangeCount == itemList.Count)
            {
                int startIdx = (int)(content.localPosition.y / _itemSize.y);
                //全部都超出了范围
                for (int i = 0; i < itemList.Count; i++)
                {
                    var item = itemList[i];
                    item.transform.localPosition = new Vector3(0, -1 * _itemSize.y * (startIdx + i), 0);
                    RefreshItemData(item);
                }
            }
            else
            {
                if (isTopOut)
                {
                    //部分超出了范围，优化算法，只把超出的用来填补未超出的部分
                    var outItems = itemList.GetRange(0, outRangeCount);
                    itemList.RemoveRange(0, outRangeCount);
                    var bottomPos = itemList[itemList.Count - 1].transform.localPosition;
                    for (int i = 0; i < outItems.Count; i++)
                    {
                        var item = outItems[i];
                        item.transform.localPosition = bottomPos + new Vector3(0, -1 * _itemSize.y * (i + 1), 0);
                        RefreshItemData(item);
                        itemList.Add(item);
                    }
                }
                else
                {
                    var outItems = itemList.GetRange(itemList.Count - outRangeCount, outRangeCount);
                    itemList.RemoveRange(itemList.Count - outRangeCount, outRangeCount);
                    var topPos = itemList[0].transform.localPosition;
                    int idx = 0;
                    for (int i = outItems.Count - 1; i >= 0; i--)
                    {
                        idx++;
                        var item = outItems[i];
                        item.transform.localPosition = topPos + new Vector3(0, _itemSize.y * idx, 0);
                        RefreshItemData(item);
                        itemList.Insert(0, item);
                    }
                }
            }
        }


        /// <summary>
        /// 根据位置算出Item显示的索引
        /// </summary>
        /// <param name="go"></param>
        void RefreshItemData(GameObject item)
        {
            float posY = item.transform.localPosition.y;
            int idx = (int)(posY / _itemSize.y * -1);
            //item.transform.localPosition = new Vector3(0, _itemSize.y * idx * -1, 0);
            if (idx >= 0 && idx < _dataAmount)
            {
                item.SetActive(true);
                _onRefresh(item, idx);
            }
            else
            {
                item.SetActive(false);
            }
            //item.GetComponent<ZListItem>().SetIdx(idx);
        }

        public void SetDataList(int dataAmount, Action<GameObject, int> onRefresh)
        {
            _dataAmount = dataAmount;
            _onRefresh = onRefresh;

            //列表总高度
            float contentH = _itemSize.y * _dataAmount;
            content.sizeDelta = new Vector2(_itemSize.x, contentH);

            InitList();
        }

        void InitList()
        {
            itemList.Clear();
            int createCount = 0;
            if (_dataAmount <= _maxItemCount - 2)
            {
                sr.vertical = false;
                createCount = _dataAmount;
            }
            else
            {
                sr.vertical = true;
                createCount = _maxItemCount;
            }

            for (int i = 0; i < createCount; i++)
            {
                var go = CreateItem();
                go.transform.localPosition = new Vector3(0, -1 * _itemSize.y * i, 0);
                RefreshItemData(go);
                itemList.Add(go);
            }
        }

        private GameObject CreateItem()
        {
            var go = GameObject.Instantiate(itemPrefab);
            go.transform.SetParent(content);
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero;
            return go;
        }
    }
}