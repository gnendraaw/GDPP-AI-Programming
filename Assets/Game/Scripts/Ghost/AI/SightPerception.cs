using System;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class SightPerception : MonoBehaviour
{
    [SerializeField] private Transform _eyePosition;
    [SerializeField] private Transform _target;
    [SerializeField] private float _viewDistance = 10f;
    [SerializeField] private float _viewAngle = 70f;
    [SerializeField] private LayerMask _targetLayer;
    
    public bool CanSeePlayer { get; private set; }
    public Vector3 LastSeenPosition { get; private set; }

    private void Update()
    {
        CanSeePlayer = CheckSight();
    }

    private bool CheckSight()
    {
        if (_target == null)
            return false;
        
        float distance = Vector3.Distance(_eyePosition.position, _target.position);
        if (distance > _viewDistance)
            return false;
        
        Vector3 directionToTarget = _target.position - _eyePosition.position;
        float angle = Vector3.Angle(_eyePosition.forward, directionToTarget);
        if (angle > _viewAngle * 0.5f)
            return false;

        bool isSeeingTarget = Physics.Raycast(
            _eyePosition.position,
            directionToTarget.normalized,
            out RaycastHit hit,
            _viewDistance,
            _targetLayer);
        if (!isSeeingTarget) return false;

        if (hit.transform == _target)
        {
            LastSeenPosition = _target.position;
            return true;
        }
        
        return false;
    }

    private void OnDrawGizmos()
    {
        if (_eyePosition == null) return;
        Gizmos.color = Color.red;

        bool isSeeingTarget = CheckSight();
        if (isSeeingTarget) Gizmos.color = Color.green;
        
        Gizmos.DrawWireSphere(_eyePosition.position, _viewDistance);

        Vector3 left = Quaternion.Euler(Vector3.up * -1f * _viewAngle * 0.5f) * _eyePosition.forward;
        Vector3 right = Quaternion.Euler(Vector3.up * _viewAngle * 0.5f) * _eyePosition.forward;
        
        Gizmos.DrawRay(_eyePosition.position, left * _viewDistance);
        Gizmos.DrawRay(_eyePosition.position, right * _viewDistance);
    }
}