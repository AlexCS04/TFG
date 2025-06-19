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
        
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("GroundItem"))
        {
            player.itemsArea.Remove(collision.GetComponent<GroundItem>());
        }

    }
}
