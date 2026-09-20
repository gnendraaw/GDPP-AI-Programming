using UnityEngine;
using UnityEngine.Events;

public class HighlightGhost : MonoBehaviour
{
    public UnityEvent OnSeeGhost;
    
    [SerializeField] private float _maxDistance = 10f;
    [SerializeField] private float _dotTreshold = 0.8f;
    [SerializeField] private bool _autoActive;

    private bool _isActive;

    private void Awake()
    {
        _isActive = _autoActive;
    }
    
    private void Update()
    {
        if (!_isActive) return;
        bool isSeeGhost = CheckIsPlayerSeeGhost();
        if (isSeeGhost)
        {
            OnSeeGhost?.Invoke();
            Destroy(this);
        }
    }
    
    public void SetActive(bool active)
    {
        _isActive = active;
    }

    private bool CheckIsPlayerSeeGhost()
    {
        Transform playerCamera = Camera.main.transform;
        Vector3 ghostDirection = (transform.position - playerCamera.position).normalized;
        
        float dotResult = Vector3.Dot(playerCamera.forward, ghostDirection);
        if (dotResult > _dotTreshold)
        {
            float distance = Vector3.Distance(transform.position, playerCamera.position);
            if (distance < _maxDistance)
            {
                return true;
            }
        }

        return false;
    }
}
