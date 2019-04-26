using System;
using Jing;
using UnityEngine;
using Zero;

namespace IL
{
    class MoveableUnit : BaseUnit
    {       
        enum EState
        {
            IDLE,
            MOVE
        }

        FiniteStateMachine<EState> _fsm = new FiniteStateMachine<EState>();
        public Vector2Int TileLastAt { get; private set; }
        public Vector2Int TileMoveTo { get; private set; }        
        public bool IsMoving { get; private set; }

        /// <summary>
        /// 移动速度
        /// </summary>
        float moveSpeed = 2f;

        Vector3 _targetLocalPosition;

        public event Action onMoveEnd;
        public event Action<EDir> onMoveStart;

        protected override void OnInit()
        {
            base.OnInit();
            _fsm.RegistState(EState.IDLE, OnEnterState);
            _fsm.RegistState(EState.MOVE, OnEnterState, null, OnUpdateMoveState);
            _fsm.SwitchState(EState.IDLE);
        }

        private void OnEnterState(EState nowState, object data)
        {
            switch (_fsm.CurState)
            {
                case EState.IDLE:
                    IsMoving = false;
                    onMoveEnd?.Invoke();
                    break;
                case EState.MOVE:
                    IsMoving = true;
                    onMoveStart?.Invoke((EDir)data);
                    break;
            }
        }

        private void OnUpdateMoveState(EState nowState)
        {
            gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, _targetLocalPosition, Time.deltaTime * moveSpeed);
            if (_targetLocalPosition == gameObject.transform.localPosition)
            {
                Tile = TileMoveTo;
                //bool lastIsOnTarget = _isOnTarget;
                //OnTileChange();
                //if (!lastIsOnTarget && _isOnTarget)
                //{
                //    GameObject.Instantiate(_bangEffPrefab, this.transform);
                //}                
                _fsm.SwitchState(EState.IDLE);                
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            ILBridge.Ins.onUpdate += OnUpdate;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            ILBridge.Ins.onUpdate -= OnUpdate;
        }

        private void OnUpdate()
        {
            _fsm.Update();
        }

        public virtual void Move(EDir dir)
        {
            if (IsMoving)
            {
                return;
            }

            TileLastAt = Tile;

            switch (dir)
            {
                case EDir.UP:
                    TileMoveTo = Tile + Vector2Int.up;
                    break;
                case EDir.DOWN:
                    TileMoveTo = Tile + Vector2Int.down;
                    break;
                case EDir.LEFT:
                    TileMoveTo = Tile + Vector2Int.left;
                    break;
                case EDir.RIGHT:
                    TileMoveTo = Tile + Vector2Int.right;
                    break;
            }

            _targetLocalPosition = TileUtil.Tile2Position(TileMoveTo);
            _fsm.SwitchState(EState.MOVE, dir);
        } 
    }
}
