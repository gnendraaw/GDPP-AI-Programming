using UnityEngine;

public class PlayerCharacterStamina : MonoBehaviour
{
    [SerializeField] private PlayerCharacterMovement _characterMovement;

    [Header("Settings")]
    [SerializeField] private float _maxStamina;
    [SerializeField] private float _staminaRegenValue;

    [Header("Cost")]
    [SerializeField] private float _sprintStaminaCost;

    private float _currentStamina;

    private void Awake()
    {
        _currentStamina = _maxStamina;
    }

    private void Update()
    {
        CalculateStamina();
    }

    private void CalculateStamina()
    {
        if (!_characterMovement.IsSprint)
            _currentStamina += Time.deltaTime * _staminaRegenValue;

        else
        {
            if (_currentStamina > 0f)
                _currentStamina -= Time.deltaTime * _sprintStaminaCost ;
            else
                _characterMovement.SetSprint(false);

        }

        _currentStamina = Mathf.Clamp(_currentStamina, 0f, _maxStamina);
    }
}
