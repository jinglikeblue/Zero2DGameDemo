using IL.Zero;
using UnityEngine;
using Zero;

namespace IL
{
    class GameStage : AView
    {
        LevelModel _lv;
        Transform _contents;
        protected override void OnInit()
        {
            _contents = GetChild("Contents");

            float off = Define.MAP_TILE_COUNT_OF_SIDE * Define.TILE_SIZE / -2f;
            _contents.localPosition = new Vector2(off, off);

            _lv = Global.Ins.lv;
            CreateBoxes();
        }

        private void CreateBoxes()
        {
            var prefab = ResMgr.Ins.Load<GameObject>("prefabs/game/Box");
            foreach (var vo in _lv.boxes)
            {
                var box = ViewFactory.Create<Box>(prefab, _contents);
                box.SetTile(vo.x, vo.y);
            }
        }
    }
}
