using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 ///<summary>
 ///���˿������
 ///</summary>
public class EnemyControl : MonoBehaviour
{
    public float speed = 3;
    private Rigidbody2D rbody;
    public bool isVertical;//�Ƿ�ֱ�����ƶ� Ĭ��ֵΪ0
    private Vector2 moveDirection;//�ƶ�����
    public float changeDirectionTime = 2f;//�ı䷽���ʱ��
    private float changeTimer;//�ı䷽��ļ�ʱ��
    private Animator anim;
    private bool isFixed;
    public ParticleSystem brokenEffect;//��������Ч   �����ͼ�ΰ�
    public AudioClip fixedClip;//���޸�����Ч

    private void Start()
    { 
        //�õ�������Ͷ��Ѷ���ı�����ֵ
        rbody = GetComponent<Rigidbody2D>();//�õ���ǰ��Rigidbody2D���

        anim = GetComponent<Animator>();//�õ���ǰAnimator���

        moveDirection = isVertical ? Vector2.up : Vector2.right;//����Ǵ�ֱ�ƶ�����������ϣ���������

        changeTimer = changeDirectionTime;

        isFixed = false;
    }

    private void Update()
    {
        if (isFixed  ==false)//���δ���޸���ִ�У��޸���ִ�У��������޸��󶯻�
        {
            changeTimer -= Time.deltaTime;
            if (changeTimer < 0)
            {
                moveDirection *= -1;//�ı䷽��
                changeTimer = changeDirectionTime;//���¿�ʼ��ʱ
            }


            Vector2 position = rbody.position;   //2D���������λ��
            position.x += moveDirection.x * speed * Time.deltaTime;
            position.y += moveDirection.y * speed * Time.deltaTime;
            rbody.MovePosition(position);


            //��������
            anim.SetFloat("Movex", moveDirection.x);//���ݺ��ߵ�ֵȷ��ǰ�ߵĶ����������� ��Ϊfloat
            anim.SetFloat("Movey", moveDirection.y);
        }
    }
    /// <summary>
    /// �ڷ�����ǰ�水3��/���ɳ��ָ�ע��   ����ҵ���ײ���
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerControl pc = other.gameObject.GetComponent<PlayerControl>();//ע�������������ֻ�ܴ���һ����ײ�������gameobject��ʼ��
        if (pc != null)//������������������ֱ�ӻ�ȡ������ �Ա�Start��������
        {
            pc.Changehealth(-1);
        }
    }

    public void Fixed()
    {
        if (brokenEffect.isPlaying == true)
        {
            brokenEffect.Stop();
            AudioManager.instance.AudioPlay(fixedClip);
        }
        isFixed = true;
        rbody.simulated = false;//���ø�������
        anim.SetTrigger("Fixed");//���ű��޸��Ķ�����Fixed����������,��Ҫ�ڶ�Ӧ��Animator Controler����ǰ��������
    }
}
