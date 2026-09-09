using System.Collections;
using UnityEngine;

public class HidingCloset : MonoBehaviour, IInteractable
{
    [SerializeField] private string _name;
    [SerializeField] private Transform _hidePosition;
    [SerializeField] private Transform _unhidePosition;
    [SerializeField] private Door _door;
    [SerializeField] private float _duration = 1f;

    public string Name => _name;

    private PlayerCharacter _hidingPlayer;
    private Coroutine _hidingCoroutine;
    private Coroutine _unhideCoroutine;
    
    public void Interact(PlayerCharacter character)
    {
        if (_hidePosition == null) return;
        if (_unhidePosition == null) return;
        if (_door == null) return;

        _hidingPlayer = character;
        
        if (_hidingCoroutine != null)
            StopCoroutine(_hidingCoroutine);
        _hidingCoroutine = StartCoroutine(Hide());
    }

    public void StopHiding()
    {
        if (_hidingPlayer == null) return;
        
        if (_unhideCoroutine != null)
            StopCoroutine(_unhideCoroutine);
        StartCoroutine(Unhide());
    }

    public IEnumerator Hide()
    {
        _hidingPlayer.InputManager.OnInteractInput.AddListener(StopHiding);
        
        _hidingPlayer.SetIsHiding(true);
        _hidingPlayer.Camera.SetCameraInputEnabled(false);
        _hidingPlayer.Movement.SetEnabled(false);
        _hidingPlayer.InteractDetector.SetEnabled(false);
        
        _door.Open();
        yield return new WaitWhile(() => _door.IsAnimating);
        
        // INFO: Animating player entering the hiding closet.
        float time = 0f;
        Vector3 startPosition = _hidingPlayer.transform.position;
        float startRotation = _hidingPlayer.Camera.PanAxis;

        while (time < _duration)
        {
            time += Time.deltaTime;
            
            _hidingPlayer.transform.position = Vector3.Lerp(startPosition, _hidePosition.position, time / _duration);
            
            float panAxis = Mathf.Lerp(startRotation, _hidePosition.eulerAngles.y, time / _duration);
            _hidingPlayer.Camera.SetPanAxisValue(panAxis);

            yield return null;
        }

        _hidingPlayer.transform.position = _hidePosition.position;
        _hidingPlayer.transform.rotation = _hidePosition.rotation;
        
        _door.Close();
        yield return new WaitWhile(() => _door.IsAnimating);
    }

    public IEnumerator Unhide()
    {
        _hidingPlayer.InputManager.OnInteractInput.RemoveListener(StopHiding);
        
        _door.Open();
        yield return new WaitWhile(() => _door.IsAnimating);

        float time = 0f;
        Vector3 startPosition = _hidingPlayer.transform.position;
        float startRotation = _hidingPlayer.Camera.PanAxis;
        
        while (time < _duration)
        {
            time += Time.deltaTime;

            _hidingPlayer.transform.position = Vector3.Lerp(
                startPosition,
                _unhidePosition.position,
                time / _duration);

            float panAxis = Mathf.Lerp(startRotation,  _unhidePosition.eulerAngles.y, time / _duration);
            _hidingPlayer.Camera.SetPanAxisValue(panAxis);

            yield return null;
        }

        _hidingPlayer.transform.position = _unhidePosition.position;
        _hidingPlayer.transform.rotation = _unhidePosition.rotation;

        _door.Close();

        _hidingPlayer.SetIsHiding(false);
        _hidingPlayer.Camera.SetCameraInputEnabled(true);
        _hidingPlayer.Movement.SetEnabled(true);
        _hidingPlayer.InteractDetector.SetEnabled(true);
        _hidingPlayer = null;

        yield return new WaitWhile(() => _door.IsAnimating);
    }
}