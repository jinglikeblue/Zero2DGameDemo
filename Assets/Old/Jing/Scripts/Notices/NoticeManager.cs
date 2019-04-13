using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jing.Notice
{    
    public delegate void CallJing(Notice t);
    /// <summary>
    /// 消息通知器
    /// </summary>
    class NoticeManager
    {

        private static Dictionary<string, IList<CallJing>> _registeredNotice = new Dictionary<string, IList<CallJing>>();

        /// <summary>
        /// 注册一个通知的监听（类似于事件机制,通过委托实现) 
        /// </summary>
        /// <param name="type">操作类型</param>
        /// <param name="action">委托方法</param>
        public static void AddNoticeAction(string type, CallJing action)
        {
            IList<CallJing> list = null;
            if (_registeredNotice.ContainsKey(type))
            {
                list = _registeredNotice[type];
            }
            else
            {
                list = new List<CallJing>();
                _registeredNotice.Add(type, list);
            }
            if (false == list.Contains(action))
            {
                list.Add(action);
            }
        }

        /// <summary>
        /// 注销一个操作监听 
        /// </summary>
        /// <param name="type">操作类型</param>
        /// <param name="action">委托方法</param>
        public static void RemoveNoticeAction(string type, CallJing action)
        {
            if (_registeredNotice.ContainsKey(type))
            {
                IList<CallJing> list = _registeredNotice[type];

                if (list.Contains(action))
                {
                    list.Remove(action);
                }
            }
        }

        /// <summary>
        /// 发送通知
        /// </summary>
        /// <param name="notice"></param>
        public static void SendNotice(Notice notice)
        {
            if (false == NoticeManager._registeredNotice.ContainsKey(notice.type))
            {
                return;
            }

            IList<CallJing> list = _registeredNotice[notice.type];
            var count = list.Count;
            while (--count > -1)
            {
                if (count >= list.Count)
                {
                    continue;
                }

                var action = list[count];
                action(notice);
            }
        }

        /// <summary>
        /// 发送一个快速通知，发送出去的是Notice对象
        /// </summary>
        /// <param name="noticeType"></param>
        /// <param name="data"></param>
        public static void SendNoticeQuick(string noticeType, Object data = null)
        {
            NoticeManager.SendNotice(new Notice(noticeType, data));
        }
    }
}
