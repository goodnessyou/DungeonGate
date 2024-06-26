using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjectScript : MovingObjectScript
{
    [SerializeField]
    private bool Move = false;
    public float degreesPerSecond0 = 20f;
    public float degreesPerSecond1 = 20f;
    public float degreesPerSecond2 = 20f;
    

    public override void Interact()
    {
        if(Move)
        {
            Move = false;
            return;
        }
        if(!Move)
        {
            Move = true;
            return;
        }
    }

    


    
    void FixedUpdate()
    {
        if(Move)
        {
            transform.Rotate(new Vector3(degreesPerSecond0, degreesPerSecond1, degreesPerSecond2) * Time.deltaTime);
        }
    }
}
