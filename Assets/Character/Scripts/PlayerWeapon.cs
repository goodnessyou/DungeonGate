using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public int damage = 0;
    public bool isPhys = true;
    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
