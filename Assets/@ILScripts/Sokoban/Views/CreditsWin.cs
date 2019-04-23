using System;
using UnityEngine.UI;

namespace IL
{
    class CreditsWin : AWinView
    {
        Button _btnClose;

        protected override void OnInit()
        {
            _btnClose = GetChildComponent<Button>("Panel/BtnClose");
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
    }
}
