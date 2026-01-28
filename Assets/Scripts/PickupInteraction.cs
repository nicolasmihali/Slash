using UnityEngine;

public class PickupInteraction : MonoBehaviour, IInteractable
{
    private bool _pickedUp = false;
    public void Interact()
    {
        if (_pickedUp) { return; }

        _pickedUp = true;
        Destroy(gameObject);
        Debug.Log("Object picked up");
    }
}
