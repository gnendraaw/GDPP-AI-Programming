using UnityEngine;

public class ShowGhostEvent : GameEventBase
{
    [SerializeField] private GameObject _ghost;
    [SerializeField] private bool _destroyWhenFinished;

    public override void Trigger()
    {
        if (!_ghost) return;
        _ghost.SetActive(true);
        base.Trigger();
    }
    
    public override void Finish()
    {
        if (_destroyWhenFinished) Destroy(_ghost);
        base.Finish();
    }
}