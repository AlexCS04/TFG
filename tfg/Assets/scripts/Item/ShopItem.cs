using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour, IInteractable
{
    public SCT sct;
    public int lvl = 1;
    public int price;

    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private GameObject aviso;

    public void SetUp()
    {
        priceText.text = price.ToString();
        GetComponent<SpriteRenderer>().sprite = sct.sprite;
    }
    public void Buy()
    {
        ItemSpwnManager.instance.SpawnItem(sct, transform.position, 1);
        Destroy(gameObject);
    }

    public bool Interact()
    {
        int money = RoomManager.instance.player.GetComponent<PlayerBackpack>().money;
        if (money >= price) { RoomManager.instance.player.GetComponent<PlayerBackpack>().Purchase(price); Buy(); return true; }
        NoMoney();
        return false;
    }
    private void NoMoney()
    {
        GameObject l = Instantiate(aviso, transform);
        // l.transform.localPosition = Vector3.zero;
    }
}
