using System;
using UnityEngine;

public class Eventmanager
{
    public static event Action EnemyDeathEvent;

    public static event Action<Item, Vector2Int> PutItemEvent;
    public static event Action<Vector2Int> RemoveItemEvent;
    public static event Action<Item> PutEquipEvent;
    public static event Action<Item> RemoveEquipEvent;




    public static void OnEnemyDeath()
    {
        EnemyDeathEvent?.Invoke();
    }
    public static void OnItemPut(Item item, Vector2Int key)
    {
        PutItemEvent?.Invoke(item, key);
    }
    public static void OnItemRemoved(Vector2Int key)
    {
        RemoveItemEvent?.Invoke(key);
    }
    public static void OnEquipPut(Item item)
    {
        PutEquipEvent?.Invoke(item);
    }
    public static void OnEquipRemoved(Item item)
    {
        RemoveEquipEvent?.Invoke(item);
    }
}
