using UnityEngine;

public class BreakingBox : MonoBehaviour
{

    public GameObject destructBox;
    private int[] rotateAngles = new int[3];
    public void Start()
    {
        rotateAngles[0] = 90;
        rotateAngles[1] = 180;
        rotateAngles[2] = 270;
        int rotateAngle = rotateAngles[Random.Range(0, rotateAngles.Length)];
        transform.rotation = Quaternion.Euler(new Vector3(0,rotateAngle,0));
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("PlayerWeapon"))
        {
            Destroy(gameObject);
            GameObject dBox = Instantiate(destructBox, transform.position, transform.rotation);
            Destroy(dBox, 5f);
        }
    }
}
