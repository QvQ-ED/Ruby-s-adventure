using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 ///<summary>
 ///�����ӵ����ƶ�����ײ
 ///</summary>
public class BulletControl : MonoBehaviour
{
    Rigidbody2D rbody;
    float Speed;
    public AudioClip hit1Clip;//������Ч
    private void Awake()//����Startִ��
    {
        rbody = GetComponent<Rigidbody2D>();
        Speed = 300f;
    }

    private void Update()
    {
        
    }

    public void Move(Vector2 moveDirection, float moveForce)
    {
        rbody.AddForce(moveDirection * moveForce);//���ӵ�ʩ�ӳ�ĳһ���������
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        EnemyControl ec = other.gameObject.GetComponent<EnemyControl>();
        if (ec != null)
        {
            ec.Fixed();//�޸�����
        }
        AudioManager.instance.AudioPlay(hit1Clip);

        Destroy(this.gameObject);//���ٸö���
    }
}
