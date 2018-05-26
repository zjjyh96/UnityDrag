using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // begin dragging
    public void OnBeginDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
    }

    // during dragging
    public void OnDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
    }

    // end dragging
    public void OnEndDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
    }

    /// <summary>
    /// set position of the dragged game object
    /// </summary>
    /// <param name="eventData"></param>
    private void SetDraggedPosition(PointerEventData eventData)
    {
        var rt = gameObject.GetComponent<RectTransform>();

        // transform the screen point to world point int rectangle
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            rt.position = globalMousePos;
        }
    }
}