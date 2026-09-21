using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachinePanTilt _panTilt;
    [SerializeField] private CinemachineInputAxisController _cameraInput;

    public float PanAxis => _panTilt.PanAxis.Value;

    public void SetCameraInputEnabled(bool value)
    {
        _cameraInput.enabled = value;
    }

    public void ResetCameraRotation()
    {
        _panTilt.PanAxis.Value = 0f;
        _panTilt.TiltAxis.Value = 0f;
    }

    public void SetPanAxisValue(float value)
    {
        _panTilt.PanAxis.Value = value;
    }
    
    public void SetTiltAxisValue(float value)
    {
        _panTilt.TiltAxis.Value = value;
    }
}
