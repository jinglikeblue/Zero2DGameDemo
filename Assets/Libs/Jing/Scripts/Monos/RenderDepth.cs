using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Render对象的排序
/// </summary>
namespace Jing.UI
{
    public class RenderDepth : MonoBehaviour
    {

        [Header("图层位置")]
        public int sortingOrder = 0;

        void Start()
        {

        }

        private void OnEnable()
        {
            Renderer renderer = this.GetComponent<Renderer>();
            renderer.sortingOrder = sortingOrder;
        }


        void Update()
        {

        }
    }
}
