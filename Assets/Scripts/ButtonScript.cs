using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    bool isPressed = false;

    [SerializeField]
    private MovingObjectScript[] movingObjects;

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("PlayerWeapon"))
        {
            if(!isPressed)
            {
                Press();
            }
            else
            {
                UnPress();
            }
        }
    }

    private void Press()
    {
        isPressed = true;
        gameObject.GetComponent<Outline> ().OutlineColor = Color.red;
        transform.Translate(-0.2f,0,0);
        foreach(MovingObjectScript obj in movingObjects)
        {
            obj.Interact();
        }
    }

    private void UnPress()
    {
        isPressed = false;
        gameObject.GetComponent<Outline> ().OutlineColor = Color.green;
        transform.Translate(0.2f,0,0);
        foreach(MovingObjectScript obj in movingObjects)
        {
            obj.Interact();
        }
    }
}
