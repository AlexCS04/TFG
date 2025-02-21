using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backpack : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float cellSize;

    public Grid<InvItem> backpackContent; 
    private RectTransform canvasRect;

    private float peso;

    [SerializeField] private GameObject slotPrefab;


    private void Start(){
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject slot = Instantiate(slotPrefab, transform);
                slot.GetComponent<Slot>().valores(i, j, this);
            }
        }
        GetComponent<GridLayoutGroup>().cellSize = new Vector2(cellSize, cellSize);

        GetComponent<RectTransform>().sizeDelta = new Vector2(width, height) * cellSize;
        canvasRect= GetComponentInParent<Canvas>().rootCanvas.GetComponent<RectTransform>();
        backpackContent = new Grid<InvItem>(width, height, cellSize, GetComponent<RectTransform>().pivot);
        GetComponentInParent<AdditionalBP>().ReSize();


    }
    public Vector2 GetGridPos(Vector3 worldPos){
        backpackContent.GetXY(worldPos, out int x, out int y);
        return new Vector2(x,y);
    }
    public bool TryPutItem(InvItem invItem, Vector2Int gridPos){

        bool canFit=true;
        List<Vector2Int> invList = invItem.GetListPos(gridPos); //lista de posiciones del objeto en la grid

        foreach (Vector2Int pos in invList)
        {
            if (!backpackContent.ValidPosition(pos.x, pos.y))
            {
                canFit=false;
                Debug.Log(pos);
                Debug.Log("Invalid Pos");
                break;
            }
            if(backpackContent.GetGridObject(pos.x,pos.y)==null){}
            else if (backpackContent.GetGridObject(pos.x,pos.y).itemSO!=invItem.itemSO) {
                canFit=false;
                Debug.Log("Collide Diff Items");
                break;
            }
            else{
                // manejo cantidad
                Debug.Log("Collide Items");
                canFit=false;
                break;
            }
        }
        if(canFit){//añadirlo al grid
            bool first=true;
            foreach (Vector2Int pos in invList)
            {
                if(first){
                    invItem.SetGridPos(pos.x,pos.y);
                    first=false;
                }
                backpackContent.SetGridObject(pos.x,pos.y, invItem);

            }
            
            invItem.myBackpack=this;
            invItem.transform.parent=transform.parent;
            return true;
        }

        return false;
    }

    public void RemoveItemAt(Vector2Int XY){
        Debug.Log(XY);
        InvItem remItem=backpackContent.GetGridObject(XY.x, XY.y);
        List<Vector2Int> invList =remItem.GetListPos(XY);
        foreach (Vector2Int item in invList)
        {
            backpackContent.RemoveObjectAt(item.x,item.y);
        }
        //Debug.Log(XY);

    }
    public bool TwoItems(InvItem selected, InvItem reciving){
        if(selected.itemSO!=reciving.itemSO){return false;}//devolver selected
        else{return false;} //modificar cantidad
    }
    void Update()
    {

    }
}

