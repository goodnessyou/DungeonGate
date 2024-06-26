using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "inventory/Resource")]
public class Resource : Item
{
    public override bool use(ThirdPersonMovement player, ItemInstance itemData)
    {
        // player.activeWeapon = itemData;
        // if(player.holder.transform.childCount > 0)
        // {
        //     Destroy(player.holder.transform.GetChild(0).gameObject);
        // }
        // Instantiate(player.activeWeapon.itemData.prefab, player.holder.transform);
        return false;
    }
}
