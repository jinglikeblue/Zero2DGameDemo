using Jing.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sokoban
{
    public class LevelItem : MonoBehaviour
    {

        public BitmapFont textLevel;
        public Text textStep;
        public GameObject imgLock;

        private int _level;
        public int Level
        {
            get { return _level; }
        }

        void Start()
        {

        }


        void Update()
        {

        }

        public void SetData(int level)
        {
            _level = level;
            textLevel.text = level.ToString();
            int step = DC.ins.save.GetLevelStepCount(level);
            textStep.text = -1 == step ? "" : string.Format("{0}", step);

            imgLock.SetActive(DC.ins.save.IsLevelLock(level));            
        }

        public void OnClick()
        {
            if (DC.ins.save.IsLevelLock(_level))
            {
                Main.current.msgBox.Show(DC.ins.i18n.T("Complete watch AD to unlock new level!"),"Watch", "Later", () =>
                {
                    Main.current.WatchAudioAD(()=> {
                        UpdateData();
                        Main.current.msgBox.Show(DC.ins.i18n.T("Unlocked!"));
                    });
                });
            }
            else
            {
                Main.current.EnterLevel(_level);
            }
        }

        void UpdateData()
        {
            SetData(_level);
        }
    }
}
