using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 ///<summary>
 ///
 ///</summary>
public class Collectible : MonoBehaviour
{
    public ParticleSystem collectEffect;//ʰȡ��Ч �󶨶�Ӧ��Ч

    public AudioClip collectclip;//ʰȡ��Ч
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerControl pc = other.GetComponent<PlayerControl>();
        if (pc != null)
        {
            if (pc.Mycurrenthealth < pc.Mymaxhealth)
            { 
                pc.Changehealth(1);
                Instantiate(collectEffect, transform.position, Quaternion.identity);//������Ч ��Ϊ�����Чֻ����һ�Σ�������֮ǰ��ѭ������ loop
                Destroy(this.gameObject);
                AudioManager.instance.AudioPlay(collectclip);//������Ч��ͨ����һ����ķ���������Ĳ���
            }
            Debug.Log("��������˲�ݮ��");
        }
    }

}
