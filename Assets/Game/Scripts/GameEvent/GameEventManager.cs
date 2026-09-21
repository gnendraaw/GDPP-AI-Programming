using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    private static GameEventManager _instance;
    public static GameEventManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<GameEventManager>();
                if (_instance == null)
                {
                    var go = new GameObject($"[Auto generated] {nameof(GameEventManager)}");
                    _instance = go.AddComponent<GameEventManager>();
                }
            }
            return _instance;
        }
    }
    
    [SerializeField] private Dictionary<string, GameEventBase> _events = new();

    private void Awake()
    {
        InitializeSingleton();
    }

    private void InitializeSingleton()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }
    
    public void RegisterEvent(GameEventBase gameEvent)
    {
        if (_events.ContainsKey(gameEvent.ID)) return;
        _events.Add(gameEvent.ID, gameEvent);
    }
    
    public void UnregisterEvent(GameEventBase gameEvent)
    {
        if (!_events.ContainsKey(gameEvent.ID)) return;
        _events.Remove(gameEvent.ID);
    }

    public void TriggerEvent(string id)
    {
        if (!_events.TryGetValue(id, out GameEventBase gameEvent)) return;
        gameEvent.Trigger();
    }

    public void FinishEvent(string id)
    {
        if (!_events.TryGetValue(id, out GameEventBase gameEvent)) return;
        gameEvent.Finish();
    }
}
