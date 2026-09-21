using System.Collections;
using UnityEngine;

public class SlidingDoor : Door
{
    [SerializeField] private Vector3 openedDoorPosition;
    [SerializeField] private Vector3 closedDoorPosition;

    public override void Open()
    {
        base.Open();
        
        if (_animatingDoorCoroutine != null)
            StopCoroutine(_animatingDoorCoroutine);
        _animatingDoorCoroutine = StartCoroutine(SlideDoor(openedDoorPosition));
    }

    public override void Close()
    {
        base.Close();
        
        if (_animatingDoorCoroutine != null)
            StopCoroutine(_animatingDoorCoroutine);
        _animatingDoorCoroutine = StartCoroutine(SlideDoor(closedDoorPosition));
    }

    private IEnumerator SlideDoor(Vector3 targetPosition)
    {
        _isAnimating = true;

        Vector3 startPosition = _doorTransform.localPosition;
        float time = 0f;

        while (time < _duration)
        {
            time = time + Time.deltaTime;
            Vector3 position = Vector3.Lerp(startPosition, targetPosition, time / _duration);
            _doorTransform.localPosition = position;
            yield return null;
        }

        _doorTransform.localPosition = targetPosition;
        _isAnimating = false;
    }
}
