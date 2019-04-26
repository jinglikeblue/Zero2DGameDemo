using System;
using IL.Zero;
using Sokoban;
using UnityEngine.UI;

namespace IL
{
    public class MenuPanel : AView
    {        
        Button _btnStart;

        Button _btnSelectLevel;

        Button _btnCredits;

        protected override void OnInit()
        {
            _btnStart = GetChildComponent<Button>("StartupMenu/BtnStart");
            _btnSelectLevel = GetChildComponent<Button>("StartupMenu/BtnSelectLevel");
            _btnCredits = GetChildComponent<Button>("StartupMenu/BtnCredits");
        }

        protected override void OnEnable()
        {
            _btnStart.onClick.AddListener(Start);
            _btnSelectLevel.onClick.AddListener(SelectLevel);
            _btnCredits.onClick.AddListener(ShowCredits);
        }

        protected override void OnDisable()
        {
            _btnStart.onClick.RemoveListener(Start);
            _btnSelectLevel.onClick.RemoveListener(SelectLevel);
            _btnCredits.onClick.RemoveListener(ShowCredits);
        }

        private void Start()
        {
            UIPanelMgr.Ins.Switch<GamePanel>();            
        }

        private void SelectLevel()
        {
            UIWinMgr.Ins.Open<LevelSelectWin>();
        }

        private void ShowCredits()
        {
            UIWinMgr.Ins.Open<CreditsWin>();
        }
    }
}