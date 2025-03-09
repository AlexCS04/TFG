using UnityEngine;
using UnityEngine.EventSystems;

public class ItemChild : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button.Equals(PointerEventData.InputButton.Left))
        {
            if (!ContainerManager.instance.HasItem())
            {
                transform.parent.GetComponent<Item>().initialDir = transform.parent.GetComponent<Item>().dir;
                transform.parent.GetComponent<Item>().initialPos = transform.parent.GetComponent<Item>().gridPos;
                transform.parent.GetComponent<Item>().PickUp();
            }
            else
            {
                transform.parent.GetComponent<Item>().Drop();
            }
        }

    }
}
