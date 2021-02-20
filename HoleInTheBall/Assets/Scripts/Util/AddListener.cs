using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Listener", menuName = "Add Listener", order = 51)]
public class AddListener : ScriptableObject
{
    private event Action listeners;

    public void RegisterListener(Action listener)
    {
        listeners+=listener;        
    }

    public void UnregisterListener(Action listener)
    {
        listeners-=listener;        
    }

    public void Notify()
    {
        listeners?.Invoke();
    }
}
