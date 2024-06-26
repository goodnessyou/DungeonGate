using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            transform.GetComponentInParent<Enemy>().SetStateAttack();
        }    
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player"))
        {
            transform.GetComponentInParent<Enemy>().SetStateFolow();
        }    
    }
}
