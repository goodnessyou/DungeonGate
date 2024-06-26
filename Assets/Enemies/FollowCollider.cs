using UnityEngine;

public class FollowCollider : MonoBehaviour
{
    private void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            transform.GetComponentInParent<Enemy>().SetStateRetreat();
        }        
    }
}
