using UnityEngine;

public class DropGameObjectEvent : GameEventBase
{
    [SerializeField] private Rigidbody _dropItemPhysics;

    public override void Trigger()
    {
        _dropItemPhysics.useGravity = true;
        base.Trigger();
    }
}
