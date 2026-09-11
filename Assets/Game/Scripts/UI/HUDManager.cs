using UnityEngine;

public class HUDManager : MonoBehaviour
{
    private static HUDManager _instance;
    public static HUDManager Instance => _instance;
    
    [SerializeField] private StaminaUI _staminaUI;
    public  StaminaUI StaminaUI => _staminaUI;

    public void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }
}
