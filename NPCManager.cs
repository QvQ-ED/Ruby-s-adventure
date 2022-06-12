using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

 ///<summary>
 ///NPC交互相关
 ///</summary>
public class NPCManager : MonoBehaviour
{
    public GameObject dialogImage;//对话
    public GameObject tipImage;//提示
    public float showTime=4;
    public float showTimer=-1;
 

    private void Start()
    {
        tipImage.SetActive(true);//初始显示tip
        dialogImage.SetActive(false);//初始隐藏该对象 对话框
        showTimer = -1;
    }

    private void Update()
    {
        showTimer -= Time.deltaTime;//计时
        if(showTimer<0)
        {
            tipImage.SetActive(true);
            dialogImage.SetActive(false);
        }
    }
    /// <summary>
    /// 显示对话框
    /// </summary>
    public void showdialog()
    {
        showTimer = showTime;//重置计时器,设置为最大值
        dialogImage.SetActive(true);
        tipImage.SetActive(false);
    }
}
