using TMPro;
using UnityEngine;

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
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            if (collision.GetComponent<PlayerBackpack>().money >= price)
            {
                aviso.SetActive(true);
                collision.GetComponent<PlayerBackpack>().shopItem = this;
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            aviso.SetActive(false);
            collision.GetComponent<PlayerBackpack>().shopItem = null;
        }
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
        return false;
    }
}
