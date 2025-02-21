using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerDownHandler
{

    private int xPos;
    private int yPos;
    private Backpack mybackpack;


    public void valores(int x, int y, Backpack b){
        xPos=x;
        yPos=y;
        mybackpack = b;
    }

    public Vector2Int Location(){
        return new Vector2Int(xPos,yPos);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(BackpackManager.instance.HasItem()){
            // mybackpack.TryPutItem(BackpackManager.instance.SelItem(), Location());
            BackpackManager.instance.EndDrag(Location(), mybackpack);
        }
    }
}
