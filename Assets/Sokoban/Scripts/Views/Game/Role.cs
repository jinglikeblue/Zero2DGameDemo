using UnityEngine;
using Jing.FSM;

namespace Sokoban
{
    public class Role : UnitBase
    {
        public delegate bool CheckMoveEnableHandler(Vector3 startTile, Vector3 endTile, EDir dir);

        enum EState
        {
            IDLE,
            MOVE
        }

        [Header("人物的动画")]
        [SerializeField]
        public Animator animator;

        EDir _dir = EDir.DOWN;
        bool _isMove = false;        

        [Header("移动速度")]
        [SerializeField]
        float _moveSpeed = 2f;

        Vector3 _targetPos;
        Vector3 _toTilePos;

        FiniteStateMachine<EState> _fsm = new FiniteStateMachine<EState>("role");

        [Header("移动音效")]
        [SerializeField]
        AudioSource _bootAudio;

        public CheckMoveEnableHandler checkMoveEnableHandler;

        protected override void Awake()
        {
            base.Awake();
            _fsm.RegistState(EState.IDLE, OnEnterState);
            _fsm.RegistState(EState.MOVE, OnEnterState, null, OnUpdateMoveState);
            _fsm.SwitchState(EState.IDLE);            
        }

        void OnEnterState(EState state)
        {
            switch (_fsm.curState)
            {
                case EState.IDLE:
                    _isMove = false;
                    _bootAudio.Stop();
                    break;
                case EState.MOVE:
                    _isMove = true;
                    _bootAudio.Play();
                    break;
            }

            if (_dir != EDir.NONE)
            {
                animator.SetInteger("Dir", (int)_dir);
            }
            animator.SetBool("Move", _isMove);
        }

        public void SetDir(EDir dir)
        {
            _dir = dir;
            if (_dir != EDir.NONE)
            {
                animator.SetInteger("Dir", (int)_dir);
            }
        }

        void OnUpdateMoveState(EState state, float deltaTime)
        {
            this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, _targetPos, deltaTime * _moveSpeed);
            if (_targetPos == this.transform.localPosition)
            {
                _tile = _toTilePos;
                CheckMove();
            }            
        }

        void Start()
        {
            
        }

        private void FixedUpdate()
        {
            _fsm.Update(Time.fixedDeltaTime);
        }        

        void Update()
        {

        }

        public void Move(EDir dir)
        {
            _dir = dir;
            if (_fsm.curState == EState.MOVE)
            {
                return;
            }

            CheckMove();
        }     
        
        void CheckMove()
        {            
            if(_dir == EDir.NONE)
            {
                _fsm.SwitchState(EState.IDLE);
                return;
            }

            Vector3? toTilePos = null;
            switch (_dir)
            {
                case EDir.UP:
                    toTilePos = _tile + Vector3.up;
                    break;
                case EDir.DOWN:
                    toTilePos = _tile + Vector3.down;
                    break;
                case EDir.LEFT:
                    toTilePos = _tile + Vector3.left;
                    break;
                case EDir.RIGHT:
                    toTilePos = _tile + Vector3.right;
                    break;
            }     
            
            if (checkMoveEnableHandler(_tile, (Vector3)toTilePos, _dir))
            {
                _toTilePos = (Vector3)toTilePos;
                _targetPos = _toTilePos * Define.TILE_SIZE;
                _fsm.SwitchState(EState.MOVE);
            }
            else
            {
                _fsm.SwitchState(EState.IDLE);
            }
        }

    }
}