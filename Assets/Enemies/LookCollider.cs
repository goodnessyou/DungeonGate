using UnityEngine;

public class LookCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            transform.GetComponentInParent<Enemy>().SetStateFolow();
        }    
    }
}
