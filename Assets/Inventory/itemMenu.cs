using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemMenu : MonoBehaviour
{
    public ThirdPersonMovement player;
    public int i;

    public void use()
    {
        player.use(i);
        hide();
    }

    public void drop()
    {
        player.drop(i);
        hide();
    }
    public void destroy()
    {
        player.destroy(i);
        hide();
    }

    public void place()
    {
        player.place(i);
        hide();
    }
    public void show(Transform parent, int ind)
    {
        i = ind;
        transform.SetParent(parent, false);
        gameObject.SetActive(true);
    }
    public void hide()
    {
        gameObject.SetActive(false);
    }

    private void LateUpdate() 
    {
        if(Input.GetMouseButton(1)) hide();    
    }
}
