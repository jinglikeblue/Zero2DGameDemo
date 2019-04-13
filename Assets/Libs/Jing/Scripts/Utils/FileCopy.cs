using System;
using System.IO;
using System.Threading;

namespace Jing
{
    public class FileCopy
    {
        float _progress = 0f;
        public float progress
        {
            get { return _progress; }
        }

        bool _isDone = false;
        public bool isDone
        {
            get { return _isDone; }
        }
        
        string _error = null;
        public string error
        {
            get { return _error; }
        }

        string _srcDir;
        string _destDir;

        public void Copy(string srcDir, string destDir)
        {
            _srcDir = srcDir;
            _destDir = destDir;
            _progress = 0f;
            _isDone = false;
            _error = null;

            Thread thread = new Thread(new ThreadStart(CopyThread));
            thread.Start();
        }

        void CopyThread()
        {
            try
            {
                CopyDir(_srcDir, _destDir);
            }
            catch (Exception e)
            {
                _error = e.Message;                
            }

            _isDone = true;
            _progress = 1f;
        }

        void CopyDir(string srcPath, string destPath)
        {
            DirectoryInfo dir = new DirectoryInfo(srcPath);
            FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //获取目录下（不包含子目录）的文件和子目录
            foreach (FileSystemInfo i in fileinfo)
            {
                if (i is DirectoryInfo)     //判断是否文件夹
                {
                    if (!Directory.Exists(destPath + i.Name))
                    {
                        Directory.CreateDirectory(destPath + i.Name);   //目标目录下不存在此文件夹即创建子文件夹
                    }
                    CopyDir(i.FullName, destPath + i.Name);    //递归调用复制子文件夹
                }
                else
                {
                    File.Copy(i.FullName, destPath + i.Name, true);      //不是文件夹即复制文件，true表示可以覆盖同名文件
                }
            }
        }
    }
}
