using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sokoban
{
    public class StartupMenu : MonoBehaviour
    {
        public Button btnStart;

        private void Update()
        {
            if(DC.ins.controlMode == 2)
            {
                if (null == EventSystem.current.currentSelectedGameObject)
                {
                    btnStart.Select();
                }
            }
        }

        public void StartGame()
        {
            int level = DC.ins.save.CompletedLevelCount + 1;
            if (level < 1)
            {
                level = 1;
            }
            Main.current.EnterLevel(level); 
        }
    }
}
