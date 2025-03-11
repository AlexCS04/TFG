using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContainerManager : MonoBehaviour
{
    public static ContainerManager instance{get; private set;}

    public GameObject slotPrefab;

    public GameObject groundItemPrefab;
    public GameObject itemPrefab;

    public int cellSize;

    [HideInInspector]public Item itemMov;

    public GameObject player;
    public GameObject mochila;
    public GameObject floor;
    public List<Equipamiento> equipment;

    public GameObject inventario;

    void Awake()
    {
        instance=this;
    }
    public bool HasItem(){
        return itemMov!=null;
    }
    public void StartDrag(Item item){
        itemMov = item;
        ItemCanvasG(.7f,false);
        itemMov.transform.SetParent(transform.root);
        itemMov.GetComponent<RectTransform>().anchorMax= new Vector2(0,1);
        itemMov.GetComponent<RectTransform>().anchorMin=new Vector2(0,1);
        itemMov.GetComponent<RectTransform>().sizeDelta=new Vector2(itemMov.sct.sizeX*cellSize,itemMov.sct.sizeY*cellSize);
        itemMov.container.RemoveAt(itemMov.gridPos);

    }
    public void EndDrag(Vector2Int gPos, Container c){
        ItemCanvasG(1f,true);
        if(c is not Equipamiento && c != null){
            c.TryPutItem(itemMov, itemMov.dir, gPos, itemMov.GetListPos(gPos));
        }
        else if(c is Equipamiento){
            c.TryPutItem(itemMov, Dir.Right, gPos, new List<Vector2Int>{Vector2Int.zero});
        }
        else{
            floor.GetComponent<Suelo>().DropOnFloor(itemMov);
        }
        itemMov=null;
    }

    public void ItemCanvasG(float a, bool raycast){
        itemMov.GetComponent<CanvasGroup>().alpha=a;
        itemMov.GetComponent<Image>().raycastTarget=false;
        for (int i = 0; i < itemMov.transform.childCount; i++)
        {
            itemMov.transform.GetChild(i).GetComponent<CanvasGroup>().blocksRaycasts = raycast;   
        }
    }
    void Update()
    {
        if(itemMov!=null){
            Vector2 targetPosition=Input.mousePosition;

            // invItem.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(invItem.GetComponent<RectTransform>().anchoredPosition, targetPosition, Time.deltaTime * 20f);
            itemMov.transform.position = Vector3.Lerp(itemMov.transform.position, targetPosition, Time.deltaTime*20f);
            if (Input.GetKeyDown(KeyCode.R))
            {
                itemMov.NextDir();
                itemMov.GetComponent<RectTransform>().rotation= Quaternion.Euler(0,0,-itemMov.GetRotationAngle());
            }

        } 
    }
    public void OpenInventory(List<GroundItem> itemsArea){
        inventario.SetActive(true);
        floor.GetComponent<Suelo>().OpenFloor(itemsArea);
        StartCoroutine("Resizing");
    }
    public void CloseInventory(){
        inventario.SetActive(false);
    }
    private IEnumerator Resizing(){
        yield return null;
        foreach (Equipamiento item in equipment)
        {
            item.Resize();
        }
    }
}
