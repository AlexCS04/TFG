using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour, IPointerDownHandler
{
    public SCT sct;
    public Dir dir;
    public int lvl=1;
    public int stack=1;
    public Container container;
    public Vector2Int gridPos;
    public Vector2Int initialPos;
    public Dir initialDir;
    public GroundItem gItem;

    [SerializeField] private GameObject childPrefab;

    void Start()
    {
        SetUp();
    }
    public void SetUp(){
        for (int i = transform.childCount-1; i >=0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        GetComponent<Image>().sprite = sct.sprite;
        GetComponent<RectTransform>().anchorMax= new Vector2(0,1);
        GetComponent<RectTransform>().anchorMin=new Vector2(0,1);
        GetComponent<RectTransform>().sizeDelta=new Vector2(sct.sizeX*ContainerManager.instance.cellSize,sct.sizeY*ContainerManager.instance.cellSize);
        GetComponent<RectTransform>().localScale=new Vector3(1,1,1);
        GetComponent<RectTransform>().rotation= Quaternion.Euler(0,0,-GetRotationAngle());
        List<Vector2Int> list = GetListPos(new Vector2Int(0,0));
        foreach (Vector2Int item in list)
        {
            GameObject g = Instantiate(childPrefab,transform);
            g.GetComponent<RectTransform>().localPosition = new Vector3(item.x * ContainerManager.instance.cellSize, -item.y * ContainerManager.instance.cellSize, 0);
            g.GetComponent<RectTransform>().sizeDelta = new Vector2(ContainerManager.instance.cellSize, ContainerManager.instance.cellSize);
        }

    }


    public List<Vector2Int> GetListPos(Vector2Int offset){
        List<Vector2Int> gridPositionList = new List<Vector2Int>();
        switch (dir)
        {
            
            default:
            case Dir.Up:
                for (int x = 0; x < sct.sizeY; x++) {
                    for (int y = 0; y < sct.sizeX; y++) {
                        if (sct.shape.rows[y].array[x])
                            gridPositionList.Add(offset + new Vector2Int(x, -y));
                    }
                }
                break;
            
            case Dir.Right:
                for (int x = 0; x < sct.sizeX; x++) {
                    for (int y = 0; y < sct.sizeY; y++) {
                        if (sct.shape.rows[x].array[y]){
                            gridPositionList.Add(offset + new Vector2Int(x, y));
                            // Debug.Log(offset + new Vector2Int(x, y));
                            }
                    }
                }
                break;
            
            case Dir.Down:
                for (int x = 0; x < sct.sizeY; x++) {
                    for (int y = 0; y < sct.sizeX; y++) {
                        if (sct.shape.rows[y].array[x])
                            gridPositionList.Add(offset + new Vector2Int(-x, y));
                    }
                }
                break;
            
            case Dir.Left:
                for (int x = 0; x < sct.sizeX; x++) {
                    for (int y = 0; y < sct.sizeY; y++) {
                        if (sct.shape.rows[x].array[y])
                            gridPositionList.Add(offset + new Vector2Int(-x, -y));
                    }
                }

            break;
            
        }
        return gridPositionList;
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
        public int GetRotationAngle() {
        switch (dir) {
            default:
            case Dir.Right: return 0;
            case Dir.Down:  return 90;
            case Dir.Left:  return 180;
            case Dir.Up:    return 270;
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

    public bool IgualQueYo(Item item2){
        if(item2.sct==sct && item2.lvl==lvl){
            return true;
        }

        return false;
    }

    public int GetCantidad(){return stack;}
    public void AddCantidad(int s){stack+=s;}

    public void PickUp(){
        if(gItem!=null) Destroy(gItem.gameObject);
        ContainerManager.instance.StartDrag(this);
        
    }
    public void Drop(){
        ContainerManager.instance.EndDrag(gridPos,container);

    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button.Equals(PointerEventData.InputButton.Left))
        {
            if (!ContainerManager.instance.HasItem())
            {
                initialDir = dir;
                initialPos = gridPos;
                PickUp();
            }
            else
            {
                Drop();
            }
        }

    }

}
public enum Dir{
    Right,
    Down,
    Left,
    Up
}
