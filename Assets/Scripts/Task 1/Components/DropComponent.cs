using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DropComponent : MonoBehaviour, IDropHandler
{
    [field: SerializeField] public Transform Container {  get; private set; }
    public UnityEvent<PointerEventData> onDrop = new UnityEvent<PointerEventData> ();
    public void OnDrop(PointerEventData eventData)
    {
        onDrop?.Invoke(eventData);
    }
}
