using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 ///<summary>
 ///UI�������
 ///</summary>
public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }//ͨ����̬������ȡ����,�����ڻ�ȡ������,�ڱ������ֱ��ʹ�ø���

    public Image HealthBar;//��ȡ�󶨵�imageͼ��/���

    public Text bulletCountText;//�ӵ�����������ʾ

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// ����Ѫ����ͨ������Image ��� HealthBar��ʵ��
    /// </summary>
    /// <param name="curAmount"></param>
    /// <param name="maxAmount"></param>
    public void UpdateHealthBar(int curAmount, int maxAmount)
    {
        HealthBar.fillAmount = (float)curAmount / (float)maxAmount;
    }
   
    /// <summary>
    /// �����ӵ������ı�����ʾ
    /// </summary>
    /// <param name="current"></param>
    /// <param name="max"></param>
    public void UpdateBulletCount(int current, int max)
    {
        bulletCountText.text = current.ToString() + "/" + max.ToString();//����ת�ַ���
    }
}
