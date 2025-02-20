using UnityEditor.ProjectWindowCallback;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Backpack : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float cellSize;

    public Grid<InvItem> backpackContent; 
    private RectTransform canvasRect;

    private float peso;

    [SerializeField] private GameObject slotPrefab;

    private void Awake(){
        
        
    }
    private void Start(){
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Instantiate(slotPrefab, transform);
            }
        }
        GetComponent<GridLayoutGroup>().cellSize = new Vector2(cellSize, cellSize);

        GetComponent<RectTransform>().sizeDelta = new Vector2(width, height) * cellSize;
        canvasRect= GetComponentInParent<Canvas>().rootCanvas.GetComponent<RectTransform>();
        backpackContent = new Grid<InvItem>(width, height, cellSize, GetComponent<RectTransform>().pivot);

        //GetComponent<RectTransform>().width = new Vector2(width, height) * cellSize;

    }
    public Vector2 GetGridPos(Vector3 worldPos){
        backpackContent.GetXY(worldPos, out int x, out int y);
        return new Vector2(x,y);
    }
    public bool TryPutItem(InvItem invItem, Vector2 gridPos){

        Vector2 XY = GetGridPos(gridPos);
        bool canFit=true;
        for (int i = 0; i < invItem.itemSO.sizeX; i++)
        {
            for (int j = 0; j < invItem.itemSO.sizeY; j++)
            {
                if(invItem.itemSO.shape.rows[i].array[j]){
                    if(backpackContent.GetGridObject((int)XY.x+i, (int)XY.y+j)==null){}
                    else if(backpackContent.GetGridObject((int)XY.x+i, (int)XY.y+j).itemSO!=invItem.itemSO){
                        canFit=false;
                        goto End;
                    }
                    else 
                    {
                        // manejo de cantidad
                    }

                }

            }
        }


        End:
        if(canFit){
            return true;
        }
        return false;
    }

    public void RemoveItemAt(Vector3 worldPos){
        Vector2 XY = GetGridPos(worldPos);
        InvItem remItem=backpackContent.GetGridObject((int)XY.x, (int)XY.y);
        Debug.Log(XY);

    }
    public void TwoItems(InvItem selected, InvItem reciving){
        if(selected.itemSO!=reciving.itemSO){return;}//devolver selected
        else{return;} //modificar cantidad
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, null, out Vector2 anchoredPos);
            Vector2 relativePos = new Vector2(anchoredPos.x / canvasRect.rect.width, anchoredPos.y / canvasRect.rect.height);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), anchoredPos, null, out Vector2 aa);
            Debug.Log("Sin GG: "+ relativePos);
            RemoveItemAt(aa);


        }
    }
}

