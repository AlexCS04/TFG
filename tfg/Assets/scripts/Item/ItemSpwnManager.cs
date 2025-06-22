using System.Collections.Generic;
using UnityEngine;

public class ItemSpwnManager : MonoBehaviour
{
    public static ItemSpwnManager instance { get; private set; }
    [SerializeField] private GameObject groundIemPrefab;
    [SerializeField] private GameObject shopItemPrefab;
    public System.Random itemRandom;


    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        int tSeed;
        if (int.TryParse(RoomManager.instance.seed, out int n)) { tSeed = System.Int32.Parse(RoomManager.instance.seed); }
        else { tSeed = RoomManager.instance.seed.GetHashCode(); }
        itemRandom = new System.Random(tSeed);
    }

    public void SpawnItem(SCT sct, Vector3 pos)
    {
        if (sct == null) return;
        SpawnItem(sct, pos, itemRandom.Next(sct.spwnQuantity.x, sct.spwnQuantity.y));
    }
    public void SpawnItem(SCT sct, Vector3 pos, int stack)
    {
        if (sct != null)
        {
            GameObject gItem = Instantiate(groundIemPrefab, pos, Quaternion.identity, RoomManager.instance.wagonList[RoomManager.instance.actualWagon].transform);
            gItem.GetComponent<GroundItem>().sct = sct;
            gItem.GetComponent<GroundItem>().lvl = RoomManager.instance.wagonCount;
            gItem.GetComponent<GroundItem>().stack = stack;
        }

    }
    public void SpawnItem(List<SCT> sctList, Vector3 pos)
    {
        if (sctList.Count == 0) return;
        SpawnItem(sctList[itemRandom.Next(0, sctList.Count)], pos);
    }
    public void SpawnItem(List<SCT> sctList, Vector3 pos, System.Random r)
    {
        if (sctList.Count == 0) return;
        SpawnItem(sctList[r.Next(0, sctList.Count)], pos);
    }
    public void SpawnShopItem(List<SCT> sctList, Vector3 pos)
    {
        if (sctList.Count == 0) return;
        SpawnShopItem(sctList[RoomManager.instance.roomRandom.Next(0, sctList.Count)], pos);
    }
    public void SpawnShopItem(SCT sct, Vector3 pos)
    {
        if (sct != null)
        {
            GameObject sItem = Instantiate(shopItemPrefab, pos, Quaternion.identity, RoomManager.instance.wagonList[RoomManager.instance.actualWagon].transform);
            sItem.GetComponent<ShopItem>().sct = sct;
            sItem.GetComponent<ShopItem>().lvl = RoomManager.instance.wagonCount;
            sItem.GetComponent<ShopItem>().price = sct.price;
            sItem.GetComponent<ShopItem>().SetUp();
        }

    }

}
