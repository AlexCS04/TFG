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
            /*
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, Input.mousePosition, null, out Vector2 targetPosition);

            targetPosition += new Vector2(-mouseDragAnchoredPositionOffset.x, -mouseDragAnchoredPositionOffset.y);
            targetPosition /=  cellSize;
            targetPosition = new Vector2(Mathf.Floor(targetPosition.x), Mathf.Floor(targetPosition.y));
            targetPosition *= cellSize;


            invItem.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(invItem.GetComponent<RectTransform>().anchoredPosition, targetPosition, Time.deltaTime * 20f);
            invItem.transform.rotation = Quaternion.Lerp(invItem.transform.rotation, Quaternion.Euler(0, 0, -invItem.GetRotationAngle(invItem.GetDir())), Time.deltaTime * 15f);
            */

            RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, Input.mousePosition, null, out Vector2 targetPosition);
            targetPosition.x = Mathf.Floor(targetPosition.x  / cellSize) * cellSize;
            targetPosition.y = Mathf.Floor(targetPosition.y / cellSize) * cellSize;


            invItem.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(invItem.GetComponent<RectTransform>().anchoredPosition, targetPosition, Time.deltaTime * 20f);
        }   
    }

    public bool HasItem(){
        return invItem!=null;
    }

    public void StartDrag(InvItem item){
        invItem=item;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, Input.mousePosition, null, out Vector2 anchoredPos);
        mouseDragAnchoredPositionOffset = anchoredPos - invItem.GetComponent<RectTransform>().anchoredPosition;

    }

}
