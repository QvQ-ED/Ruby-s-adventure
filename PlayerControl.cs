using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 ///<summary>
 ///
 ///</summary>
public class PlayerControl : MonoBehaviour
{
    public float Speed = 5f;
    Rigidbody2D rbody;
    Animator anim;
    private int maxhealth = 5;
    private int currenthealth = 2;
    private float invinciableTime = 2f;//Ĭ���޵�ʱ��
    private float invinciableTimer;//�޵�ʱ���ʱ��
    private bool isInvinciable;//�Ƿ��޵�
    public GameObject bulletPrefab;//�ӵ�


    [SerializeField]//���л�
    private int curBulletCount;//�ӵ���
    private int maxBulletCount;

    public int MycurBulletcount { get{ return curBulletCount; } }//�����Ϳ������ӵ����Ľű��������������
    public int MymaxBulletcount { get { return maxBulletCount; } }

    //===��ҵķ�������Ϣ===========================/
    private Vector2 lookDirection;

    //===��ҵ���Ч==========================/
    //�Լ��������
    public AudioClip hitClip;
    public AudioClip launchClip;
  
    


    public int Mymaxhealth { get { return maxhealth; } }//��ʾ����ֻ�����ɸı�
    public int Mycurrenthealth { get { return currenthealth; } }

    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        invinciableTimer = invinciableTime;
        isInvinciable = false;
        anim = GetComponent<Animator>();
        UIManager.instance.UpdateHealthBar(currenthealth, maxhealth);
        curBulletCount = 99;
        maxBulletCount = 99;
        UIManager.instance.UpdateBulletCount(curBulletCount, maxBulletCount);
    }

    private void Update()
    {   //ÿִ֡��

        float moveX = Input.GetAxisRaw("Horizontal");//����ˮƽ�ƶ����� A��-1 D��1 û����Ϊ0
        float moveY = Input.GetAxisRaw("Vertical");//���ƴ�ֱ�ƶ����� W��1 S��-1

        //=========ȷ��������Ϣ================
        //�������к�LookXLookY������blendtree������

        Vector2 moveVector = new Vector2(moveX, moveY);//ʵʱ�仯 ��ֹʱΪ0��0
        if (moveVector.x != 0 || moveVector.y != 0)//���벥��һ�ֶ���
        {
            lookDirection = moveVector;//ָ���Ӧ״̬����
            anim.SetFloat("Look X", lookDirection.x);
            anim.SetFloat("Look Y", lookDirection.y);
           
        }
         anim.SetFloat("Speed", moveVector.magnitude);//�����ܻ�ִ�У������Ƿ�Ϊ0
        //�ƶ�
        Vector2 position = transform.position;
        //position.x += moveX * Speed * Time.deltaTime;
        //position.y += moveY * Speed * Time.deltaTime;
        position += moveVector * Speed * Time.deltaTime;
        rbody.MovePosition(position);//���任���λ�ý�����ǰ����λ��

        //�޵��ж�
        if (isInvinciable==true)//��������޵�ʱ����,��ʼ����ʱ
        {
            invinciableTimer -= Time.deltaTime;
            if (invinciableTimer <= 0)
            {
                isInvinciable = false;//�޵н���
            }
        }

        //========����K�����й���======
        //1 ���� 2 ����
        if (Input.GetKeyDown(KeyCode.K)&&(curBulletCount>0))
        {
            ChangeBulletCount(-1);
            anim.SetTrigger("Launch");//���Ź�������
            AudioManager.instance.AudioPlay(launchClip);//����������Ч
            GameObject bullet = Instantiate(bulletPrefab, rbody.position+Vector2.up*2f, Quaternion.identity);//����һ����Ϸ����
            BulletControl bc = bullet.GetComponent<BulletControl>();//�õ��ö���Ԥ�Ƽ���BulletControl���
            if (bc != null)
            {
                bc.Move(lookDirection, 300);
            }
       
        }

        //=======����E����NPC����======
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D hit = Physics2D.Raycast(rbody.position, lookDirection, 2f, LayerMask.GetMask("NPC"));//�������� ��ʼλ�� ���� ��� ���㼶
            if (hit.collider != null)
            {
                NPCManager npc = hit.collider.GetComponent<NPCManager>();//�õ������ж�������
                if (npc != null)
                {
                    npc.showdialog();
                }
            }
        }
    }
    
     public void Changehealth(int amount)//�Զ��ָı�Ѫ��������������
    {
        //����ܵ��˺�
        if (amount < 0)
        {
            if (isInvinciable == true)
            { return; }//�޵�ʱ�����,�˳��������
                isInvinciable = true;//���ֻ���޵н�����ִ��һ��,ֻ��ִ��    ��һ�βŵ�Ѫ
            invinciableTimer = invinciableTime;
            anim.SetTrigger("Hit");//������������ ����
            AudioManager.instance.AudioPlay(hitClip);//������Ч
        }

        //Debug.Log(currenthealth + "/" + maxhealth);
        currenthealth = Mathf.Clamp(amount+currenthealth, 0, maxhealth);
        //  Debug.Log(currenthealth + "/" + maxhealth);
        UIManager.instance.UpdateHealthBar(currenthealth, maxhealth);//����Ѫ��

    }

    public void ChangeBulletCount(int amount)
    {
        curBulletCount = Mathf.Clamp(curBulletCount + amount, 0, maxBulletCount);
        UIManager.instance.UpdateBulletCount(curBulletCount, maxBulletCount);

    }
}
