using UnityEngine;

public class DoorInteraction : MonoBehaviour, IInteractable
{
    private Animator _animator;

    private bool _isInAnimation;
    private bool _isOpened = false;

    void Start()
    {
        _animator = GetComponentInParent<Animator>();
    }

    public void Interact()
    {
        if (_isInAnimation) { return; }

        _isInAnimation = true;

        if (!_isOpened)
        {
            _animator.Play("DoorOpen", 0, 0.0f);
            _isOpened = true;
        }

        else if (_isOpened)
        {
            _animator.Play("DoorClose", 0, 0.0f);
            _isOpened = false;
        }
    }

    private void Update()
    {
        AnimatorStateInfo info = _animator.GetCurrentAnimatorStateInfo(0);

        if (info.normalizedTime >= 1f & (info.IsName("DoorOpen") || info.IsName("DoorClose")))
        {
            Debug.Log("Finished anim");
            _isInAnimation = false;
        }
    }
}
