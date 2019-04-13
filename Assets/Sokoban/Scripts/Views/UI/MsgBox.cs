using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 消息窗口
/// </summary>
namespace Sokoban
{
    public class MsgBox : MonoBehaviour {

        Action _onYes;
        Action _onNo;

        public Button btnCancel;

        public Text text;

        public Text textLabelYes;
        public Text textLabelNo;

        bool _isYesClose = false;

        private void Awake()
        {
        }

        void Start() {
            
        }

        void Update() {

        }

        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="content"></param>
        /// <param name="onYes"></param>
        public void Show(string content, Action onYes = null, Action onNo = null)
        {
            Show(content, "YES", "NO", onYes, onNo);
        }

        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="content"></param>
        /// <param name="onYes"></param>
        public void Show(string content, string labelYes, string labelNo, Action onYes = null, Action onNo = null)
        {
            _onYes = onYes;
            _onNo = onNo;
            textLabelYes.text = labelYes;
            textLabelNo.text = labelNo;
            text.text = content;
            if (DC.ins.controlMode == 2)
            {
                btnCancel.Select();
            }
            gameObject.SetActive(true);
            GetComponent<WindowTween>().TweenIn();
        }

        void Close()
        {
            GetComponent<WindowTween>().TweenOut(() =>
            {
                gameObject.SetActive(false);
                if(_isYesClose)
                {
                    if (_onYes != null)
                    {
                        _onYes();
                        _onYes = null;
                    }
                }
                else
                {
                    if (_onNo != null)
                    {
                        _onNo();
                        _onNo = null;
                    }
                }
            });            
        }

        public void OnClickYes()
        {
            _isYesClose = true;
            Close();
        }

        public void OnClickNo()
        {
            _isYesClose = false;
            Close();
        }
    }
}
