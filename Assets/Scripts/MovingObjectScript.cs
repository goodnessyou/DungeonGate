using UnityEngine;

public abstract class MovingObjectScript : MonoBehaviour
{
    public bool isActive;
    public ButtonScript[] Buttons;

    public abstract void Interact();
}
