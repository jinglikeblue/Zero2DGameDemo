using IL.Zero;

namespace IL
{
    class GameStage : AView
    {
        LevelModel _lv;
        protected override void OnInit()
        {
            _lv = Global.Ins.lv;

        }
    }
}
