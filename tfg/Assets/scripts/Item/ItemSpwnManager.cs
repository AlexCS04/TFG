using System.Collections.Generic;
using UnityEngine;

public class ItemSpwnManager : MonoBehaviour
{
    public static ItemSpwnManager instance { get; private set; }
    [SerializeField] private GameObject groundIemPrefab;
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
        itemRandom=new System.Random(tSeed);
    }

    public void SpawnItem(SCT sct, Vector3 pos)
    {
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

}
