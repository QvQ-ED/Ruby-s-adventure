using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 ///<summary>
 ///补充子弹
 ///</summary>
public class BulletBag : MonoBehaviour
{
    public int bulletcount = 10;
    public ParticleSystem collectEffect;//拾取特效


    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerControl pc = other.GetComponent<PlayerControl>();
        if (pc != null)
        {
            if (pc.MycurBulletcount < pc.MymaxBulletcount)
            {
                pc.ChangeBulletCount(bulletcount);
                Instantiate(collectEffect, transform.position, Quaternion.identity);//添加拾取特效
                Destroy(this.gameObject);
            }
            
        }
    }
}
