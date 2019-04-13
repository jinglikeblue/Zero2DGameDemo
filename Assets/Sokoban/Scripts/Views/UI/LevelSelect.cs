using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sokoban
{
    public class LevelSelect : MonoBehaviour
    {

        public Transform list;
        public GameObject itemPrefab;
        List<LevelItem> _itemList;
        public WindowTween tween;

        private void Awake()
        {
            Refresh();
        }

        void Refresh()
        {
            //清除列表
            for (int i = 1; i < list.childCount; i++)
            {
                Destroy(list.GetChild(i).gameObject);
            }

            int showLevel = DC.ins.save.CompletedLevelCount + 1;
            if(DC.ins.isAllLevel || showLevel > Define.LEVEL_AMOUNT)
            {
                showLevel = Define.LEVEL_AMOUNT;
            }

            _itemList = new List<LevelItem>();
            for (int level = 1; level <= showLevel; level++)
            {                
                var item = Instantiate(itemPrefab);
                item.name = string.Format("Level{0}", level);
                item.SetActive(true);
                item.transform.SetParent(list);
                item.transform.localScale = Vector3.one;
                LevelItem li = item.GetComponent<LevelItem>();
                li.SetData(level);
                _itemList.Add(li);
            }
        }


        void Start()
        {

        }

        private void OnEnable()
        {         
            if (2 == DC.ins.controlMode)
            {
                //EventSystem.current.SetSelectedGameObject(item);
            }            
        }

        private void OnDisable()
        {
            
        }

        public void Show()
        {
            gameObject.SetActive(true);
            tween.TweenIn();
        }

        public void Hide()
        {
            tween.TweenOut(() =>
            {
                gameObject.SetActive(false);
            });
        }

        void OnWatchedAD()
        {
            DC.ins.save.UnlockLevel += 1;
            foreach (var item in _itemList)
            {
                item.SetData(item.Level);
            }
            Main.current.msgBox.Show(DC.ins.i18n.T("新关卡已解锁!"));
        }
    }
}
