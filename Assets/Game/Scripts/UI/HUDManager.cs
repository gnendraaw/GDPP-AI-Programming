using UnityEngine;

public class HUDManager : MonoBehaviour
{
    private static HUDManager _instance;
    public static HUDManager Instance => _instance;
    
    [SerializeField] private StaminaUI _staminaUI;
    [SerializeField] private BatteryLevelUI _batteryUI;
    [SerializeField] private InteractionInfoUI _interactionInfoUI;
    [SerializeField] private CrosshairUI _crosshairImage;
    
    public  StaminaUI StaminaUI => _staminaUI;
    public BatteryLevelUI BatteryUI => _batteryUI;
    public InteractionInfoUI InteractionInfoUI => _interactionInfoUI;
    public CrosshairUI CrosshairImage => _crosshairImage;
    
    public void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }
}
