using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sokoban
{
    public class Credits : MonoBehaviour
    {

        public Button btnClose;
        void Start()
        {

        }

        private void OnEnable()
        {
            if (DC.ins.controlMode == 2)
            {                
                btnClose.Select();                
            }            
        }

        private void OnDisable()
        {
            if (DC.ins.controlMode == 2)
            {
                EventSystem.current.SetSelectedGameObject(null);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
