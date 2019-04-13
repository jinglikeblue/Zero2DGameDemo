using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Jing.Anim
{
    /// <summary>
    /// 动画回调
    /// </summary>
    public class AnimationCallback : MonoBehaviour
    {
        [Serializable]
        public class AnimationCallbackEvent : UnityEvent<string> { };

        public AnimationCallbackEvent callbackEvent;

        void Start()
        {

        }
     
        void Update()
        {

        }

        public void Callback(string content)
        {
            callbackEvent.Invoke(content);
        }
    }
}
