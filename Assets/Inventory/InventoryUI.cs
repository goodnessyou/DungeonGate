using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{   
    public ThirdPersonMovement player;
    public itemMenu menu;
    public Inventory inventory;
    [SerializeField] List<Image> icons = new List<Image>();
    [SerializeField] List<TMP_Text> amounts = new List<TMP_Text>();
    [SerializeField] List<TMP_Text> names = new List<TMP_Text>();
    [SerializeField] List<Image> borders = new List<Image>();


    void Start()
    {
        for(int i = 0;i < inventory.getSize(); i++)
        {
 
            


            icons[i].color = new Color(1, 1, 1, 1);
            icons[i].sprite = inventory.getItem(i).itemData.icon;
            amounts[i].text = (inventory.getAmount(i) > 1) ? inventory.getAmount(i).ToString() : "";
            names[i].text = inventory.getItem(i).itemData.name;
        }

        for(int i = inventory.getSize(); i < icons.Count; i++)
        {
            borders[i].color = new Color(1, 1, 1, 0);
            icons[i].color = new Color(1, 1, 1, 0);
            icons[i].sprite = null;
            amounts[i].text = "";
            names[i].text = "";
        }
    }

    public void updateUI()
    {
        for(int i = 0;i < inventory.getSize(); i++)
        {
            if(inventory.getItem(i) == player.activeArmor || inventory.getItem(i) == player.activeWeapon)
            {
                borders[i].color = new Color(1, 1, 1, 1);
            }
            else
            {
                borders[i].color = new Color(1, 1, 1, 0);
            }

            icons[i].color = new Color(1, 1, 1, 1);
            icons[i].sprite = inventory.getItem(i).itemData.icon;
            amounts[i].text = (inventory.getAmount(i) > 1) ? inventory.getAmount(i).ToString() : "";
            names[i].text = inventory.getItem(i).itemData.name;
        }

        for(int i = inventory.getSize(); i < icons.Count; i++)
        {

            borders[i].color = new Color(1, 1, 1, 0);

            icons[i].color = new Color(1, 1, 1, 0);
            icons[i].sprite = null;
            amounts[i].text = "";
            names[i].text = "";
        }
    }

    public void showMenu(int i)
    {
        if(inventory.getItem(i) != null)
        {
            menu.show(icons[i].transform, i);
        }
    }

}
