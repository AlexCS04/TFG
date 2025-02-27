using System.Collections.Generic;
using UnityEngine;

public class FloorBackpack: Backpack
{
    [SerializeField] private GameObject itemPrefab;
    void Start()
    {
        //Debug.Log("A");
        floor = true;
    }
    public void PutItems(List<GrounItemtest> listItems){
        DeleteI();
        stack= false; 
        foreach (GrounItemtest item in listItems)
        {
            GameObject invItem = Instantiate(itemPrefab, transform);
            invItem.GetComponent<InvItem>().SetDir(Dir.Right);
            invItem.GetComponent<InvItem>().itemSO=item.itemSO;
            invItem.GetComponent<InvItem>().groundItem=item;
            invItem.GetComponent<InvItem>().SetCantidad(item.GetCantidad());
            invItem.GetComponent<InvItem>().level=item.GetLevel();
            TryAgain:;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Debug.Log(x+", "+y);
                    bool f = TryPutItem(invItem.GetComponent<InvItem>(), new Vector2Int(x,y), Dir.Right);
                    if(f){
                        goto Next;
                    }
                }
            }
            Bigger(item.itemSO);
            goto TryAgain;
            Next:;
        }
        stack = true;
    }
    public void PutItem(InvItem item){
        GameObject invItem = Instantiate(itemPrefab,transform);
        invItem.GetComponent<InvItem>().SetDir(Dir.Right);
        invItem.GetComponent<InvItem>().itemSO=item.itemSO;
        // invItem.GetComponent<InvItem>().groundItem=item;
        invItem.GetComponent<InvItem>().SetCantidad(item.GetCantidad());
        invItem.GetComponent<InvItem>().level=item.level;
        TryAgain:;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // Debug.Log(x+", "+y);
                bool f = TryPutItem(invItem.GetComponent<InvItem>(), new Vector2Int(x,y), Dir.Right);
                if(f){
                    goto Next;
                }
            }
        }
        Bigger(item.itemSO);
        goto TryAgain;
        Next:;
    }
    public void Bigger(ItemSO item){
        height+=item.sizeY;
        backpackContent.MoreRoomY(height);
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

    void Update()
    {
        
    }

}
