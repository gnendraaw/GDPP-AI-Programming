using UnityEngine;

public class InteractDetector : MonoBehaviour
{
    [SerializeField] private PlayerCharacter _owner;
    [SerializeField] private float _detectorDistance;
    [SerializeField] private Vector3 _detectorBoxSize = Vector3.one;
    [SerializeField] private LayerMask _interactableLayer;

    private IInteractable _detectedInteractable;
    private bool _isInteracting;

    public bool IsEnabled { get; private set; } = true;

    private void Update()
    {
        UpdateDetection();
    }

    public void SetEnabled(bool value)
    {
        IsEnabled = value;
    }

    public void Interact()
    {
        if (!IsEnabled) return;
        if (_detectedInteractable == null) return;
        
        _detectedInteractable.Interact(_owner);
        _detectedInteractable = null;
        _isInteracting = true;
    }

    private void UpdateDetection()
    {
        if (_isInteracting)
        {
            _isInteracting = false;
            return;
        }

        Transform cameraTransform = Camera.main.transform;
        
        bool isDetectingInteractable = Physics.BoxCast(
            cameraTransform.position,
            _detectorBoxSize * 0.5f,
            cameraTransform.forward,
            out RaycastHit hit,
            Quaternion.identity,
            _detectorDistance,
            _interactableLayer);
        
        if (!isDetectingInteractable) return;
        
        IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();
        if (interactable == null) return;
        
        _detectedInteractable = interactable;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Transform cameraTransform = Camera.main.transform;

        bool isDetectingInteractable = Physics.BoxCast(
            cameraTransform.position,
            _detectorBoxSize * 0.5f,
            cameraTransform.forward,
            out RaycastHit hit,
            Quaternion.identity,
            _detectorDistance,
            _interactableLayer);

        if (isDetectingInteractable)
            Gizmos.color = Color.green;
        
        Gizmos.DrawLine(cameraTransform.position, cameraTransform.position + cameraTransform.forward * _detectorDistance);
        Gizmos.DrawWireCube(cameraTransform.position + cameraTransform.forward * _detectorDistance, _detectorBoxSize);
    }
}
