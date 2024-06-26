using UnityEngine;

public class PlayerCheckerScript : MonoBehaviour
{
    public ElevatorScript elevator;
    private void OnTriggerStay(Collider other) {
        if(other.transform.CompareTag("Player"))
        {
            other.transform.SetParent(transform, false);
            other.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f); 
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.transform.CompareTag("Player"))
        {
            other.transform.parent = null;
            other.transform.localScale = new Vector3(2, 2, 2);    
        }
    }

}
