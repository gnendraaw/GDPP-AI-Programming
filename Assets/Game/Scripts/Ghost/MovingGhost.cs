using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovingGhost : MonoBehaviour
{
    public UnityEvent OnStartMoving;
    public UnityEvent OnReachDestination;
    public UnityEvent OnReachedAllDestination;
    
    [SerializeField] private List<Vector3> _destinationPositions = new();
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _distanceToTolerance = 0.1f;
    [SerializeField] private bool _playOnAwake = true;
    [SerializeField] private bool _autoNextDestination = true;

    private int _destinationIndex = 0;
    private Coroutine _moveCoroutine;

    private void Start()
    {
        if (_playOnAwake)
            MoveToNextDestination();
    }

    public void MoveToNextDestination()
    {
        if (_destinationPositions.Count > 0 && _destinationPositions.Count > _destinationIndex)
        {
            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
                _moveCoroutine = null;
            }
            OnStartMoving?.Invoke();
            _moveCoroutine = StartCoroutine(MoveToTarget(_destinationPositions[_destinationIndex]));
            _destinationIndex++;
        }
        else
        {
            OnReachedAllDestination?.Invoke();
            Destroy(this);
        }
    }

    private IEnumerator MoveToTarget(Vector3 target)
    {
        RotateToDestination();
        
        while (Vector3.Distance(transform.position, target) > _distanceToTolerance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);
            yield return null;
        }

        transform.position = target;
        OnReachDestination?.Invoke();
        if (_autoNextDestination)
        {
            MoveToNextDestination();
        }
        else
        {
            if (_destinationIndex >= _destinationPositions.Count)
            {
                OnReachedAllDestination?.Invoke();
                Destroy(this);
            }
        }
    }

    public void RotateToDestination()
    {
        if (_destinationPositions.Count > 0 && _destinationPositions.Count > _destinationIndex)
        {
            transform.LookAt(_destinationPositions[_destinationIndex]);
        }
    }
}
