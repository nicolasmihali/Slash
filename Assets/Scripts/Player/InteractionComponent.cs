using UnityEngine;

public class InteractionComponent : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject _cameraPivot;
    [Header("Ray Settings")]
    [SerializeField] float _distance = 2.5f;
    [SerializeField] LayerMask _interactableMask;

    public IInteractable hoveredInteractable;

    void Update()
    {
        if (Physics.Raycast(_cameraPivot.transform.position, _cameraPivot.transform.forward, out RaycastHit hitInfo, _distance))
        {
            Debug.DrawRay(_cameraPivot.transform.position, _cameraPivot.transform.forward * hitInfo.distance, Color.blue);

            if (hitInfo.collider.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                hoveredInteractable = interactable;
            }
        }

        else
        {
            Debug.DrawRay(_cameraPivot.transform.position, _cameraPivot.transform.forward * _distance, Color.red);
            hoveredInteractable = null;
        }
    }
}
