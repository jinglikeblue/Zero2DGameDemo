using IL.Zero;
using UnityEngine;

namespace IL.Sokoban
{
    public class ILMain
    {
        public static void Main()
        {
            Application.targetFrameRate = 60;
            Global.Ins.panelLayer = new SingularViewLayer(GameObject.Find("UIPanel"));
            Global.Ins.menu.ShowMenu();
        }
    }
}