using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject _cameraPivot;
    [SerializeField] private GameObject _player;
    [SerializeField] private MouseCapture _mouseCapture;

    private float _pitch = 0;
    private float _yaw = 0;

    private void Start()
    {
        _cameraPivot.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    private void Update()
    {
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        _pitch -= _mouseCapture._mouseInput.y;
        _yaw += _mouseCapture._mouseInput.x;
        _pitch = Mathf.Clamp(_pitch, -89, 89);
        _player.transform.localRotation = Quaternion.Euler(0f, _yaw, 0f);
        _cameraPivot.transform.localRotation = Quaternion.Euler(_pitch, 0f, 0f);

    }
}
