using System.Collections;
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
    private Coroutine _stopRegenStaminaCoroutine;
    private bool _isWaitingStaminaRegen;

    private void Awake()
    {
        _currentStamina = _maxStamina;
    }
    
    private void Start()
    {
        HUDManager.Instance.StaminaUI.SetStaminaFill(_currentStamina, _maxStamina);
    }

    private void Update()
    {
        CalculateStamina();
    }

    private void CalculateStamina()
    {
        // INFO: Character is not sprinting.
        if (!_characterMovement.IsSprint)
        {
            if (_currentStamina < _maxStamina)
                _currentStamina += Time.deltaTime * _staminaRegenValue;
            
            // INFO: Stamina is fully regenerated.
            else if (!_isWaitingStaminaRegen)
            {
                _stopRegenStaminaCoroutine = StartCoroutine(StopRegenStaminaWait());
                _isWaitingStaminaRegen = true;
            }
        }
        // INFO: Character is sprinting.
        else
        {
            if (_stopRegenStaminaCoroutine != null)
            {
                StopCoroutine(_stopRegenStaminaCoroutine);
                _stopRegenStaminaCoroutine = null;
            }

            _isWaitingStaminaRegen = false;
            
            if (_currentStamina > 0f)
            {
                _currentStamina -= Time.deltaTime * _sprintStaminaCost ;
            }
            else
                _characterMovement.SetSprint(false);
        }

        _currentStamina = Mathf.Clamp(_currentStamina, 0f, _maxStamina);
        HUDManager.Instance.StaminaUI.SetStaminaFill(_currentStamina, _maxStamina);
    }

    private IEnumerator StopRegenStaminaWait()
    {
        yield return new WaitForSeconds(1f);
        HUDManager.Instance.StaminaUI.SetVisible(false);
    }
}
