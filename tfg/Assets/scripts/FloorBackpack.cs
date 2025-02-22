using System.Collections.Generic;
using UnityEngine;

public class FloorBackpack: Backpack
{
    [SerializeField] private GameObject itemPrefab;
    void Start()
    {
        Debug.Log("A");
    }
    public void PutItems(List<ItemSO> listItems){
        DeleteI();
        foreach (ItemSO item in listItems)
        {
            GameObject invItem = Instantiate(itemPrefab);
            invItem.GetComponent<InvItem>().SetDir(Dir.Right);
            invItem.GetComponent<InvItem>().itemSO=item;
            TryAgain:;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    bool f = TryPutItem(invItem.GetComponent<InvItem>(), new Vector2Int(x,y), Dir.Right);
                    if(f){
                        goto Next;
                    }
                }
            }
            Bigger(item);
            goto TryAgain;
            Next:;
        }
    }
    public void Bigger(ItemSO item){
        // int tHeight=height;
        height+=item.sizeY;
        // Debug.Log(height);
        // Debug.Log(backpackContent.GetHeight());
        backpackContent.MoreRoomY(height);
        // Debug.Log(backpackContent.GetHeight());
        for (int i = transform.childCount-1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject slot = Instantiate(slotPrefab, transform);
                slot.GetComponent<Slot>().valores(x, y, this);
            }
        }
        GetComponent<RectTransform>().sizeDelta = new Vector2(width, height) * cellSize;
        if (GetComponentInParent<AdditionalBP>())
        {
            GetComponentInParent<AdditionalBP>().ReSize();
        }else{
            Debug.Log("How?");
        }
        


    }
    private void DeleteI(){
        for (int i = 1; i < transform.parent.childCount; i++)
        {
            DestroyImmediate(transform.parent.GetChild(i).gameObject);
        }
        backpackContent=new Grid<InvItem>(width, height, cellSize, GetComponent<RectTransform>().pivot);

    }
}
