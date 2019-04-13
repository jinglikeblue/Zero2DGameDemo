using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Sokoban
{
    public class DestroyOnPlayOver : MonoBehaviour
    {

        void Start()
        {

        }


        void Update()
        {

        }

        public void OnPlayOver()
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
