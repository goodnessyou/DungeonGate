using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public ItemInstance item;
    public int amount;

    public InventorySlot(ItemInstance item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }
}
