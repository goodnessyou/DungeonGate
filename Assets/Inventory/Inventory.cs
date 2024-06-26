using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    [SerializeField] public List<InventorySlot> slots;
    [SerializeField] public int size;
    [SerializeField] public UnityEvent onInventoryChanged;

    // Start is called before the first frame update
    void Start()
    {
        onInventoryChanged.Invoke();
    }

    public ItemInstance getItem(int i)
    {
        return(i<slots.Count) ? slots[i].item : null;
    }

    public int getAmount(int i)
    {
        return (i<slots.Count) ? slots[i].amount : 0;
    }

    public int getSize()
    {
        return slots.Count;
    }

    public int addItems(ItemInstance item, int amount)
    {
        foreach(InventorySlot slot in slots)
        {
            if(slot.item.itemData.id == item.itemData.id)
            {
                if(slot.amount < item.itemData.max_stack)
                {
                    Debug.Log(amount);
                    Debug.Log(slot.amount);
                    if((slot.amount + amount) > item.itemData.max_stack)
                    {
                        amount -= item.itemData.max_stack - slot.amount;
                        slot.amount = item.itemData.max_stack;
                        onInventoryChanged.Invoke();
                        continue;
                    }
                    slot.amount += amount;
                    onInventoryChanged.Invoke();
                    return 0;
                }
            }
        }

        if(slots.Count >= size) return amount;

        while(amount > item.itemData.max_stack)
        {
            ItemInstance itm = new ItemInstance();
            itm.itemData = item.itemData;
            slots.Add(new InventorySlot(itm, itm.itemData.max_stack));
            amount -= itm.itemData.max_stack;
            onInventoryChanged.Invoke();
            if(slots.Count >= size)return amount; 
        }
        slots.Add(new InventorySlot(item, amount));
        onInventoryChanged.Invoke();
        return 0;

    }


    public void removeItem(int i)
    {
        if(i < slots.Count)
        {
            slots[i].amount--;
            if(slots[i].amount <= 0)
            {
                slots.RemoveAt(i);
            }
            onInventoryChanged.Invoke();
        }
    }

    public void dropItem(int i)
    {
        if(i < slots.Count)
        {
            GameObject pref = slots[i].item.itemData.prefab;

            GameObject o = Instantiate(pref, transform.position + transform.forward *3, pref.transform.rotation);
            o.GetComponent<ItemContainer>().item = slots[i].item;
            o.GetComponent<ItemContainer>().amount = slots[i].amount;
            slots.RemoveAt(i);

            onInventoryChanged.Invoke();
        }
    }

    public void destroyItem(int i)
    {
        if(i < slots.Count)
        {
            slots.RemoveAt(i);
            onInventoryChanged.Invoke();
        }
    }
}
