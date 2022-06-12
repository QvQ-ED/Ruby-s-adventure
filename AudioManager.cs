using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 ///<summary>
 ///����������Ч,���ƽ������ֲ��ţ����ֲ���Ψһ����
 ///</summary>
public class AudioManager : MonoBehaviour
{
   public static AudioManager instance { get; private set; }

    private AudioSource audioS;
    private void Awake()
    {
        instance = this;//ʵ���������������ű�������Ψһ����
        audioS = GetComponent<AudioSource>();//�õ������ϵ�AudioSource���
    }

    public void AudioPlay(AudioClip clip)
    {
        audioS.PlayOneShot(clip);//��Ӱ�����������Audio������ֲ���
    }

}
