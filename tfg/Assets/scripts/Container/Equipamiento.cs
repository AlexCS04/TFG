using UnityEngine;
using UnityEngine.UI;

public class Equipamiento : Container
{
    public int place;
    public Equipamiento()
    {
        width = 1;
        height = 1;
    }
    protected override void PutItem(Item item, Vector2Int gridPos, Dir dir)
    {
        item.dir = dir;
        item.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, -item.GetRotationAngle());
        item.gridPos = gridPos;
        contents.SetGridObject(0, 0, item);
        item.container = this;
        item.transform.SetParent(transform);
        item.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
        item.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
        item.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        item.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
        item.GetComponent<Image>().raycastTarget = true;
        ActualizarPeso(item.sct.peso * item.GetCantidad());
        Eventmanager.OnEquipPut(item, place);

    }
    public override void ActualizarPeso(float p)
    {
        ContainerManager.instance.mochila.GetComponent<Mochila>().ActualizarPeso(p / 2);

    }
    protected override bool SameEquipType(EquipType et)
    {
        return equipType == et;
    }

    protected override void ReSize()
    {
        transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = GetComponent<RectTransform>().sizeDelta;
    }
    public void Resize() { ReSize(); }
    protected override void TwoItems(Item selected, Item reciving)
    {
        base.TwoItems(selected, reciving);
        Eventmanager.OnEquipPut(reciving, place);
    }
}
