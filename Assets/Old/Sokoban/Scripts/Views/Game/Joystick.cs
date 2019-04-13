using Sokoban;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sokoban
{
    public class Joystick : MonoBehaviour {

        /// <summary>
        /// Stick值改变的委托
        /// </summary>
        /// <param name="value"></param>
        public delegate void OnStickValueChangeHandler(Vector2 value);

        [Header("摇杆最大半径")]
        public float maxRadius = 0;
        [Header("摇杆最小半径")]
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


        Vector3 _stickPos;

        /// <summary>
        /// 触摸的起始位置
        /// </summary>
        Vector3 _touchStartPos;

        /// <summary>
        /// 摇杆起始位置
        /// </summary>
        Vector3 _stickBorderInitPos;

        /// <summary>
        /// 当Stick的值改变时触发
        /// </summary>
        public OnStickValueChangeHandler onStickValueChange;

        List<KeyCode> _pressedKeyCode = new List<KeyCode>();

        Vector2 _lastValue;

        bool _isStickMode = false;

        void Start() {
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

                Vector2? tempValue = null;
                if (_pressedKeyCode.Count == 0)
                {
                    tempValue = Vector2.zero;

                }
                else
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


                if (_lastValue != tempValue)
                {
                    _lastValue = (Vector2)tempValue;
                    onStickValueChange(_lastValue);
                }
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
        
        
        public void OnPointerDown(BaseEventData e)
        {
            Main.current.console.Output("PointerDown");                                   
            stickBorder.position = Input.mousePosition;           

            stickBorder.GetComponent<CanvasGroup>().alpha = 0.4f;
        }

        public void OnBeginDrag(BaseEventData e)
        {
            Main.current.console.Output("BeginDrag");

            _stickPos = stick.position;
            _stickPos.z = 0;

            _isStickMode = true;
            _touchStartPos = camera.ScreenToWorldPoint(Input.mousePosition);
            _touchStartPos.z = 0;
        }

        public void OnDrag(BaseEventData e)
        {
            Main.current.console.Output("Drag");
            Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            var moveVector = (mousePos - _touchStartPos);
            //Debug.LogFormat("start:{0}   mouse:{1}    moved:{2}", startPos, Input.mousePosition, moveVector);
            if (moveVector.magnitude > maxRadius)
            {
                var k = moveVector.magnitude / maxRadius;
                moveVector /= k;
            }
            stick.position = _stickPos + moveVector * 100;

            Vector2? value;
            if (moveVector.magnitude < minRadius)
            {
                value = Vector2.zero;
            }
            else
            {
                value = new Vector2(moveVector.x, moveVector.y);
            }

            if (_lastValue != value)
            {
                _lastValue = (Vector2)value;
                onStickValueChange(_lastValue);
            }
        }

        public void OnEndDrag(BaseEventData e)
        {
            Main.current.console.Output("EndDrag");
            stick.localPosition = Vector3.zero;
            onStickValueChange(Vector2.zero);
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
