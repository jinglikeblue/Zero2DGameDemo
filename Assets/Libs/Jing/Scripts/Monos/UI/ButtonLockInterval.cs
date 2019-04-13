using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jing.UI
{
    /// <summary>
    /// 按钮锁定间隔
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class ButtonLockInterval : MonoBehaviour
    {

        /// <summary>
        /// 每次点击的锁定时间
        /// </summary>
        public float LockCD;

        Button _btn;
        float _cd;

        private void Awake()
        {
            _btn = this.gameObject.GetComponent<Button>();
        }

        void Start()
        {

        }

        private void OnEnable()
        {
            _btn.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _btn.onClick.RemoveListener(OnClick);
        }


        void Update()
        {
            if (_cd > 0)
            {
                _cd -= Time.deltaTime;
                if (_cd < 0)
                {
                    _btn.interactable = true;
                }
            }
        }

        void OnClick()
        {
            _cd = LockCD;
            _btn.interactable = false;
        }
    }
}
