using UnityEngine;
using UnityEngine.UI;

namespace Sokoban
{   
    public class Menu : MonoBehaviour
    {
        public Text textVer;

        void Start()
        {
            textVer.text = Application.version;    
        }

        void Update()
        {

        }
    }
}
