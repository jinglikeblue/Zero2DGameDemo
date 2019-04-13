using System.Collections.Generic;

namespace Jing.Data
{
    public class ArrayPage<T>
    {

        public int pageSize;
        T[] _list;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list">数据列表</param>
        /// <param name="pageSize">每页数据量，至少为1</param>
        public ArrayPage(T[] list, int pageSize)
        {
            if (pageSize < 1)
            {
                pageSize = 1;
            }
            this.pageSize = pageSize;
            this._list = list;
        }

        /// <summary>
        /// 获取指定页数据
        /// </summary>
        /// <param name="page">从0开始</param>
        /// <returns></returns>
        public IList<T> getPage(int page)
        {
            IList<T> list = new List<T>();
            int i = page * pageSize;
            int endIdx = i + pageSize;
            while (i < endIdx && i < _list.Length)
            {
                list.Add(_list[i]);
                i++;
            }
            return list;
        }

        /// <summary>
        /// 获取总页数
        /// </summary>
        /// <returns></returns>
        public int getTotalPage()
        {
            int pageAmount = _list.Length / pageSize;
            if (_list.Length % pageSize > 0)
            {
                pageAmount++;
            }
            return pageAmount;
        }
    }
}
