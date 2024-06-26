using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestItemMenu : MonoBehaviour
{
    public ThirdPersonMovement player;
    public int i;

    public void take()
    {
        player.take(i);
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
