using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zero;

namespace IL
{
    class ScreenChecker
    {
        int lastSW = 0;
        int lastSH = 0;
        public ScreenChecker()
        {
            lastSW = Screen.width;
            lastSH = Screen.height;
            ILBridge.Ins.StartCoroutine(ScreenCheckLoop());
        }

        IEnumerator ScreenCheckLoop()
        {
            do
            {
                if(lastSW != Screen.width || lastSH != Screen.height)
                {
                    lastSW = Screen.width;
                    lastSH = Screen.height;
                    Log.I("屏幕分辨率改变：{0} x {1}", lastSW, lastSH);
                    GameEvent.Ins.onScreenSizeChange?.Invoke();
                }
                yield return new WaitForSeconds(0.1f);
            } while (true);
        }        
    }
}
