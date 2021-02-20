using UnityEngine;

[CreateAssetMenu(fileName = "Listener", menuName = "Add Listener", order = 51)]
public class AddListener : ScriptableObject
{
    public delegate void Execute();

    private event Execute listeners;

    public void RegisterListener(Execute listener)
    {
        listeners+=listener;        
    }

    public void UnregisterListener(Execute listener)
    {
        listeners-=listener;        
    }

    public void Notify()
    {
        listeners?.Invoke();
    }
}
