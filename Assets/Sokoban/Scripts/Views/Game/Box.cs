using Jing.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokoban
{
    public class Box : UnitBase
    {
        public delegate bool OnBoxOnTileHandler(Box box, Vector3 tile);


        enum EState
        {
            IDLE,
            MOVE
        }

        [Header("Bang")]
        [SerializeField]
        GameObject _bangEffPrefab;

        [Header("移动速度")]
        [SerializeField]
        float _moveSpeed = 2f;

        [Header("移动音效")]
        [SerializeField]
        AudioSource _moveAudio;

        bool _isMove = false;

        Vector3 _targetPos;
        Vector3 _toTilePos;

        

        /// <summary>
        /// 要移动到的格子
        /// </summary>
        public Vector3 Tile2
        {
            get { return _toTilePos; }
        }

        FiniteStateMachine<EState> _fsm = new FiniteStateMachine<EState>();

        public OnBoxOnTileHandler onBoxOnTileHandler;

        [SerializeField]
        Sprite[] _imgSprites;
        [SerializeField]
        SpriteRenderer _img;

        /// <summary>
        /// 是否正在移动
        /// </summary>
        public bool IsMoveing
        {
            get { return _isMove; }
        }

        bool _isOnTarget = false;

        protected override void Awake()
        {
            base.Awake();
            _fsm.RegistState(EState.IDLE, OnEnterState);
            _fsm.RegistState(EState.MOVE, OnEnterState, null, OnUpdateMoveState);
            _fsm.SwitchState(EState.IDLE);
        }

        void Start()
        {
            
        }

        public override void SetTile(ushort x, ushort y)
        {
            base.SetTile(x, y);
            OnTileChange();
        }

        void OnEnterState(EState state)
        {
            switch (_fsm.curState)
            {
                case EState.IDLE:
                    _isMove = false;
                    //_moveAudio.Stop();
                    break;
                case EState.MOVE:
                    _isMove = true;
                    _moveAudio.Play();
                    break;
            }
        }

        void OnUpdateMoveState(EState state, float deltaTime)
        {
            this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, _targetPos, deltaTime * _moveSpeed);
            if (_targetPos == this.transform.localPosition)
            {
                _tile = _toTilePos;
                bool lastIsOnTarget = _isOnTarget;
                OnTileChange();
                if(!lastIsOnTarget && _isOnTarget)
                {
                    GameObject.Instantiate(_bangEffPrefab, this.transform);
                }
                _fsm.SwitchState(EState.IDLE);
            }
        }

        void OnTileChange()
        {
            if (onBoxOnTileHandler != null)
            {
                _isOnTarget = onBoxOnTileHandler(this, _tile);
                if (_isOnTarget)
                {                    
                    _img.sprite = _imgSprites[1];                    
                }
                else
                {
                    _img.sprite = _imgSprites[0];                    
                }
            }
        }


        void Update()
        {
            
        }

        private void FixedUpdate()
        {
            _fsm.Update(Time.fixedDeltaTime);
        }

        public void Move(EDir dir)
        {
            switch (dir)
            {
                case EDir.UP:
                    _toTilePos = _tile + Vector3.up;
                    break;
                case EDir.DOWN:
                    _toTilePos = _tile + Vector3.down;
                    break;
                case EDir.LEFT:
                    _toTilePos = _tile + Vector3.left;
                    break;
                case EDir.RIGHT:
                    _toTilePos = _tile + Vector3.right;
                    break;
            }

            _targetPos = _toTilePos * Define.TILE_SIZE;
            _fsm.SwitchState(EState.MOVE);
        }
    }
}
