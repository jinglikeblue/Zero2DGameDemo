using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jing.I18n
{
    /// <summary>
    /// 多语言处理(母语为简体中文)
    /// </summary>
    public class I18n
    {
        /// <summary>
        /// 语言的字典
        /// </summary>
        Dictionary<string, string> _dic;

        public I18n()
        {
            _dic = new Dictionary<string, string>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dic">字典内容</param>
        public void SetData(Dictionary<string, string> dic)
        {
            _dic = dic;
        }

        /// <summary>
        /// 字典的二维数据，索引0表示原始字符串 索引1表示翻译字符串
        /// </summary>
        /// <param name="dicTable"></param>
        public void SetData(string[][] dicTable)
        {
            _dic = new Dictionary<string, string>();
            for(int i = 0; i < dicTable.Length; i++)
            {
                string[] kv = dicTable[i];
                _dic[kv[0]] = kv[1];
                //Debug.LogFormat("翻译内容：{0} => {1}", kv[0], kv[1]);
            }
        }

        /// <summary>
        /// 添加一个翻译
        /// </summary>
        /// <param name="key">原始语言</param>
        /// <param name="value">翻译后的语言</param>
        public void Add(string key, string value)
        {
            _dic[key] = value;
        }

        /// <summary>
        /// 翻译为指定的国家语言
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string T(string content)
        {
            return Translate(content);
        }

        /// <summary>
        /// 翻译为指定的国家语言
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Translate(string content)
        {
            if(_dic.ContainsKey(content))
            {
                return _dic[content];
            }
            return content;
        }
        
    }
}
