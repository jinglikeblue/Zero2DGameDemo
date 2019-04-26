using System;
using UnityEngine.UI;

namespace IL
{
    class MsgWin:AWinView
    {
        public static MsgWin Show(string content, bool isConfirm = false, Action onOK = null, Action onCancel = null)
        {
            var msgWin = UIWinMgr.Ins.Open<MsgWin>();
            msgWin.Set(content, isConfirm, onOK, onCancel);
            return msgWin;
        }

        Action _onOK;
        Action _onCancel;

        Button _btnOK;
        Button _btnCancel;
        Text _text;        

        protected override void OnInit()
        {
            _btnOK = GetChildComponent<Button>("Panel/Buttons/BtnOK");
            _btnCancel = GetChildComponent<Button>("Panel/Buttons/BtnCancel");
            _text = GetChildComponent<Text>("Panel/Text");
        }

        protected override void OnEnable()
        {
            DefaultShowEffect();

            _btnOK.onClick.AddListener(OK);
            _btnCancel.onClick.AddListener(Cancel);
        }

        public void SetLabel(string labelOK, string labelCancel = "")
        {
            _btnOK.GetComponentInChildren<Text>().text = labelOK;
            _btnCancel.GetComponentInChildren<Text>().text = labelCancel;
        }

        public void Set(string content, bool isConfirm = false, Action onOK = null, Action onCancel = null)
        {
            _btnCancel.gameObject.SetActive(isConfirm);
            _text.text = content;
            _onOK = onOK;
            _onCancel = onCancel;
        }

        private void OK()
        {
            _onOK?.Invoke();
            Close();            
        }

        private void Cancel()
        {
            _onCancel?.Invoke();
            Close();            
        }

        void Close()
        {
            _onOK = null;
            _onCancel = null;
            DefaultCloseEffect();
        }
    }
}
