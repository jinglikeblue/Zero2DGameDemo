using System;
using DG.Tweening;
using UnityEngine;

namespace IL
{
    class RoleUnit : MoveableUnit
    {
        Animator _animator;
        EDir _toward = EDir.DOWN;                

        protected override void OnInit()
        {
            base.OnInit();
            _animator = GetChildComponent<Animator>(0);

            SetToward(_toward);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            onMoveStart += OnMoveStart;
            onMoveEnd += OnMoveEnd;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            onMoveStart -= OnMoveStart;
            onMoveEnd -= OnMoveEnd;
        }

        private void OnMoveStart(MoveableUnit unit)
        {
            SetToward(MoveDir);
            _animator.SetBool("Move", true);
        }

        private void OnMoveEnd(MoveableUnit unit)
        {
            _animator.SetBool("Move", false);
        }

        /// <summary>
        /// 设置朝向
        /// </summary>
        /// <param name="dir"></param>
        public void SetToward(EDir dir)
        {
            _toward = dir;
            if (_toward != EDir.NONE)
            {
                _animator.SetInteger("Dir", (int)_toward);
            }
        }

        public override bool Move(EDir dir, Vector2Int targetTile)
        {            
            if(dir == EDir.NONE)
            {
                return false;
            }

            SetToward(dir);

            return base.Move(dir, targetTile);
        }
    }
}
