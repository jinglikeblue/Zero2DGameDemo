using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Jing.UI
{
    [RequireComponent(typeof(HorizontalLayoutGroup))]
    public class BitmapFont : MonoBehaviour
    {

        [Header("位图纹理")]
        public Texture[] textures;
        [Header("位图字符(顺序和纹理对应)")]
        public string charsTxt;
        [Header("位图样本")]
        public GameObject sample;

        [Header("初始化文本")]
        public string initText = "";

        bool _isDirty = false;
        List<GameObject> charPool = new List<GameObject>();
        [SerializeField]
        private bool userSelfSize = false;
        
        public string text
        {
            get { return _text; }
            set { setText(value); }
        }

        string _text;

        void Start()
        {
            //for (var i = 0; i < gameObject.transform.childCount; i++)
            //{
            //    var child = gameObject.transform.GetChild(i).gameObject;
            //    if (child != sample)
            //    {
            //        Destroy(child);
            //    }
            //}
            sample.SetActive(false);
            gameObject.GetComponent<HorizontalLayoutGroup>().childForceExpandWidth = false;
            gameObject.GetComponent<HorizontalLayoutGroup>().childForceExpandHeight = false;

            if (_text == null)
            {
                text = initText;
            }
        }


        void Update()
        {
            if (_isDirty)
            {
                _isDirty = false;
                Refresh();
            }
        }

        void Refresh()
        {
            clear();
            var chars = _text.ToCharArray();
            for (var i = 0; i < chars.Length; i++)
            {
                char c = chars[i];
                int idx = charsTxt.IndexOf(c);
                if (idx < 0 || idx >= textures.Length)
                {
                    continue;
                }

                var img = GetCharImg();

                img.name = c.ToString();
                img.SetActive(true);
                var rawImage = img.GetComponent<RawImage>();
                var layoutEle = img.GetComponent<LayoutElement>();
                var t = textures[idx];
                rawImage.texture = t;
                rawImage.SetNativeSize();
                if (layoutEle != null && t != null && !userSelfSize)
                {
                    layoutEle.preferredWidth = t.width;
                    layoutEle.preferredHeight = t.height;
                }
                else
                {
                    
                }
                img.transform.localScale = Vector3.one;
                img.transform.localPosition = Vector3.zero;
            }

            for (int i = 0; i < charPool.Count; i++)
            {
                Destroy(charPool[i]);
            }
            charPool.Clear();
        }

        /// <summary>
        /// 设置文本内容
        /// </summary>
        /// <param name="content">内容</param>
        void setText(string content)
        {
            if (_text == content)
            {
                return;
            }

            _text = content;
            _isDirty = true;
        }

        GameObject GetCharImg()
        {
            GameObject img = null;
            if (charPool.Count > 0)
            {
                img = charPool[0];
                charPool.RemoveAt(0);
                img.transform.SetAsLastSibling();
            }
            else
            {
                img = Instantiate(sample);
                img.transform.SetParent(gameObject.transform);
            }
            return img;
        }

        void clear()
        {
            for (var i = 1; i < gameObject.transform.childCount; i++)
            {
                charPool.Add(gameObject.transform.GetChild(i).gameObject);
            }
        }
    }
}
