using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerDownHandler
{

    private int xPos;
    private int yPos;
    private Container myContainer;


    public void valores(int x, int y, Container c){
        xPos=x;
        yPos=y;
        myContainer = c;
    }

    public Vector2Int Location(){
        return new Vector2Int(xPos,yPos);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(ContainerManager.instance.HasItem()){
            // mybackpack.TryPutItem(BackpackManager.instance.SelItem(), Location());
            ContainerManager.instance.EndDrag(Location(), myContainer);
        }
    }
}