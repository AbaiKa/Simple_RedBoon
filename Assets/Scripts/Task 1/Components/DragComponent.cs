using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DragComponent : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public UnityEvent<PointerEventData> onBeginDrag = new UnityEvent<PointerEventData>();
    public UnityEvent<PointerEventData> onDrag = new UnityEvent<PointerEventData>();
    public UnityEvent<PointerEventData> onEndDrag = new UnityEvent<PointerEventData>();

    [SerializeField] private CanvasGroup canvasGroup;

    private Vector2 startPosition;
    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        onBeginDrag?.Invoke(eventData);

        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, Camera.main.nearClipPlane));
        newPosition.z = transform.position.z;
        transform.position = newPosition;
        onDrag?.Invoke(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = startPosition;
        onEndDrag?.Invoke(eventData);

        canvasGroup.blocksRaycasts = true;
    }
}
