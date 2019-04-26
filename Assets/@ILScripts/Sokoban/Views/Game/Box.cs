using Jing;

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

        float _moveSpeed = 2f;

        FiniteStateMachine<EState> _fsm = new FiniteStateMachine<EState>();
    }
}
