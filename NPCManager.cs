using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

 ///<summary>
 ///NPC�������
 ///</summary>
public class NPCManager : MonoBehaviour
{
    public GameObject dialogImage;//�Ի�
    public GameObject tipImage;//��ʾ
    public float showTime=4;
    public float showTimer=-1;
 

    private void Start()
    {
        tipImage.SetActive(true);//��ʼ��ʾtip
        dialogImage.SetActive(false);//��ʼ���ظö��� �Ի���
        showTimer = -1;
    }

    private void Update()
    {
        showTimer -= Time.deltaTime;//��ʱ
        if(showTimer<0)
        {
            tipImage.SetActive(true);
            dialogImage.SetActive(false);
        }
    }
    /// <summary>
    /// ��ʾ�Ի���
    /// </summary>
    public void showdialog()
    {
        showTimer = showTime;//���ü�ʱ��,����Ϊ���ֵ
        dialogImage.SetActive(true);
        tipImage.SetActive(false);
    }
}
