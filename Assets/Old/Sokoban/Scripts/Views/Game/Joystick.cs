using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameKit
{
    public class Joystick : MonoBehaviour
    {

        [Header("摇杆最大半径(UGUI)")]
        public float maxRadius = 0;
        [Header("摇杆最小半径(UGUI)")]
        public float minRadius = 0;
        [Header("摇杆框")]
        public Transform stickBorder;
        [Header("摇杆")]
        public Transform stick;

        /// <summary>
        /// 绑定的相机
        /// </summary>
        [HideInInspector]
        public Camera camera;

        /// <summary>
        /// 触摸的起始位置
        /// </summary>
        Vector2 _touchStartPos;

        /// <summary>
        /// 摇杆起始位置
        /// </summary>
        Vector3 _stickBorderInitPos;

        /// <summary>
        /// 当Stick的值改变时触发
        /// </summary>
        public event Action<Vector2> onValueChange;

        List<KeyCode> _pressedKeyCode = new List<KeyCode>();

        Vector2 _lastValue;

        bool _isStickMode = false;

        private void Awake()
        {
            camera = Camera.main;
        }

        void Start()
        {
            _stickBorderInitPos = stickBorder.position;
        }

        private void OnGUI()
        {
        }

        private void FixedUpdate()
        {
            if (_isStickMode == false)
            {
                CheckKeyPress(KeyCode.UpArrow);
                CheckKeyPress(KeyCode.DownArrow);
                CheckKeyPress(KeyCode.LeftArrow);
                CheckKeyPress(KeyCode.RightArrow);

                CheckKeyRelease(KeyCode.UpArrow);
                CheckKeyRelease(KeyCode.DownArrow);
                CheckKeyRelease(KeyCode.LeftArrow);
                CheckKeyRelease(KeyCode.RightArrow);

                Vector2 tempValue = Vector2.zero;
                if (_pressedKeyCode.Count > 0)
                {
                    switch (_pressedKeyCode[0])
                    {
                        case KeyCode.UpArrow:
                            tempValue = Vector2.up;
                            break;
                        case KeyCode.DownArrow:
                            tempValue = Vector2.down;
                            break;
                        case KeyCode.LeftArrow:
                            tempValue = Vector2.left;
                            break;
                        case KeyCode.RightArrow:
                            tempValue = Vector2.right;
                            break;
                    }
                }

                SetValue(tempValue);
            }
        }

        void SetValue(Vector2 value)
        {
            if (_lastValue != value)
            {
                _lastValue = value;
                onValueChange?.Invoke(_lastValue);
            }
        }

        void CheckKeyPress(KeyCode keyCode)
        {
            if (Input.GetKeyDown(keyCode))
            {
                _pressedKeyCode.Remove(keyCode);
                _pressedKeyCode.Insert(0, keyCode);
            }
        }

        void CheckKeyRelease(KeyCode keyCode)
        {
            if (Input.GetKeyUp(keyCode))
            {
                _pressedKeyCode.Remove(keyCode);
            }
        }

        /// <summary>
        /// 得到指定GameObject下，鼠标相对的localposition坐标
        /// </summary>
        /// <param name="go"></param>
        /// <returns></returns>
        Vector2 GetLocalMousePosition(GameObject go)
        {
            Vector2 screenMouse = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(go.GetComponent<RectTransform>(), screenMouse, camera, out localPoint);

            //Debug.LogFormat("Mouse:{0}  Screen:{1}  LocalPoint:{2}", Input.mousePosition, screenMouse, localPoint);
            return localPoint;
        }

        /// <summary>
        /// 触摸开始的时候
        /// </summary>
        /// <param name="e"></param>
        public void OnPointerDown(BaseEventData e)
        {
            stickBorder.localPosition = GetLocalMousePosition(gameObject);

            stickBorder.GetComponent<CanvasGroup>().alpha = 0.4f;
        }

        /// <summary>
        /// 滑动开始的时候
        /// </summary>
        /// <param name="e"></param>
        public void OnBeginDrag(BaseEventData e)
        {
            _isStickMode = true;

            _touchStartPos = GetLocalMousePosition(stickBorder.gameObject);
        }

        /// <summary>
        /// 滑动中
        /// </summary>
        /// <param name="e"></param>
        public void OnDrag(BaseEventData e)
        {
            Vector2 touchNowPos = GetLocalMousePosition(stickBorder.gameObject);

            var moveVector = (touchNowPos - _touchStartPos);
            //Debug.LogFormat("start:{0}   mouse:{1}    moved:{2}", _touchStartPos, Input.mousePosition, moveVector);
            if (moveVector.magnitude > maxRadius)
            {
                var k = moveVector.magnitude / maxRadius;
                moveVector /= k;
            }
            stick.localPosition = moveVector;

            Vector2 value = Vector2.zero;
            if (moveVector.magnitude >= minRadius)
            {
                value = new Vector2(moveVector.x, moveVector.y);
            }

            SetValue(value);
        }

        /// <summary>
        /// 滑动结束的时候
        /// </summary>
        /// <param name="e"></param>
        public void OnEndDrag(BaseEventData e)
        {
            stick.localPosition = Vector3.zero;
            onValueChange?.Invoke(Vector2.zero);
            _isStickMode = false;

            ResetStickBorder();
        }

        void ResetStickBorder()
        {
            stickBorder.GetComponent<CanvasGroup>().alpha = 0.2f;
            stickBorder.position = _stickBorderInitPos;
        }
    }
}
