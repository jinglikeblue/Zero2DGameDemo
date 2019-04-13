using Jing.Data;
using Jing.Debug;
using Jing.I18n;
using System;
using UnityEngine;

namespace Sokoban
{
    public class Main : MonoBehaviour
    {
        public enum EAppPlatform
        {
            Default,
            GooglePlay,
            TapTap,
            XiaoMi,
        }

        /// <summary>
        /// 当前主程序
        /// </summary>
        static public Main current = null;

        [Header("上架的平台")]
        public EAppPlatform appPlatform;

        [Header("调试控制台")]
        public DebugConsole console;

        [Header("摄像机")]
        public Camera mainCamera;

        [Header("舞台层")]
        public Transform stageLayer;

        [Header("场景加载")]
        public Loading loading;

        [Header("游戏场景的prefab")]
        public GameObject gamePrefab;

        [Header("游戏主菜单的prefab")]
        public GameObject menuPrefab;

        [Header("消息窗口")]
        public MsgBox msgBox;

        [Header("分享窗口")]
        public ShareWin shareWin;

        [Header("是否全关卡开放")]
        [SerializeField]
        bool isAllLevel = true;

        [Header("是否使用本地化")]
        [SerializeField]
        bool isI18n = true;

        Action _onWatchCompleted;
        Action _onWatchInterrupted;

        float _lastFocusADTime = 0;

        private void Awake()
        {

        }

        private void Start()
        {
            current = this;
            Application.targetFrameRate = 30;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            DC.ins.isAllLevel = isAllLevel;
            DC.ins.mainCamera = mainCamera;
            DC.ins.save = new SaveModel();
            FitScreen();
            InitI18N();
            InitTalkingData();

            ShowStage(menuPrefab, false);
        }

        /// <summary>
        /// 初始化多语言
        /// </summary>
        void InitI18N()
        {            
            var i18n = new I18n();
            DC.ins.i18n = i18n;
            if(!isI18n)
            {
                return;
            }
            //Debug.LogFormat("系统语言：{0}", Application.systemLanguage);
            switch (Application.systemLanguage)
            {
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseSimplified:
                    var obj = Resources.Load<TextAsset>("CN");
                    CSVFile csv = new CSVFile(obj.bytes);
                    i18n.SetData(csv.Data);
                    break;
                default:
                    break;
            }

           
        }

        /// <summary>
        /// 初始化TalkingData
        /// </summary>
        void InitTalkingData()
        {
            string channelId = "default";
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    channelId = "Android";
                    break;
                case RuntimePlatform.IPhonePlayer:
                    channelId = "iOS";
                    break;
            }

            if(appPlatform != EAppPlatform.Default)
            {
                channelId = string.Format("{0}({1})", channelId, appPlatform);
            }
        }

