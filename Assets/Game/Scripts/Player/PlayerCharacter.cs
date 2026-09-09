using System;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private PlayerCharacterMovement _movement;
    [SerializeField] private PlayerCharacterStamina _stamina;
    [SerializeField] private InventoryManager _inventory;
    [SerializeField] private CameraManager _camera;
    [SerializeField] private InteractDetector _interactDetector;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private Flashlight _flashlight;
    
    public PlayerCharacterMovement Movement => _movement;
    public PlayerCharacterStamina Stamina => _stamina;
    public InventoryManager Inventory => _inventory;
    public CameraManager Camera => _camera;
    public InteractDetector InteractDetector => _interactDetector;
    public  InputManager InputManager => _inputManager;
    public Flashlight Flashlight => _flashlight;
    
    public bool IsHiding { get; private set; }

    private void Awake()
    {
        // INFO: Hide & lock the cursor in game.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void SetIsHiding(bool value)
    {
        IsHiding = value;
    }
}
