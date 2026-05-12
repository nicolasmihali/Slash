using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraEffects : MonoBehaviour
{
    [SerializeField] private bool _enableHeadbob;

    [SerializeField, Range(0, 0.1f)] private float _amplitude = 0.015f;
    [SerializeField, Range(0, 30)] private float _frequency = 10f;

    [SerializeField] private PlayerStateMachine _player;
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _cameraPivot;

    private float _toggleSpeed = 3.0f;
    private Vector3 _startPos;
    private PlayerStateMachine _controller;

    private void Awake()
    {
        _startPos = _camera.localPosition;
    }

    private void Update()
    {
        if (!_enableHeadbob) { return; }

        CheckMotion();
        _camera.LookAt(FocusTarget());
    }

    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * _frequency) * _amplitude;
        pos.x += Mathf.Cos(Time.time * _frequency / 2) * _amplitude * 2;
        return pos;
    }

    private void CheckMotion()
    {
        float speed = new Vector3(_player.Controller.velocity.x, 0, _player.Controller.velocity.z).magnitude;

        ResetPosition();

        if (speed < _toggleSpeed) { return; }
        if (!_player.IsGrounded()) { return; }

        PlayMotion(FootStepMotion());
    }

    private void ResetPosition()
    {
        if (_camera.localPosition == _startPos) { return; }
        _camera.localPosition = Vector3.Lerp(_camera.localPosition, _startPos, 1 * Time.deltaTime);
    }

    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + _cameraPivot.localPosition.y, transform.position.z);
        pos += _cameraPivot.forward * 15f;
        return pos;
    }

    private void PlayMotion(Vector3 motion)
    {
        _camera.localPosition += motion;
    }
    /*[SerializeField] private PlayerStateMachine _player;

    [SerializeField] private bool _enableTilt;
    [SerializeField] private bool _enableFallKick;
    [SerializeField] private bool _enableDamageKick;
    [SerializeField] private bool _enableHeadbob;

    [SerializeField] private float bobPitch;
    [SerializeField] private float bobRoll;
    [SerializeField] private float bobUp;
    [SerializeField] private float bobFrequency;

    private float _stepTimer;


    private void Update()
    {
        CalculateViewOffset(Time.deltaTime);
    }

    private void CalculateViewOffset(float deltaTime)
    {
        if (!_player) { return; }

        Vector3 velocity = _player.Controller.velocity;

        // Headbob Step Timer and Sin value.
        float speed = new Vector2(velocity.x, velocity.z).magnitude;
        if (speed > 0.1 && _player.IsGrounded())
        {
            _stepTimer += deltaTime * (speed / bobFrequency);
            _stepTimer = _stepTimer % 1.0f;
        }
        else
        {
            _stepTimer = 0f;
        }
        float bobSin = Mathf.Sin(_stepTimer * 2f * Mathf.PI) * 0.5f;

        Vector3 angles = Vector3.zero;
        Vector3 offset = Vector3.zero;

        // Headbob
        if (_enableHeadbob)
        {
            float pitchDelta = bobSin * bobPitch * speed;
            angles.x -= pitchDelta;

            float rollDelta = Mathf.Sin(_stepTimer * 2f * Mathf.PI) * bobRoll;
            angles.z -= rollDelta;

            float bobHeight = bobSin * bobUp * speed;
            offset.y += bobHeight;
        }

        transform.localPosition = offset;
        transform.localRotation = Quaternion.Euler(angles);

    }*/
}
