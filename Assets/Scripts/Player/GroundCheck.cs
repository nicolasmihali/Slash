using Unity.VisualScripting;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] CharacterController _player;
    [SerializeField] LayerMask layermask;

    Vector3 boxCenter;
    Vector3 halfExtents;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        boxCenter = _player.bounds.center;
        halfExtents = _player.bounds.extents;

        halfExtents.y = 1f;

        float maxDistance = _player.bounds.extents.y;

        
        if (Physics.SphereCast(_player.bounds.center, 0.5f, Vector3.down, out RaycastHit hitInfo, maxDistance, layermask))
        {
            Debug.Log("Grounded");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(boxCenter, 0.5f);
    }
}
