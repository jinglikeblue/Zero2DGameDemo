using System;
using DG.Tweening;
using Jing;
using UnityEngine;
using Zero;

namespace IL
{
    /// <summary>
    /// 箱子
    /// </summary>
    class BoxUnit : MoveableUnit
    {
        public void SetIsAtTarget(bool isAtTarget)
        {            
            var sprites = GetComponent<ObjectBindingData>().Find("BoxState");
            if (isAtTarget)
            {
                Img.sprite = sprites[1] as Sprite;
            }
            else
            {
                Img.sprite = sprites[0] as Sprite;
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            onMoveStart += OnMoveStart;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            onMoveStart -= OnMoveStart;
        }        

        private void OnMoveStart(MoveableUnit obj)
        {
            Global.Ins.effectDevice.Play(ResMgr.Ins.Load<AudioClip>("hot_res/audios/push"));
        }
    }
}
