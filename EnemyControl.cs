using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 ///<summary>
 ///敌人控制相关
 ///</summary>
public class EnemyControl : MonoBehaviour
{
    public float speed = 3;
    private Rigidbody2D rbody;
    public bool isVertical;//是否垂直方向移动 默认值为0
    private Vector2 moveDirection;//移动方向
    public float changeDirectionTime = 2f;//改变方向的时间
    private float changeTimer;//改变方向的计时器
    private Animator anim;
    private bool isFixed;
    public ParticleSystem brokenEffect;//公开损坏特效   用外界图形绑定
    public AudioClip fixedClip;//被修复的音效

    private void Start()
    { 
        //得到组件，和对已定义的变量赋值
        rbody = GetComponent<Rigidbody2D>();//得到当前的Rigidbody2D组件

        anim = GetComponent<Animator>();//得到当前Animator组件

        moveDirection = isVertical ? Vector2.up : Vector2.right;//如果是垂直移动，方向就向上，否则向右

        changeTimer = changeDirectionTime;

        isFixed = false;
    }

    private void Update()
    {
        if (isFixed  ==false)//如果未被修复则执行，修复后不执行，仅播放修复后动画
        {
            changeTimer -= Time.deltaTime;
            if (changeTimer < 0)
            {
                moveDirection *= -1;//改变方向
                changeTimer = changeDirectionTime;//重新开始计时
            }


            Vector2 position = rbody.position;   //2D刚体组件的位置
            position.x += moveDirection.x * speed * Time.deltaTime;
            position.y += moveDirection.y * speed * Time.deltaTime;
            rbody.MovePosition(position);


            //动画播放
            anim.SetFloat("Movex", moveDirection.x);//根据后者的值确定前者的动画播放种类 都为float
            anim.SetFloat("Movey", moveDirection.y);
        }
    }
    /// <summary>
    /// 在方法的前面按3次/即可出现该注释   与玩家的碰撞检测
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerControl pc = other.gameObject.GetComponent<PlayerControl>();//注意这个方法里面只能从另一个碰撞的物体的gameobject开始找
        if (pc != null)//对其他对象的组件不能直接获取，如上 对比Start里面的语句
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
        rbody.simulated = false;//禁用刚体物理
        anim.SetTrigger("Fixed");//播放被修复的动画，Fixed参数被激发,需要在对应的Animator Controler中提前设立关联
    }
}
