using UnityEngine;

public class ElevatorScript : MovingObjectScript
{
    public GameObject TopPoint;
    public GameObject BotPoint;
    private bool MoveUp = false;
    private bool MoveDown = true;

    public override void Interact()
    {
        StartMove();
    }

    public void StartMove()
    {
        if(MoveUp)
        {
            MoveDown = true;
            MoveUp = false;
            return;
        }
        if(MoveDown)
        {
            MoveUp = true;
            MoveDown = false;
            return;
        }
    }

    public void StopMove()
    {

    }
    // private void OnCollisionStay(Collision other) {
    //     Debug.Log("stay");
    //     if(other.transform.CompareTag("Player"))
    //     {
    //         Debug.Log("stayplayer");
    //         other.transform.parent = transform;
    //     }
    // }

    // private void OnCollisionExit(Collision other) {
    //     if(other.transform.CompareTag("Player"))
    //     {
    //         Debug.Log("exit");
    //         other.transform.parent = null;
    //     }
    // }
    
    void FixedUpdate()
    {
        if(MoveUp)
        {
            var step =  2 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, TopPoint.transform.position, step);
            if(transform.position == TopPoint.transform.position)
            {
                StopMove();
            }
            return;
        }
        if(MoveDown)
        {
            var step =  2 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, BotPoint.transform.position, step);
            if(transform.position == BotPoint.transform.position)
            {
                StopMove();
            }
            return;
        }
    }
}
