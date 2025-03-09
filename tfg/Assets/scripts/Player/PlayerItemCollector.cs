using System.Collections.Generic;
using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    private PlayerControls player;
    void Start()
    {
        player=GetComponentInParent<PlayerControls>();   
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
