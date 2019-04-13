using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jing.Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        /// <summary>
        /// 声音剪辑
        /// </summary>
        public AudioClip[] audioClipList;

        /// <summary>
        /// 背景音播放源
        /// </summary>
        public AudioSource bgmAS = null;

        /// <summary>
        /// 音效播放源
        /// </summary>
        public AudioSource[] effectTracks = null;

        /// <summary>
        /// 背景音音量
        /// </summary>
        public float bgmVolume
        {
            get { return bgmAS.volume; }
            set
            {
                bgmAS.volume = value;
            }
        }

        /// <summary>
        /// 音效音量
        /// </summary>
        public float effectVolume
        {
            get
            {
                return effectTracks[0].volume;
            }

            set
            {
                foreach (var obj in effectTracks)
                {
                    obj.volume = value;
                }
            }
        }

        void Start()
        {

        }
        
        void Update()
        {

        }

        public void PlayBGM(int acIdx)
        {
            if(acIdx < 0 || acIdx >= audioClipList.Length)
            {
                bgmAS.Stop();
                return;
            }

            AudioClip ac = audioClipList[acIdx];
            bgmAS.clip = ac;
            bgmAS.Play();
        }

        /// <summary>
        /// 停止播放背景音乐
        /// </summary>
        /// <param name="smooth">是否平滑淡出</param>
        public void StopBGM(bool smooth = false)
        {
            bgmAS.Stop();
        }

        public void PlayEffect(int acIdx)
        {
            if (acIdx < 0 || acIdx >= audioClipList.Length)
            {
                return;
            }

            AudioClip ac = audioClipList[acIdx];

            AudioSource useSource = null;            
            foreach (var source in effectTracks)
            {
                if (false == source.isPlaying)
                {
                    useSource = source;
                }
            }

            if (null == useSource)
            {
                useSource = effectTracks[0];
            }

            useSource.clip = ac;
            useSource.Play();
        }

        public void PlayEffect(int acIdx, int track)
        {
            if (acIdx < 0 || acIdx >= audioClipList.Length)
            {
                return;
            }

            AudioClip ac = audioClipList[acIdx];

            AudioSource useSource = effectTracks[track];
            useSource.clip = ac;
            useSource.Play();
        }

        /// <summary>
        /// 停止印象
        /// </summary>
        /// <param name="track"></param>
        public void StopEffect(int track = -1)
        {
            if(track < 0)
            {
                foreach(var source in  effectTracks)
                {
                    source.Stop();
                }
            }
            else if(track < effectTracks.Length)
            {
                effectTracks[track].Stop();
            }
        }
        
    }
}
