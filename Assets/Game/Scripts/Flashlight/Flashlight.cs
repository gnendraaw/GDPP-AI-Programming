using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private Light _light;
    [SerializeField] private PlayerCharacter _owner;
    [SerializeField] private float _initialBatteryLevel = 100f;
    [SerializeField] private float _batteryDrainRate = 1f;

    private float _batteryLevel;

    public bool HasFlashlight => _owner.Inventory.CheckItem(_name);
    public bool HasBattery => _batteryLevel > 0f;

    private void Awake()
    {
        _batteryLevel = _initialBatteryLevel;
    }
    

    private void Update()
    {
        UpdateFlashlightRotation();
        UpdateBatteryLevel();
    }

    public void UseFlashlight()
    {
        if (_light == null) return;
        if (!HasFlashlight) return;
        if (!HasBattery) return;
        
        _light.enabled = !_light.enabled;
    }

    public void RefillBattery()
    {
        _batteryLevel = _initialBatteryLevel;
    }

    private void UpdateFlashlightRotation()
    {
        _light.transform.rotation = Camera.main.transform.rotation;
    }

    private void UpdateBatteryLevel()
    {
        if (!_light || !_light.enabled) return;

        if (HasBattery)
            _batteryLevel -= Time.deltaTime * _batteryDrainRate;
        else
        {
            _batteryLevel = 0f;
            _light.enabled = false;
        }
    }
}
