using System.Collections.Generic;
using UnityEngine;

public class Mochila : Container
{
    [SerializeField] private GameObject player;
    // protected override void GroundItem(Item item)
    // {
    //     player.GetComponent<PlayerBackpack>().PutItem(item, item.gridPos);
    // }
    private void RevisarPeso()
    {
        // if (peso >= .8f * maxPeso) player.GetComponent<PlayerControls>().currentSpeed = player.GetComponent<PlayerControls>().speed / 2;
        // else player.GetComponent<PlayerControls>().currentSpeed = player.GetComponent<PlayerControls>().speed;
        player.GetComponent<PlayerControls>().mochilaMaxPeso = maxPeso;
        player.GetComponent<PlayerControls>().mochilaPeso = peso;

    }

    public override void ActualizarPeso(float p)
    {
        base.ActualizarPeso(p);
        RevisarPeso();
    }
    public override void RemoveAt(Vector2Int gridPos)
    {
        player.GetComponent<PlayerBackpack>().RemoveItem(gridPos);
        Item remItem = contents.GetGridObject(gridPos.x, gridPos.y);
        if (remItem != null)
        {
            // peso-=remItem.GetPeso();
            List<Vector2Int> invList = remItem.GetListPos(gridPos);
            foreach (Vector2Int item in invList)
            {
                contents.RemoveObjectAt(item.x, item.y);
            }
            ActualizarPeso(-remItem.sct.peso * remItem.GetCantidad());

        }
    }
    protected override void PutItem(Item item, Vector2Int gridPos, Dir dir)
    {
        base.PutItem(item, gridPos, dir);
        Eventmanager.OnItemPut(item, gridPos);
    }
}
