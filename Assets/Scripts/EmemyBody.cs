using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EmemyBody : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "PlayerWeapon")
        {
            GameObject obj = this.transform.parent.gameObject;
            int damage = collision.transform.root.GetComponent<Player>().playerDamage;
            obj.GetComponent<Enemy>().Hit(damage);
        }
    }
}
