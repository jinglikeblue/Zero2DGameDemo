using DG.Tweening;
using Jing;
using UnityEngine;

namespace IL
{
    /// <summary>
    /// 箱子
    /// </summary>
    class Box : BaseUnit
    {
        enum EState
        {
            IDLE,
            MOVE
        }

        //FiniteStateMachine<EState> _fsm = new FiniteStateMachine<EState>();

        public Vector2Int TileMoveTo { get; private set; }

        public bool IsMoving { get; private set; }

        protected override void OnInit()
        {

        }

        public void Move(EDir dir)
        {
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

            Vector3 targetPos = TileUtil.Tile2Position(TileMoveTo);

            IsMoving = true;
            gameObject.transform.DOLocalMove(targetPos, 0.5f).OnComplete(()=> {
                IsMoving = false;
            });                        
        }
    }
}
