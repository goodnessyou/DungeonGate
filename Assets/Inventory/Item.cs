using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "inventory/item")]
public abstract class Item : ScriptableObject
{
    public int id;
    public string item_name;
    public int max_stack;

    public Sprite icon;
    public GameObject prefab;

    public string action;

    public abstract bool use(ThirdPersonMovement player, ItemInstance itemData);
}
