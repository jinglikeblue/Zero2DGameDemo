using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using UnityEngine;

namespace Jing.Net
{
    /// <summary>
    /// 下载器
    /// </summary>
    public class Downloader
    {
        /// <summary>
        /// 重写的WebClient类
        /// </summary>
        class DownloadWebClient : WebClient
        {
            readonly int _timeout;
            public DownloadWebClient(int timeout = 60)
            {
                _timeout = timeout * 1000;                
            }

            protected override WebRequest GetWebRequest(Uri address)
            {                
                HttpWebRequest request = base.GetWebRequest(address) as HttpWebRequest;
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                request.Timeout = _timeout;
                request.ReadWriteTimeout = _timeout;
                request.Proxy = null;
                return request;
            }

            protected override WebResponse GetWebResponse(WebRequest request)
            {
                return base.GetWebResponse(request);
            }
        }

        DownloadWebClient _client;

        bool _isDone;

        /// <summary>
        /// 是否操作完成
        /// </summary>
        public bool isDone
        {
            get
            {
                return _isDone;
            }
        }

        float _progress;

        /// <summary>
        /// 操作进度
        /// </summary>
        public float progress
        {
            get
            {
                return _progress;
            }
        }

        string _error;

        /// <summary>
        /// 错误信息
        /// </summary>
        public string error
        {
            get
            {
                return _error;
            }
        }

        long _totalSize;

        /// <summary>
        /// 文件总大小
        /// </summary>
        public long totalSize
        {
            get
            {
                return _totalSize;
            }
        }

        long _loadedSize;

        /// <summary>
        /// 已完成大小
        /// </summary>
        public long loadedSize
        {
            get
            {
                return _loadedSize;
            }
        }

        /// <summary>
        /// 是否已销毁
        /// </summary>
        public bool isDisposeed
        {
            get { return _client == null ? true : false; }
        }


        string _savePath;

        string _url;

        /// <summary>
        /// 下载的URL地址
        /// </summary>
        public string url
        {
            get { return _url; }
        }

        /// <summary>
        /// 文件的保存路径
        /// </summary>
        public string savePath
        {
            get { return _savePath; }
        }

        public Downloader(string url, string savePath, string version)
        {           
            _url = url;
            _savePath = savePath;
            string saveDir = Path.GetDirectoryName(savePath);
            if (Directory.Exists(saveDir) == false)
            {
                Directory.CreateDirectory(saveDir);
            }
            _client = new DownloadWebClient();            
            _client.DownloadProgressChanged += OnDownloadProgressChanged;
            _client.DownloadFileCompleted += OnDownloadFileCompleted;

            string flag;
            if(url.Contains("?"))
            {
                flag = "&";
            }
            else
            {
                flag = "?";
            }

            url += string.Format("{0}unity_download_ver={1}", flag, version);

            Uri uri = new Uri(url);
            _client.DownloadFileAsync(uri, savePath);
        }

        /// <summary>
        /// 销毁对象，会停止所有的下载
        /// </summary>
        public void Dispose()
        {
            if (_client != null)
            {
                _client.DownloadProgressChanged -= OnDownloadProgressChanged;
                _client.DownloadFileCompleted -= OnDownloadFileCompleted;
                _client.CancelAsync();
                _client.Dispose();
                _client = null;
                if(false ==_isDone)
                {
                    _error = "Canceled";
                    _isDone = true;
                }                
            }
        }

        /// <summary>
        /// 下载文件完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {            
            if (e.Error != null)
            {
                _error = e.Error.Message;
            }
            else if (_loadedSize < _totalSize)
            {
                _error = "Disconnected";
            }
            _isDone = true;                      
        }

        /// <summary>
        /// 下载进度改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {            
            _loadedSize = e.BytesReceived;
            _totalSize = e.TotalBytesToReceive;
            if(0 == _totalSize)
            {
                _progress = 0;
            }
            else
            {
                _progress = _loadedSize / (float)_totalSize;
            }            
        }
    }
}
