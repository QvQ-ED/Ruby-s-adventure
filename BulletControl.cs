using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 ///<summary>
 ///控制子弹的移动、碰撞
 ///</summary>
public class BulletControl : MonoBehaviour
{
    Rigidbody2D rbody;
    float Speed;
    public AudioClip hit1Clip;//命中音效
    private void Awake()//先与Start执行
    {
        rbody = GetComponent<Rigidbody2D>();
        Speed = 300f;
    }

    private void Update()
    {
        
    }

    public void Move(Vector2 moveDirection, float moveForce)
    {
        rbody.AddForce(moveDirection * moveForce);//对子弹施加朝某一个方向的力
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        EnemyControl ec = other.gameObject.GetComponent<EnemyControl>();
        if (ec != null)
        {
            ec.Fixed();//修复敌人
        }
        AudioManager.instance.AudioPlay(hit1Clip);

        Destroy(this.gameObject);//销毁该对象
    }
}
