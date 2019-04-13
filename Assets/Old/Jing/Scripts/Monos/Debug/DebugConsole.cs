using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jing.Debug
{
    public class DebugConsole : MonoBehaviour
    {

        struct InfoVO
        {
            public int fps;

            public string output;

            public string ToString()
            {
                string str = "---Info--- \r\n";
                str += string.Format("FPS:{0}\r\n", fps);
                if (output != null)
                {
                    str += string.Format("Output:{0}\r\n", output);
                }
                return str;
            }
        }

        [Header("调试信息")]
        public Text textInfo;
        InfoVO info = new InfoVO();
        float _cd = 0f;

        public void Output(string content)
        {
            info.output = content;
        }

        void Start()
        {

        }


        void Update()
        {
            _cd += Time.deltaTime;
            info.fps++;
            if (_cd > 1f)
            {
                textInfo.text = info.ToString();
                _cd = 0f;
                info.fps = 0;
            }
        }
    }
}