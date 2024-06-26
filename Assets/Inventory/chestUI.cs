using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class chestUI : MonoBehaviour
{
    public ThirdPersonMovement player;
    public chestItemMenu chestMenu;
    public Inventory inventory = null;
    [SerializeField] List<Image> icons = new List<Image>();
    [SerializeField] List<TMP_Text> amounts = new List<TMP_Text>();
  

    void Start()
    {
        // for(int i = 0;i < inventory.getSize(); i++)
        // {
        //     icons[i].color = new Color(1, 1, 1, 1);
        //     icons[i].sprite = inventory.getItem(i).itemData.icon;
        //     amounts[i].text = (inventory.getAmount(i) > 1) ? inventory.getAmount(i).ToString() : "";
        // }

        // for(int i = inventory.getSize(); i < icons.Count; i++)
        // {
        //     icons[i].color = new Color(1, 1, 1, 0);
        //     icons[i].sprite = null;
        //     amounts[i].text = "";
        // }
    }

    public void updateUI()
    {
        for(int i = 0;i < inventory.getSize(); i++)
        {
            icons[i].color = new Color(1, 1, 1, 1);
            icons[i].sprite = inventory.getItem(i).itemData.icon;
            amounts[i].text = (inventory.getAmount(i) > 1) ? inventory.getAmount(i).ToString() : "";
        }

        for(int i = inventory.getSize(); i < icons.Count; i++)
        {
            icons[i].color = new Color(1, 1, 1, 0);
            icons[i].sprite = null;
            amounts[i].text = "";
        }
    }

    public void showMenu(int i)
    {
        if(inventory.getItem(i) != null)
        {
            chestMenu.show(icons[i].transform, i);
        }
    }

}
