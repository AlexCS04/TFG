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
        GameObject gItem = Instantiate(groundIemPrefab, pos, Quaternion.identity);
        gItem.GetComponent<GroundItem>().sct=sct;
        gItem.GetComponent<GroundItem>().lvl=RoomManager.instance.wagonCount;
        gItem.GetComponent<GroundItem>().stack=Random.Range(sct.spwnQuantity.x, sct.spwnQuantity.y);

    }
    public void SpawnItem(List<SCT> sctList, Vector3 pos)
    {
        SpawnItem(sctList[Random.Range(0, sctList.Count)], pos);
    }
}
