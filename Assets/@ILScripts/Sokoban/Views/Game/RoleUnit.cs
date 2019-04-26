using System;
using DG.Tweening;
using UnityEngine;

namespace IL
{
    class RoleUnit : MoveableUnit
    {
        Animator _animator;
        EDir _dir = EDir.DOWN;        
        EDir _moveDir = EDir.NONE;

        protected override void OnInit()
        {
            base.OnInit();
            _animator = GetChildComponent<Animator>(0);

            SetDir(_dir);
            OnMoveEnd();
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

        private void OnMoveStart(EDir dir)
        {
            SetDir(dir);
            _animator.SetBool("Move", true);
        }

        private void OnMoveEnd()
        {
            if (_moveDir == EDir.NONE)
            {
                _animator.SetBool("Move", false); 
            }
            else
            {
                Move(_moveDir);
            }           
        }

        public void SetDir(EDir dir)
        {
            _dir = dir;
            if (_dir != EDir.NONE)
            {
                _animator.SetInteger("Dir", (int)_dir);
            }
        }

        public override void Move(EDir dir)
        {
            _moveDir = dir;

            base.Move(dir);
        }
    }
}
