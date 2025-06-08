using System.Collections.Generic;
using UnityEngine;

public class RoomGridPoint : MonoBehaviour
{
    private List<GameObject> blockers = new List<GameObject>();

    public bool walkable => blockers.Count == 0;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Obstacle"))
        {
            blockers.Add(collision.gameObject);
        }
        else if (collision.tag.Equals("Enemies")) blockers.Add(collision.gameObject);
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Obstacle"))
        {
            blockers.Remove(collision.gameObject);
        }
        else if (collision.tag.Equals("Enemies")) blockers.Remove(collision.gameObject);
    }
}
