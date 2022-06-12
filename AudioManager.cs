using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 ///<summary>
 ///播放音乐音效,控制交互音乐播放，音乐播放唯一出口
 ///</summary>
public class AudioManager : MonoBehaviour
{
   public static AudioManager instance { get; private set; }

    private AudioSource audioS;
    private void Awake()
    {
        instance = this;//实例化对象与绑定这个脚本的物体唯一关联
        audioS = GetComponent<AudioSource>();//得到物体上的AudioSource组件
    }

    public void AudioPlay(AudioClip clip)
    {
        audioS.PlayOneShot(clip);//不影响另外独立的Audio组件音乐播放
    }

}
