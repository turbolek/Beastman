using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private Animator _animator;
    [SerializeField] private Hitter _hitter;

    private GameInput _gameInput;
    private float _movementHorizontal;
    private float _movementVertical;
    private bool _isAttacking = false;


    private void Start()
    {
        _gameInput = new GameInput();
        _gameInput.Enable();
        _gameInput.PlayerControls.WalkHorizontal.performed += ctx =>
        {
            _movementHorizontal = ctx.ReadValue<float>();
        };

        _gameInput.PlayerControls.WalkHorizontal.canceled += ctx =>
        {
            _movementHorizontal = 0f;
        };

        _gameInput.PlayerControls.WalkVertical.performed += ctx =>
        {
            _movementVertical = ctx.ReadValue<float>();
        };

        _gameInput.PlayerControls.WalkVertical.canceled += ctx =>
        {
            _movementVertical = 0f;
        };

        _gameInput.PlayerControls.Punch.performed += ctx =>
        {
            Punch();
        };
    }

    private void Update()
    {
        if (!_isAttacking)
        {
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        if (_movementVertical == 0f && _movementHorizontal == 0f || _isAttacking)
        {
            _animator.SetBool("IsWalking", false);
            return;
        }
        _animator.SetBool("IsWalking", true);

        Vector3 verticalVector = _camera.transform.forward * _movementVertical;
        Vector3 verticalVectorFlat = new Vector3(verticalVector.x, 0f, verticalVector.z);

        Vector3 horizontalVector = _camera.transform.right * _movementHorizontal;
        Vector3 horizontalVectorFlat = new Vector3(horizontalVector.x, 0f, horizontalVector.z);

        Vector3 movementDirection = (verticalVectorFlat + horizontalVectorFlat).normalized;

        Vector3 targetPosition = transform.position + movementDirection;

        transform.LookAt(targetPosition, Vector3.up);

        transform.Translate(movementDirection * _speed * Time.deltaTime, Space.World);

    }

    public void HitRequest()
    {
        _hitter.Hit();
    }

    private void Punch()
    {
        _animator.SetTrigger("Punch");
        _isAttacking = true;
    }

    public void OnPunchEnd()
    {
        _isAttacking = false;
    }

}
