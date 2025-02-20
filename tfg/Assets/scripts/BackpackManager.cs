using UnityEngine;

public class BackpackManager : MonoBehaviour
{
    public static BackpackManager instance {get; private set;}

    public float cellSize;
    private InvItem invItem;

    public RectTransform rect;

    private Vector3 mouseDragAnchoredPositionOffset;

    void Awake()
    {
        instance=this;
        
    }
    void Update()
    {
        if(invItem!=null){

            RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, Input.mousePosition, null, out Vector2 targetPosition);
            targetPosition += new Vector2(-mouseDragAnchoredPositionOffset.x, -mouseDragAnchoredPositionOffset.y);
            //targetPosition.x = Mathf.Floor(targetPosition.x  / cellSize) * cellSize;
            //targetPosition.y = Mathf.Floor(targetPosition.y / cellSize) * cellSize;


            invItem.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(invItem.GetComponent<RectTransform>().anchoredPosition, targetPosition, Time.deltaTime * 20f);
            


        }   
    }

    public bool HasItem(){
        return invItem!=null;
    }
    public InvItem SelItem(){
        return invItem;
    }

    public void StartDrag(InvItem item){
        invItem=item;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, Input.mousePosition, null, out Vector2 anchoredPos);
        mouseDragAnchoredPositionOffset = anchoredPos - invItem.GetComponent<RectTransform>().anchoredPosition;

    }
    public void EndDrag(){
        invItem=null;


        





    }

}
