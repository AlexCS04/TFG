using System.Collections.Generic;
using UnityEngine;

public class ItemSpwnManager : MonoBehaviour
{
    public static ItemSpwnManager instance { get; private set; }
    [SerializeField] private GameObject groundIemPrefab;

    void Awake()
    {
        instance = this;
    }

    public void SpawnItem(SCT sct, Vector3 pos)
    {
        SpawnItem(sct, pos, RoomManager.instance.roomRandom.Next(sct.spwnQuantity.x, sct.spwnQuantity.y));
    }
    public void SpawnItem(SCT sct, Vector3 pos, int stack)
    {
        GameObject gItem = Instantiate(groundIemPrefab, pos, Quaternion.identity);
        gItem.GetComponent<GroundItem>().sct=sct;
        gItem.GetComponent<GroundItem>().lvl=RoomManager.instance.wagonCount;
        gItem.GetComponent<GroundItem>().stack=stack;

    }
    public void SpawnItem(List<SCT> sctList, Vector3 pos)
    {
        SpawnItem(sctList[RoomManager.instance.roomRandom.Next(0, sctList.Count)], pos);
    }
}
