using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

public class MovableHeaderUI : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private Transform _target;

    private Vector2 _startPoint;
    private Vector2 _endPoint;

    private void Awake()
    {
        Assert.IsNull( _target );
        _target = transform.parent;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        _startPoint = _target.position;
        _endPoint = eventData.position;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        _target.position = _startPoint + (eventData.position - _endPoint);
    }
}