using IL.Zero;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zero;

namespace IL
{
    public class LevelSelectWin : AWinView
    {
        Transform _list;
        GameObject _itemPrefab;        

        Button _btnClose;

        protected override void OnInit()
        {
            _btnClose = GetChildComponent<Button>("Panel/BtnClose");
            _list = GetChild("Panel/LevelList/Viewport/Content");
            _itemPrefab = _list.Find("LevelItem").gameObject;
            _itemPrefab.SetActive(false);
            RefreshList();
        }

        protected override void OnEnable()
        {
            DefaultShowEffect();
            _btnClose.onClick.AddListener(Close);
        }

        private void Close()
        {
            DefaultCloseEffect();
        }

        void RefreshList()
        {
            //清除列表
            for (int i = 1; i < _list.childCount; i++)
            {
                GameObject.Destroy(_list.GetChild(i).gameObject);
            }

            for(int i = 0; i < Define.LEVEL_AMOUNT; i++)
            {
                ViewFactory.Create<ItemView>(_itemPrefab, _list, i + 1).SetActive(true);
            }
        }

        class ItemView : AView
        {
            int _level = -1;
            protected override void OnData(object data)
            {
                _level = (int)data;
                GetChildComponent<BitmapText>("Image/TextLevel").Text = _level.ToString();
            }

            protected override void OnEnable()
            {
                GetChildComponent<Button>("Image").onClick.AddListener(SelectLevel);
            }

            private void SelectLevel()
            {
                Global.Ins.menu.EnterLevel(_level);
            }
        }
    }
}
