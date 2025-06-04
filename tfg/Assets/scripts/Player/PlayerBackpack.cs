using System.Collections.Generic;
using UnityEngine;

public class PlayerBackpack : MonoBehaviour
{
    [SerializeField] private Dictionary<Vector2Int, Item> contents = new Dictionary<Vector2Int, Item>();
    public void PutItem(Item item, Vector2Int key)
    {
        contents.Add(key, item);
    }
    public void RemoveItem(Vector2Int key)
    {
        contents.Remove(key);
    }
}