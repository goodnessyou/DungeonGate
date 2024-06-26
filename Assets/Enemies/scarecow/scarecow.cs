using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scarecow : Enemy
{
    

  

    void Start()
    {


    }

    
    void Update()
    {
        
    }



    private void hitEnd()
    {
        //Debug.Log("HIT END");
        
        animator.SetBool("isHit", false);
    }
}
