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
    private float invinciableTime = 2f;//默认无敌时间
    private float invinciableTimer;//无敌时间计时器
    private bool isInvinciable;//是否无敌
    public GameObject bulletPrefab;//子弹


    [SerializeField]//序列化
    private int curBulletCount;//子弹数
    private int maxBulletCount;

    public int MycurBulletcount { get{ return curBulletCount; } }//这样就可以让子弹包的脚本访问这个变量了
    public int MymaxBulletcount { get { return maxBulletCount; } }

    //===玩家的方向朝向信息===========================/
    private Vector2 lookDirection;

    //===玩家的音效==========================/
    //自己在外面绑定
    public AudioClip hitClip;
    public AudioClip launchClip;
  
    


    public int Mymaxhealth { get { return maxhealth; } }//表示数据只读不可改变
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
    {   //每帧执行

        float moveX = Input.GetAxisRaw("Horizontal");//控制水平移动方向 A：-1 D：1 没按就为0
        float moveY = Input.GetAxisRaw("Vertical");//控制垂直移动方向 W：1 S：-1

        //=========确定朝向信息================
        //对于所有含LookXLookY参数的blendtree均适用

        Vector2 moveVector = new Vector2(moveX, moveY);//实时变化 静止时为0，0
        if (moveVector.x != 0 || moveVector.y != 0)//必须播放一种动画
        {
            lookDirection = moveVector;//指向对应状态动画
            anim.SetFloat("Look X", lookDirection.x);
            anim.SetFloat("Look Y", lookDirection.y);
           
        }
         anim.SetFloat("Speed", moveVector.magnitude);//这里总会执行，不论是否为0
        //移动
        Vector2 position = transform.position;
        //position.x += moveX * Speed * Time.deltaTime;
        //position.y += moveY * Speed * Time.deltaTime;
        position += moveVector * Speed * Time.deltaTime;
        rbody.MovePosition(position);//将变换后的位置交给当前刚体位置

        //无敌判断
        if (isInvinciable==true)//如果处于无敌时间内,开始倒计时
        {
            invinciableTimer -= Time.deltaTime;
            if (invinciableTimer <= 0)
            {
                isInvinciable = false;//无敌结束
            }
        }

        //========按下K键进行攻击======
        //1 生成 2 发射
        if (Input.GetKeyDown(KeyCode.K)&&(curBulletCount>0))
        {
            ChangeBulletCount(-1);
            anim.SetTrigger("Launch");//播放攻击动画
            AudioManager.instance.AudioPlay(launchClip);//触发攻击音效
            GameObject bullet = Instantiate(bulletPrefab, rbody.position+Vector2.up*2f, Quaternion.identity);//生成一个游戏对象
            BulletControl bc = bullet.GetComponent<BulletControl>();//得到该对象预制件的BulletControl组件
            if (bc != null)
            {
                bc.Move(lookDirection, 300);
            }
       
        }

        //=======按下E键与NPC交互======
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D hit = Physics2D.Raycast(rbody.position, lookDirection, 2f, LayerMask.GetMask("NPC"));//发射射线 起始位置 方向 射程 检测层级
            if (hit.collider != null)
            {
                NPCManager npc = hit.collider.GetComponent<NPCManager>();//得到被击中对象的组件
                if (npc != null)
                {
                    npc.showdialog();
                }
            }
        }
    }
    
     public void Changehealth(int amount)//对多种改变血量的条件均适用
    {
        //如果受到伤害
        if (amount < 0)
        {
            if (isInvinciable == true)
            { return; }//无敌时间段内,退出这个函数
                isInvinciable = true;//这个只在无敌结束后执行一次,只有执行    这一次才掉血
            invinciableTimer = invinciableTime;
            anim.SetTrigger("Hit");//触发受伤条件 动画
            AudioManager.instance.AudioPlay(hitClip);//受伤音效
        }

        //Debug.Log(currenthealth + "/" + maxhealth);
        currenthealth = Mathf.Clamp(amount+currenthealth, 0, maxhealth);
        //  Debug.Log(currenthealth + "/" + maxhealth);
        UIManager.instance.UpdateHealthBar(currenthealth, maxhealth);//更新血条

    }

    public void ChangeBulletCount(int amount)
    {
        curBulletCount = Mathf.Clamp(curBulletCount + amount, 0, maxBulletCount);
        UIManager.instance.UpdateBulletCount(curBulletCount, maxBulletCount);

    }
}
