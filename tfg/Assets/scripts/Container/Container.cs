using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Container : MonoBehaviour
{
    protected Grid<Item> contents;
    [SerializeField] protected int width;
    [SerializeField] protected int height;

    private RectTransform canvasRect;

    [SerializeField] protected float maxPeso;
    [SerializeField] protected float peso;

    [SerializeField] protected EquipType equipType;

    void Awake()
    {
        SetUp();
    }

    void SetUp()
    {
        int cellsize = ContainerManager.instance.cellSize;
        GameObject slotP = ContainerManager.instance.slotPrefab;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject slot = Instantiate(slotP, transform);
                slot.GetComponent<Slot>().valores(i, j, this);
            }
        }
        if (GetComponent<GridLayoutGroup>())
            GetComponent<GridLayoutGroup>().cellSize = new Vector2(cellsize, cellsize);
        GetComponent<RectTransform>().sizeDelta = new Vector2(width, height) * cellsize;
        canvasRect = GetComponentInParent<Canvas>().rootCanvas.GetComponent<RectTransform>();
        contents = new Grid<Item>(width, height, cellsize, GetComponent<RectTransform>().pivot);
        ReSize();
    }
    protected virtual void ReSize()
    {
        RectTransform rect = transform.parent.GetComponent<RectTransform>();
        rect.sizeDelta = GetComponent<RectTransform>().sizeDelta;
    }
    public virtual bool TryPutItem(Item item, Dir dir, Vector2Int gridPos, List<Vector2Int> invList)
    {

        // List<Vector2Int> invList = item.GetListPos(gridPos); //lista de posiciones del objeto en la grid
        bool canFit = true;
        bool cantidad = false;
        Item temp = null;
        foreach (Vector2Int pos in invList)
        {
            if (!contents.ValidPosition(pos.x, pos.y))
            {
                canFit = false;
                break;
            }
            if (!SameEquipType(item.sct.equipType))
            {
                canFit = false;
                break;
            }
            if (contents.GetGridObject(pos.x, pos.y) == null)
            {
                //nada
            }
            else if (!item.IgualQueYo(contents.GetGridObject(pos.x, pos.y)))
            {
                canFit = false;
                break;
            }
            else
            {
                //manego cantidad
                temp = contents.GetGridObject(pos.x, pos.y);
                cantidad = HayStack();
                canFit = false;
                break;
            }
        }
        if (cantidad)
        {
            TwoItems(item, temp);
        }
        else if (canFit)
        {
            PutItem(item, gridPos, dir);
            return true;
        }
        else
        {
            ReturnItem(item, item.initialPos, item.initialDir);
            return false;
        }

        return false;
    }
    protected virtual void ReturnItem(Item item, Vector2Int gridPos, Dir dir)
    {
        item.container.PutItem(item, item.initialPos, item.initialDir);
    }
    protected virtual bool SameEquipType(EquipType et)
    {
        return true;
    }

    protected virtual void PutItem(Item item, Vector2Int gridPos, Dir dir)
    {
        bool first = true;
        int cellSize = ContainerManager.instance.cellSize;
        item.dir = dir;
        item.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, -item.GetRotationAngle());
        List<Vector2Int> invList = item.GetListPos(gridPos);
        foreach (Vector2Int pos in invList)
        {
            if (first)
            {
                item.gridPos = new Vector2Int(pos.x, pos.y);
                first = false;
            }
            contents.SetGridObject(pos.x, pos.y, item);
        }
        item.container = this;
        item.transform.SetParent(transform.parent);
        item.GetComponent<RectTransform>().anchoredPosition = new Vector2((item.gridPos.x + item.GetRotationOffset().x) * cellSize, -(item.gridPos.y + item.GetRotationOffset().y) * cellSize);
        GroundItem(item);
        ActualizarPeso(item.sct.peso * item.GetCantidad());

    }
    public virtual void RemoveAt(Vector2Int gridPos)
    {
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
    protected virtual bool HayStack()
    {
        return true;
    }
    protected virtual void TwoItems(Item selected, Item reciving)
    {
        //modificar cantidad
        int dar = reciving.sct.maxStack - (selected.GetCantidad() + reciving.GetCantidad());
        if (dar <= 0) dar = reciving.sct.maxStack - reciving.GetCantidad();
        else dar = selected.GetCantidad();
        reciving.AddCantidad(dar);
        ActualizarPeso(dar * reciving.sct.peso);
        selected.AddCantidad(-dar);
        if (reciving.gItem != null) reciving.gItem.stack += dar;
        selected.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = selected.GetCantidad().ToString();
        reciving.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = reciving.GetCantidad().ToString();
        if (selected.GetCantidad() <= 0)
        {
            if (selected.gItem != null) Destroy(selected.gItem.gameObject);
            Destroy(selected.gameObject);
            return;
        }
        selected.container.PutItem(selected, selected.initialPos, selected.initialDir);
    }
    protected virtual void GroundItem(Item item) { }
    public virtual void ActualizarPeso(float p)
    {
        peso += p;

    }
    protected void DeleteI()
    {
        for (int i = 1; i < transform.parent.childCount; i++)
        {
            Destroy(transform.parent.GetChild(i).gameObject);
        }
        contents = new Grid<Item>(width, height, ContainerManager.instance.cellSize, GetComponent<RectTransform>().pivot);

    }
    void Update()
    {

    }
}
