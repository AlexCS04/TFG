using System.Collections.Generic;
using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    private PlayerControls player;
    private bool buying = false;
    private ShopItem shopItem;
    void Start()
    {
        player = GetComponentInParent<PlayerControls>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("GroundItem"))
        {
            player.itemsArea.Add(collision.GetComponent<GroundItem>());
        }
        if (collision.tag.Equals("ShopItem") && transform.parent.GetComponent<PlayerBackpack>().money >= collision.GetComponent<ShopItem>().price) { buying = true;  shopItem = collision.GetComponent<ShopItem>(); }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("GroundItem"))
        {
            player.itemsArea.Remove(collision.GetComponent<GroundItem>());
        }
        if (collision.tag.Equals("ShopItem"))
        {
            buying = false;
            if (collision.GetComponent<ShopItem>() == shopItem) shopItem = null;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)&&buying)
        {
            if (shopItem != null)
            {
                transform.parent.GetComponent<PlayerBackpack>().Purchase(shopItem.price);
                shopItem.Buy();
            }
        }
    }
}
