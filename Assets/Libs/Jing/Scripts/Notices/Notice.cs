using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jing.Notice
{
    public class Notice
    {
        private string _type;
        /// <summary>
        /// 通知的类型
        /// </summary>
        public string type
        {
            get { return _type; }
        }

        private Object _data;

        /// <summary>
        /// 通知的数据
        /// </summary>
        public Object data
        {
            get { return _data; }
        }

        public Notice(string type, Object data = null)
        {
            _type = type;
            _data = data;
        }
    }
}
