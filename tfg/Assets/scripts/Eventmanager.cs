using System;
using UnityEngine;

public class Eventmanager
{
    public static event Action EnemyDeathEvent;

    public static event Action<Item, Vector2Int> PutItemEvent;
    public static event Action<Vector2Int> RemoveItemEvent;
    public static event Action<Item, int> PutEquipEvent;
    public static event Action<Item, int> RemoveEquipEvent;
    public static event Action OpenInvEvent;
    public static event Action CloseInvEvent;





    public static void OnEnemyDeath()
    {
        EnemyDeathEvent?.Invoke();
    }
    public static void OnOpenInv()
    {
        OpenInvEvent?.Invoke();
    }
    public static void OnCloseInv()
    {
        CloseInvEvent?.Invoke();
    }
    public static void OnItemPut(Item item, Vector2Int key)
    {
        PutItemEvent?.Invoke(item, key);
    }
    public static void OnItemRemoved(Vector2Int key)
    {
        RemoveItemEvent?.Invoke(key);
    }
    public static void OnEquipPut(Item item, int place)
    {
        PutEquipEvent?.Invoke(item, place);
    }
    public static void OnEquipRemoved(Item item, int place)
    {
        RemoveEquipEvent?.Invoke(item, place);
    }

}
