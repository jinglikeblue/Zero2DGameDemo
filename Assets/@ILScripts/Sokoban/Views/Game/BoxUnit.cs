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
            
            Object[] sprites = GetChildComponent<ObjectBindingData>(0).Find("BoxState");
            if (isAtTarget)
            {
                Img.sprite = sprites[1] as Sprite;
            }
            else
            {
                Img.sprite = sprites[0] as Sprite;
            }
        }


    }
}
