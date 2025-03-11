using System.Collections.Generic;
using UnityEngine;

public class Suelo:Container
{
    private bool stack;
    protected override bool HayStack()
    {
        return stack;
    }
    protected override void GroundItem(Item item)
    {
        if(stack){
        GameObject gItem = Instantiate(ContainerManager.instance.groundItemPrefab,ContainerManager.instance.player.transform.position,Quaternion.identity);
        gItem.GetComponent<GroundItem>().sct=item.sct;
        gItem.GetComponent<GroundItem>().lvl=item.lvl;
        gItem.GetComponent<GroundItem>().stack=item.stack;
        item.gItem=gItem.GetComponent<GroundItem>();
        }
    }
    protected override void ReturnItem(Item item, Vector2Int gridPos, Dir dir)
    {
        if(stack)
            base.ReturnItem(item, gridPos, dir);
    }
    public void OpenFloor(List<GroundItem> listGItems){
        DeleteI();
        stack=false;
        foreach (GroundItem gItem in listGItems)
        {
            GameObject invItem = Instantiate(ContainerManager.instance.itemPrefab,transform.parent);
            invItem.GetComponent<Item>().dir=Dir.Right;
            invItem.GetComponent<Item>().sct=gItem.sct;
            invItem.GetComponent<Item>().gItem=gItem;
            invItem.GetComponent<Item>().stack=gItem.stack;
            invItem.GetComponent<Item>().lvl=gItem.lvl;
            invItem.GetComponent<Item>().SetUp();
            TryAgain:;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //Debug.Log(x+", "+y);
                    bool f = TryPutItem(invItem.GetComponent<Item>(),Dir.Right, new Vector2Int(x,y), invItem.GetComponent<Item>().GetListPos(new Vector2Int(x,y)));
                    if(f){
                        goto Next;
                    }
                }
            }
            Bigger(gItem.sct);
            goto TryAgain;
            Next:;
        }
        stack=true;

    }
    public void DropOnFloor(Item item){
        stack=false;
        GameObject gItem = Instantiate(ContainerManager.instance.groundItemPrefab,ContainerManager.instance.player.transform.position,Quaternion.identity);
        gItem.GetComponent<GroundItem>().sct=item.sct;
        gItem.GetComponent<GroundItem>().lvl=item.lvl;
        gItem.GetComponent<GroundItem>().stack=item.stack;
        item.gItem=gItem.GetComponent<GroundItem>();
        TryAgain:;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                //Debug.Log(x+", "+y);
                bool f = TryPutItem(item,item.dir, new Vector2Int(x,y), item.GetListPos(new Vector2Int(x,y)));
                if(f){
                    goto Next;
                }
            }
        }
        Bigger(item.sct);
        goto TryAgain;
        Next:;
        stack=true;
    }

    private void Bigger(SCT item){
        height+=item.sizeY;
        contents.MoreRoomY(height);
        for (int i = transform.childCount-1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject slot = Instantiate(ContainerManager.instance.slotPrefab, transform);
                slot.GetComponent<Slot>().valores(x, y, this);
            }
        }
        GetComponent<RectTransform>().sizeDelta = new Vector2(width, height) * ContainerManager.instance.cellSize;
        ReSize();

    }
}
