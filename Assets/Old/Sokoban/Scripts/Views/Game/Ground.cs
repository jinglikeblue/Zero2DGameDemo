using UnityEngine;

namespace Sokoban
{
    public class Ground : MonoBehaviour
    {
        void Start()
        {
            var sr = GetComponent<SpriteRenderer>();
            sr.size = DC.ins.cameraSize;
        }               
    }
}
