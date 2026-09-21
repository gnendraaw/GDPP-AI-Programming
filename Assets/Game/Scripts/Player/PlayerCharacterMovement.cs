using UnityEngine;

public class PlayerCharacterMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _walkSpeed = 1f;
    [SerializeField] private float _sprintSpeed = 2f;
    [SerializeField] private float _acceleration = 0.5f;

    [Header("Ground Check")]
    [SerializeField] private LayerMask _groundLayer;

    public bool IsEnabled { get; private set; } = true;
    public bool IsSprint => _isSprint;

    private const float DEFAULT_Y_MOVE_DIRECTION = 0f;
    private const float GROUND_CHECK_RADIUS = 0.5f;
    private const float RESET_VELOCITY_Y_VALUE = -2f;

    private float _gravityScale = 1f;
    private float _velocityY;

    private float _currentSpeed = 1f;
    private Vector3 _moveDirection = Vector3.zero;
    private Vector3 _velocityXZ = Vector3.zero;

    private bool _isSprint;

    private bool _isGrounded;

    private void Update()
    {
        CheckIsGrounded();
        ResetVelocityY();
        CalculateAcceleration();
        Move();
    }

    public void SetEnabled(bool value)
    {
        IsEnabled = value;
    }

    public void SetMoveDirection(Vector2 inputDirection)
    {
        _moveDirection = new Vector3(inputDirection.x, DEFAULT_Y_MOVE_DIRECTION, inputDirection.y);
    }

    public void SetSprint(bool value)
    {
        _isSprint = value;
        if (_isSprint)
            HUDManager.Instance.StaminaUI.SetVisible(true);
    }

    private void CheckIsGrounded()
    {
        _isGrounded = Physics.CheckSphere(transform.position, GROUND_CHECK_RADIUS, _groundLayer);
    }

    private void ResetVelocityY()
    {
        _velocityY = _isGrounded && _velocityY < 0 ? RESET_VELOCITY_Y_VALUE : _velocityY;
    }

    private void Move()
    {
        if (!IsEnabled) return;
        
        CalculateVelocityXZ();
        CalculateVelocityY();

        Vector3 velocity = new Vector3(_velocityXZ.x, _velocityY, _velocityXZ.z);
        _characterController.Move(velocity);
    }

    private void CalculateVelocityXZ()
    {
        Transform cameraTransform = Camera.main.transform;

        Vector3 directionX = cameraTransform.right * _moveDirection.x;
        Vector3 directionZ = cameraTransform.forward * _moveDirection.z;
        Vector3 direction = directionX + directionZ;

        direction.y = DEFAULT_Y_MOVE_DIRECTION;

        _velocityXZ = Time.deltaTime * _currentSpeed * direction.normalized;
        _velocityXZ *= _moveDirection.magnitude > 0.01f ? 1 : 0;
    }

    private void CalculateVelocityY()
    {
        _velocityY = Time.deltaTime * _gravityScale * Physics.gravity.y + _velocityY;
    }

    private void CalculateAcceleration()
    {
        if (_moveDirection.magnitude < 0.1f)
            _currentSpeed = 0;
        else
        {
            _currentSpeed += Time.deltaTime * _acceleration * (_isSprint ? 1f : -1f);
            _currentSpeed = Mathf.Clamp(_currentSpeed, _walkSpeed, _sprintSpeed);
        }

    }
}
