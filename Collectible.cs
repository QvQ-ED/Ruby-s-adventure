using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 ///<summary>
 ///
 ///</summary>
public class Collectible : MonoBehaviour
{
    public ParticleSystem collectEffect;//拾取特效 绑定对应特效

    public AudioClip collectclip;//拾取音效
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
                Instantiate(collectEffect, transform.position, Quaternion.identity);//生成特效 因为这个特效只播放一次，区别与之前的循环播放 loop
                Destroy(this.gameObject);
                AudioManager.instance.AudioPlay(collectclip);//播放音效，通过另一个类的方法，本类的参数
            }
            Debug.Log("玩家碰到了草莓！");
        }
    }

}
