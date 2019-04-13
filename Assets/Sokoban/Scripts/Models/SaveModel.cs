using System.IO;
using UnityEngine;

namespace Sokoban
{
    /// <summary>
    /// 存档模型
    /// </summary>
    public class SaveModel
    {        
        GameSaveVO _vo;

        string _file;

        public SaveModel()
        {
            _file = GetSaveFile();
            if (File.Exists(_file))
            {
                string str = File.ReadAllText(_file);
                _vo = JsonUtility.FromJson<GameSaveVO>(str);
            }
            else
            {
                _vo = new GameSaveVO();
                Save();
            }
        }

        /// <summary>
        /// 最后玩的关卡
        /// </summary>
        public int LastLevel
        {
            get { return _vo.lastLevel; }
            set
            {
                _vo.lastLevel = value;
                Save();
            }
        }

        /// <summary>
        /// 已解锁的关卡
        /// </summary>
        public int UnlockLevel
        {
            get { return _vo.unlockLevel; }
            set
            {
                _vo.unlockLevel = value;
                Save();
            }
        }

        /// <summary>
        /// 是否关卡解锁了(广告解锁) 
        /// </summary>
        /// <returns></returns>
        public bool IsLevelLock(int level)
        {
            //--暂时注释掉，因为视频广告不稳定

            //if(false == DC.ins.isThereAD)
            //{
            //    return false;
            //}

            //if(level > UnlockLevel)
            //{
            //    return true;
            //}
            return false;
        }

        /// <summary>
        /// 已完成的关卡数量
        /// </summary>
        public int CompletedLevelCount
        {
            get { return _vo.completeList.Count; }
        }

        /// <summary>
        /// 是否已通过了关卡
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public bool IsLevelComplete(int level)
        {
            if(-1 == GetLevelStepCount(level))
            {
                return false;
            }
            return true;
        }

        public void UpdateLevelStepCount(int level, int stepCount)
        {
            CompleteLevelInfoVO vo = null;
            foreach(var tempVO in _vo.completeList)
            {
                if(tempVO.level == level)
                {
                    vo = tempVO;
                    break;
                }
            }

            if(null == vo)
            {
                vo = new CompleteLevelInfoVO(level, stepCount);
                _vo.completeList.Add(vo);
            }
            else
            {
                vo.stepCount = stepCount;
            }

            Save();
        }

        public int GetLevelStepCount(int level)
        {
            CompleteLevelInfoVO vo = null;
            foreach (var tempVO in _vo.completeList)
            {
                if (tempVO.level == level)
                {
                    vo = tempVO;
                    break;
                }
            }

            if (null != vo)
            {
                return vo.stepCount;
            }

            return -1;
        }

        void Save()
        {
            string str = JsonUtility.ToJson(_vo);
            File.WriteAllText(_file, str);
        }

        string GetSaveFile()
        {
            string file = Application.persistentDataPath + "/save.txt";
            if(Application.platform == RuntimePlatform.WindowsEditor)
            {
                file = Application.dataPath + "/save.txt";
            }
            return file;
        }
    }
}
