using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InvItem : MonoBehaviour, IPointerDownHandler
{
    
    public ItemSO itemSO;

    public int cantidad=1;
    private Dir dir;
    public Dir inictialDir;
    public CanvasGroup canvasGroup;
    public Backpack myBackpack;

    private Vector2Int gridPos;



    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();   
    }


    public Dir GetDir(){
        return dir;
    }
    public void NextDir(){
        switch (dir)
        {
            
            default:
                case Dir.Up:
                dir=Dir.Right;
                break;
                case Dir.Right:
                dir=Dir.Down;
                break;
                case Dir.Down:
                dir=Dir.Left;
                break;
                case Dir.Left:
                dir=Dir.Up;
                break;
        }
    }
    public List<Vector2Int> GetListPos(Vector2Int offset){
        List<Vector2Int> gridPositionList = new List<Vector2Int>();
        switch (dir)
        {
            
            default:
            case Dir.Up:
                for (int x = 0; x < itemSO.sizeY; x++) {
                    for (int y = 0; y < itemSO.sizeX; y++) {
                        if (itemSO.shape.rows[y].array[x])
                            gridPositionList.Add(offset + new Vector2Int(x, -y));
                    }
                }
                break;
            
            case Dir.Right:
                for (int x = 0; x < itemSO.sizeX; x++) {
                    for (int y = 0; y < itemSO.sizeY; y++) {
                        if (itemSO.shape.rows[x].array[y]){
                            gridPositionList.Add(offset + new Vector2Int(x, y));
                            // Debug.Log(offset + new Vector2Int(x, y));
                            }
                    }
                }
                break;
            
            case Dir.Down:
                for (int x = 0; x < itemSO.sizeY; x++) {
                    for (int y = 0; y < itemSO.sizeX; y++) {
                        if (itemSO.shape.rows[y].array[x])
                            gridPositionList.Add(offset + new Vector2Int(-x, y));
                    }
                }
                break;
            
            case Dir.Left:
                for (int x = 0; x < itemSO.sizeX; x++) {
                    for (int y = 0; y < itemSO.sizeY; y++) {
                        if (itemSO.shape.rows[x].array[y])
                            gridPositionList.Add(offset + new Vector2Int(-x, -y));
                    }
                }

            break;
            
        }
        return gridPositionList;
    }
    public int GetRotationAngle() {
        switch (dir) {
            default:
            case Dir.Down:  return 90;
            case Dir.Left:  return 180;
            case Dir.Up:    return 270;
            case Dir.Right: return 0;
        }
    }
    public Vector2Int GetRotationOffset() {
        switch (dir) {
            default:
            case Dir.Right:  return new Vector2Int(0, 0);
            case Dir.Up:  return new Vector2Int(0, 1);
            case Dir.Left:    return new Vector2Int(1, 1);
            case Dir.Down: return new Vector2Int(1, 0);
        }
    }
    public void SetDir(Dir d){
        dir=d;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(!BackpackManager.instance.HasItem()){
            inictialDir=dir;
            BackpackManager.instance.StartDrag(this);
        }
        else{
            //myBackpack.TwoItems(BackpackManager.instance.SelItem(), this);
            BackpackManager.instance.EndDrag(gridPos, myBackpack);
        }


    }
    // void OnMouseDown()
    // {
    //     Debug.Log("mouse");
    // }
    public void SetGridPos(int x, int y){
        gridPos=new Vector2Int(x,y);
    }
    public Vector2Int GetGridPos(){
        return gridPos;
    }
}
