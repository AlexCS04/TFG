using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSon : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button.Equals(PointerEventData.InputButton.Left))
        {

            Debug.Log("meeee");
            if (!BackpackManager.instance.HasItem())
            {
                transform.parent.GetComponent<InvItem>().inictialDir = transform.parent.GetComponent<InvItem>().GetDir();
                BackpackManager.instance.StartDrag(transform.parent.GetComponent<InvItem>());
            }
            else
            {
                //myBackpack.TwoItems(BackpackManager.instance.SelItem(), this);
                BackpackManager.instance.EndDrag(transform.parent.GetComponent<InvItem>().GetGridPos(), transform.parent.GetComponent<InvItem>().myBackpack);
            }
        }

    }
}