using UnityEngine;

public class MouseCapture : MonoBehaviour
{
    [SerializeField] InputReader _inputReader;
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private CursorLockMode currentLockMode = CursorLockMode.Locked;

    public Vector2 _mouseInput;

    void Start()
    {
        Cursor.lockState = currentLockMode;
    }

    void Update()
    {
        _mouseInput = _inputReader.LookValue * _mouseSensitivity;
    }
}
