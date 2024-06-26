using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHitCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.GetComponent<PlayerWeapon>())
        {
            CombatEngine.hittingTheEnemy(GetComponentInParent<Enemy>(), other.GetComponent<PlayerWeapon>());
        }
    }
}
