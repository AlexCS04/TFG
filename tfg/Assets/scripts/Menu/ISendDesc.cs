using UnityEngine;
using UnityEngine.EventSystems;

public class ISendDesc : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected string desc;
    protected void OnDescription(string des)
    {
        Descriptor.instance.Description(des);
    }
    protected void OffDescription()
    {
        Descriptor.instance.Off();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnDescription(desc);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OffDescription();
    }
}
