using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 ///<summary>
 ///UI管理相关
 ///</summary>
public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }//通过静态方法获取单例,类似于获取这个组件,在别的内中直接使用该类

    public Image HealthBar;//获取绑定的image图像/组件

    public Text bulletCountText;//子弹数量内容显示

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// 更新血条，通过控制Image 类的 HealthBar来实现
    /// </summary>
    /// <param name="curAmount"></param>
    /// <param name="maxAmount"></param>
    public void UpdateHealthBar(int curAmount, int maxAmount)
    {
        HealthBar.fillAmount = (float)curAmount / (float)maxAmount;
    }
   
    /// <summary>
    /// 更新子弹数量文本的显示
    /// </summary>
    /// <param name="current"></param>
    /// <param name="max"></param>
    public void UpdateBulletCount(int current, int max)
    {
        bulletCountText.text = current.ToString() + "/" + max.ToString();//数字转字符串
    }
}
