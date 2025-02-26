using UnityEngine;
using UnityEngine.UIElements;

public class BackpackManager : MonoBehaviour
{
    public static BackpackManager instance {get; private set;}

    public float cellSize;
    [HideInInspector]public InvItem invItem;

    public GameObject groundItemPrefb;

    public Transform playerTransform;

    // public RectTransform rect;

    // private Vector3 mouseDragAnchoredPositionOffset;

    void Awake()
    {
        instance=this;
        
    }
    void Update()
    {
        if(invItem!=null){

            // RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, Input.mousePosition, null, out Vector2 targetPosition);
            // targetPosition += new Vector2(-mouseDragAnchoredPositionOffset.x, -mouseDragAnchoredPositionOffset.y);
            //targetPosition.x = Mathf.Floor(targetPosition.x  / cellSize) * cellSize;
            //targetPosition.y = Mathf.Floor(targetPosition.y / cellSize) * cellSize;
            Vector2 targetPosition=Input.mousePosition;

            // invItem.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(invItem.GetComponent<RectTransform>().anchoredPosition, targetPosition, Time.deltaTime * 20f);
            invItem.transform.position = Vector3.Lerp(invItem.transform.position, targetPosition, Time.deltaTime*20f);
            if (Input.GetKeyDown(KeyCode.R))
            {
                invItem.NextDir();
                invItem.GetComponent<RectTransform>().rotation= Quaternion.Euler(0,0,-invItem.GetRotationAngle());
            }

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
        SetCanvas(0.7f, false);
        invItem.transform.SetParent(transform.root);
        invItem.GetComponent<RectTransform>().anchorMax= new Vector2(0,1);
        invItem.GetComponent<RectTransform>().anchorMin=new Vector2(0,1);
        invItem.GetComponent<RectTransform>().sizeDelta=new Vector2(invItem.itemSO.sizeX*cellSize,invItem.itemSO.sizeY*cellSize);


        // RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, Input.mousePosition, null, out Vector2 anchoredPos);
        // mouseDragAnchoredPositionOffset = anchoredPos - invItem.GetComponent<RectTransform>().anchoredPosition;
        if(invItem.myBackpack!=null)
            invItem.myBackpack.RemoveItemAt(invItem.GetGridPos());
        

    }
    public void EndDrag(Vector2Int pos, Backpack b){

        SetCanvas(1f, true);
        if(b!=null){
            if(b.TryPutItem(invItem, pos, invItem.inictialDir)){
                // Debug.Log("posicionado");
                //correct posicion
            }
            else{
                // Debug.Log("NOOO");
                b.ReturnItem(invItem, invItem.inictialDir);
            }
        }else invItem.myBackpack.ReturnItem(invItem, invItem.inictialDir);


        invItem=null;

    }
    private void SetCanvas(float alpha, bool raycast)
    {
        invItem.canvasGroup.alpha = alpha;
        for (int i = 0; i < invItem.transform.childCount; i++)
        {
            invItem.transform.GetChild(i).GetComponent<CanvasGroup>().blocksRaycasts = raycast;   
        }
    }
    public void EndDrag(Equipment slot){
        SetCanvas(1f, true);
        if(slot.equipType.Equals(invItem.equipType)&&!slot.ocupado){
            slot.ocupado=true;
            invItem.SetDir(Dir.Right);
            invItem.equipmentSlot=slot;
            invItem.myBackpack=null;
            invItem.GetComponent<RectTransform>().rotation= Quaternion.Euler(0,0,-invItem.GetRotationAngle());
            invItem.transform.SetParent(slot.transform);
            invItem.GetComponent<RectTransform>().anchorMax= new Vector2(1,1);
            invItem.GetComponent<RectTransform>().anchorMin=new Vector2(0,0);
            invItem.GetComponent<RectTransform>().anchoredPosition=new Vector3(0,0,0);
            invItem.GetComponent<RectTransform>().sizeDelta=new Vector2(0,0);


        }
        else{
            invItem.myBackpack.ReturnItem(invItem,invItem.inictialDir);
            
        }
        invItem=null;
    }

}
