using UnityEngine;

public class InvItemAssets : MonoBehaviour
{

    public static InvItemAssets instance{get; private set;}

    [SerializeField] private ItemSO[] listItemAsets;
    [SerializeField] private GameObject itemPrefab;

    void Awake()
    {
        instance=this;
    }

    public InvItem GetItemSO(string itemName, Dir dir){
        foreach (ItemSO itemTetrisSO in listItemAsets) {
            if (itemTetrisSO.name == itemName) {
                GameObject invItem = Instantiate(itemPrefab);
                invItem.GetComponent<InvItem>().SetDir(dir);
                invItem.GetComponent<InvItem>().itemSO=itemTetrisSO;
                return invItem.GetComponent<InvItem>();
            }
        }
        Debug.Log("Huh?");
        return null;
    }

}
