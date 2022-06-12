using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 ///<summary>
 ///�����ӵ�
 ///</summary>
public class BulletBag : MonoBehaviour
{
    public int bulletcount = 10;
    public ParticleSystem collectEffect;//ʰȡ��Ч


    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerControl pc = other.GetComponent<PlayerControl>();
        if (pc != null)
        {
            if (pc.MycurBulletcount < pc.MymaxBulletcount)
            {
                pc.ChangeBulletCount(bulletcount);
                Instantiate(collectEffect, transform.position, Quaternion.identity);//���ʰȡ��Ч
                Destroy(this.gameObject);
            }
            
        }
    }
}
