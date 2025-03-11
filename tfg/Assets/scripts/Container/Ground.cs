using UnityEngine;
using UnityEngine.EventSystems;

public class Ground : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        if(ContainerManager.instance.HasItem()){
            ContainerManager.instance.EndDrag(Vector2Int.zero, null);
        }
    }
}
