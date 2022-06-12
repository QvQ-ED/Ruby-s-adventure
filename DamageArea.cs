using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 ///<summary>
 ///
 ///</summary>
public class DamageArea : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)//在collider区域内每帧都会执行
    {
        PlayerControl pc = other.GetComponent<PlayerControl>();
        if (pc != null)
        {
            pc.Changehealth(-1);
        }
    }

}
