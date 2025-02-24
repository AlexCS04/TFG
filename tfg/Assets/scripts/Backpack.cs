using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backpack : MonoBehaviour
{
    [SerializeField] protected int width;
    [SerializeField] protected int height;
    [SerializeField] protected float cellSize;

    [SerializeField] private bool floor;

    protected bool stack=true;

    public bool isFloor(){return floor;}
    public Grid<InvItem> backpackContent; 
    private RectTransform canvasRect;

    private float peso;

    [SerializeField] protected GameObject slotPrefab;

    private string test;

    private void Start(){
        
        SetUp();

    }
    protected void SetUp(){
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
    public bool TryPutItem(InvItem invItem, Vector2Int gridPos, Dir iDir){

        bool canFit=true;
        bool tt= false;
        List<Vector2Int> invList = invItem.GetListPos(gridPos); //lista de posiciones del objeto en la grid

        foreach (Vector2Int pos in invList)
        {
            if (!backpackContent.ValidPosition(pos.x, pos.y))
            {
                canFit=false;
                // Debug.Log(pos);
                // Debug.Log("Invalid Pos");
                break;
            }
            if(backpackContent.GetGridObject(pos.x,pos.y)==null){
                // Debug.Log("Nada");
            }
            else if (backpackContent.GetGridObject(pos.x,pos.y).itemSO!=invItem.itemSO) {
                canFit=false;
                // Debug.Log("Collide Diff Items");
                break;
            }
            else{
                // manejo cantidad
                // Debug.Log("Collide Items");
                canFit=false;
                tt=TwoItems(invItem, backpackContent.GetGridObject(pos.x,pos.y));
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
            invItem.transform.SetParent(transform.parent);
            invItem.GetComponent<RectTransform>().anchoredPosition=  new Vector2((invItem.GetGridPos().x+invItem.GetRotationOffset().x)*cellSize,-(invItem.GetGridPos().y+invItem.GetRotationOffset().y)*cellSize);
            return true;
        }else{
            if (tt)
            {
                // ReturnItem(invItem, iDir); !tt
                return true;
               
            }
        }

        //devolver item
        return false;
    }

    public void RemoveItemAt(Vector2Int XY){
        
        InvItem remItem=backpackContent.GetGridObject(XY.x, XY.y);
        if (remItem!=null)
        {
            List<Vector2Int> invList =remItem.GetListPos(XY);
            foreach (Vector2Int item in invList)
            {
                backpackContent.RemoveObjectAt(item.x,item.y);
            }
        }


    }
    public void ReturnItem(InvItem invItem, Dir iDir){
        invItem.transform.SetParent(invItem.myBackpack.transform.parent);
        invItem.SetDir(iDir);
        invItem.GetComponent<RectTransform>().rotation= Quaternion.Euler(0,0,-invItem.GetRotationAngle());
        invItem.GetComponent<RectTransform>().anchoredPosition=  new Vector2((invItem.GetGridPos().x+invItem.GetRotationOffset().x)*cellSize,-(invItem.GetGridPos().y+invItem.GetRotationOffset().y)*cellSize);
        List<Vector2Int> invList= invItem.GetListPos(invItem.GetGridPos());
        foreach (Vector2Int pos in invList)
        {
            invItem.myBackpack.backpackContent.SetGridObject(pos.x,pos.y, invItem);
        }
    }
    public bool TwoItems(InvItem selected, InvItem reciving){
        if (stack)
        {
            if (selected.itemSO != reciving.itemSO) { return false; }//devolver selected
            else
            {//modificar cantidad

                int dar = reciving.itemSO.maxStack - (selected.cantidad + reciving.cantidad);
                if (dar <= 0) dar = reciving.itemSO.maxStack - reciving.cantidad;
                else dar = selected.cantidad;
                reciving.cantidad += dar;
                selected.cantidad -= dar;
                if (selected.cantidad <= 0)
                {
                    Destroy(selected.gameObject);
                    return true;
                }


                return false;
            }
        }
        else return false;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S)){
            test=SaveBContent();
            Debug.Log(test);
        }
        if(Input.GetKeyDown(KeyCode.L)){
            LoadBContent(test);
        }

    }
    public string SaveBContent(){
        List<InvItem> placedObjectList = new List<InvItem>();
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                if (backpackContent.GetGridObject(x, y)!=null) {
                    placedObjectList.Remove(backpackContent.GetGridObject(x, y));
                    placedObjectList.Add(backpackContent.GetGridObject(x, y));
                }
            }
        }

        List<SaveItem> addItemTetrisList = new List<SaveItem>();
        foreach (InvItem placedObject in placedObjectList) {
            addItemTetrisList.Add(new SaveItem {
                dir = placedObject.GetDir(),
                gridPosition = placedObject.GetGridPos(),
                itemTetrisSOName = placedObject.itemSO.name,
            });

        }
        Debug.Log(test);
        return JsonUtility.ToJson(new ListSaveItem { listSaveItems = addItemTetrisList });
    }
    public void LoadBContent(string json){
        ListSaveItem listAddItemTetris = JsonUtility.FromJson<ListSaveItem>(json);

        foreach (SaveItem addItemTetris in listAddItemTetris.listSaveItems) {
            TryPutItem(InvItemAssets.instance.GetItemSO(addItemTetris.itemTetrisSOName, addItemTetris.dir), addItemTetris.gridPosition, addItemTetris.dir);
        }
    }


    
}
[Serializable]
public struct SaveItem{

    public string itemTetrisSOName;
    public Vector2Int gridPosition;
    public Dir dir;
}
[Serializable]
public struct ListSaveItem{
    public List<SaveItem> listSaveItems;
}
