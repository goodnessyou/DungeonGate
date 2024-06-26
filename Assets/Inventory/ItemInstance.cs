using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemInstance
{
    [SerializeReference] public Item itemData;
    

    public bool use(ThirdPersonMovement player)
    {
        return itemData.use(player, this);
    }
}
