using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField]
    private int damage;
    public bool staticWeapon = false;

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
    public int GetDamage()
    {
        if(!staticWeapon)
        {
            transform.GetComponent<Collider>().enabled = false;
        }
        return damage;
    }
}
