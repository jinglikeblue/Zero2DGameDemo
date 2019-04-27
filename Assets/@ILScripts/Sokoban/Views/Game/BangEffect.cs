using System;
using IL.Zero;
using UnityEngine;
using Zero;

namespace IL
{
    class BangEffect:AView
    {
        AnimationCallback _acb;

        protected override void OnInit()
        {
            base.OnInit();
            _acb = GetComponent<AnimationCallback>();            
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _acb.onCallback += OnCallBack;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _acb.onCallback -= OnCallBack;
        }

        private void OnCallBack(string obj)
        {
            Destroy();
        }
    }
}
