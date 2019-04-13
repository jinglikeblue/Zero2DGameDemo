using UnityEngine;
using UnityEngine.UI;

namespace Sokoban
{
    [RequireComponent(typeof(Text))]
    public class I18nText : MonoBehaviour
    {
        
        private void Awake()
        {

        }

        private void OnEnable()
        {
            if (DC.ins.i18n != null)
            {
                Text text = GetComponent<Text>();
                text.text = DC.ins.i18n.T(text.text);
            }
        }

        void Start()
        {

        }


        void Update()
        {

        }
    }
}