        private void OnGUI()
        {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.WindowsEditor)
            {
                if (0 == DC.ins.controlMode)
                {
                    if (Input.anyKeyDown)
                    {
                        Event e = Event.current;
                        if (e.isKey)
                        {
                            DC.ins.controlMode = 2;
                            DC.ins.isShareEnable = false;
                        }
                        else if (e.isMouse)
                        {
                            DC.ins.controlMode = 1;
                        }

                        //Debug.LogFormat("操作模式：{0}", DC.ins.controlMode);
                    }
                }
            }
        }

        private void OnEnable()
        {
            GameNotice.nativeCallback += OnNativeCallback;

            GameNotice.audioAdPlayCompleted += OnWatchedAD;
            GameNotice.audioAdPlayInterrupted += UnWatchAD;
        }

        private void OnDisable()
        {
            GameNotice.nativeCallback -= OnNativeCallback;

            GameNotice.audioAdPlayCompleted -= OnWatchedAD;
            GameNotice.audioAdPlayInterrupted -= UnWatchAD;
        }

        void OnNativeCallback(string content)
        {
            switch (content)
            {
                case "YOUMI_REQUEST_AUDIO_AD_PLAY_INTERRUPTED":
                case "YOUMI_REQUEST_AUDIO_AD_PLAY_FAILED":
                    GameNotice.audioAdPlayInterrupted();
                    break;
                case "YOUMI_REQUEST_AUDIO_AD_PLAY_COMPLETED":
                    GameNotice.audioAdPlayCompleted();
                    break;
            }
        }

        private void OnDestroy()
        {
            current = null;
        }

        /// <summary>
        /// 适配屏幕
        /// </summary>
        void FitScreen()
        {
            //得到设计时相机大小
            float h = Camera.main.orthographicSize * 2;
            float w = (float)Screen.width / (float)Screen.height * h;
            DC.ins.cameraSize = new Vector2(w, h);
            //Debug.LogFormat("屏幕大小 w:{0}  h:{1}   size:[{2}, {3}]", Screen.width, Screen.height, w, h);
        }

        /// <summary>
        /// 进入关卡
        /// </summary>
        /// <param name="lv"></param>
        public void EnterLevel(int lv)
        {
            if (DC.ins.save.IsLevelLock(lv))
            {
                Main.current.msgBox.Show(DC.ins.i18n.T("Complete watch AD to unlock new level!"),"Watch","Later", () =>
                {
                    WatchAudioAD(
                    () =>
                    {
                        EnterLevel(lv);
                    },
                    EnterMenu);
                },
                EnterMenu);
                return;
            }

            if (lv > Define.LEVEL_AMOUNT)
            {
                lv = 1;
            }
            DC.ins.selectedLevel = lv;

           
            ShowStage(gamePrefab);
        }

        /// <summary>
        /// 进入菜单
        /// </summary>
        public void EnterMenu()
        {            
            ShowStage(menuPrefab);            
        }

        /// <summary>
        /// 清理舞台
        /// </summary>
        void ClearStage()
        {
            for (int i = 0; i < stageLayer.childCount; i++)
            {
                GameObject.Destroy(stageLayer.GetChild(i).gameObject);
            }
        }

        /// <summary>
        /// 显示舞台
        /// </summary>
        /// <param name="prefab"></param>
        void ShowStage(GameObject prefab, bool isLoading = true)
        {
            if (isLoading)
            {
                loading.Show(() =>
                {
                    ClearStage();
                    var go = GameObject.Instantiate(prefab);
                    go.transform.SetParent(stageLayer);
                    go.name = prefab.name;
                });
            }
            else
            {
                ClearStage();
                var go = GameObject.Instantiate(prefab);
                go.transform.SetParent(stageLayer);
                go.name = prefab.name;
            }
        }

        /// <summary>
        /// 观看成功
        /// </summary>
        void OnWatchedAD()
        {
            //关卡成功一次，会解锁一个关卡
            DC.ins.save.UnlockLevel += 1;
            if (_onWatchCompleted != null)
            {
                _onWatchCompleted();
            }
            _onWatchCompleted = null;
            _onWatchInterrupted = null;
        }

        /// <summary>
        /// 观看失败
        /// </summary>
        void UnWatchAD()
        {
            Main.current.msgBox.Show(DC.ins.i18n.T("Need watch complete!"), "Watch", "Later", () =>
              {
                  //MultiPlatformUtil.ShowAudioAD();
              },
            () =>
            {
                if (_onWatchInterrupted != null)
                {
                    _onWatchInterrupted();
                }
                _onWatchCompleted = null;
                _onWatchInterrupted = null;
            });
        }

        /// <summary>
        /// 关卡视频广告
        /// </summary>
        /// <param name="onWatchCompleted">观看成功回调</param>
        /// <param name="onWatchInterrupted">观看失败回调</param>
        public void WatchAudioAD(Action onWatchCompleted = null, Action onWatchInterrupted = null)
        {
            _onWatchCompleted = onWatchCompleted;
            _onWatchInterrupted = onWatchInterrupted;
        }
        
        public void ShowShare()
        {
            shareWin.gameObject.SetActive(true);
        }
    }
}
