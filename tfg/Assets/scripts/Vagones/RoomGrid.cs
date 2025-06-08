using System.Collections.Generic;
using UnityEngine;
public class RoomGrid : MonoBehaviour
{

    [SerializeField] private GameObject gridPoint;
    public Dictionary<Vector2Int, RoomGridPoint> grid = new Dictionary<Vector2Int, RoomGridPoint>();
    const float BETWEEN = 0.5f;


    private int gridX;
    private int gridY;
    [SerializeField]private GameObject pathVisualPrefab;

    public void GenerateGrid()
    {
        gridX = 0;
        for (float i = 0; i < RoomManager.WAGON_WIDHT; i += BETWEEN)
        {
            gridY = 0;
            for (float j = 0; j < RoomManager.WAGON_HEIGHT ; j += BETWEEN)
            {
                GameObject p = Instantiate(gridPoint, transform);
                p.transform.localPosition = new Vector3(i, j, 0);
                grid.Add(new Vector2Int(gridX, gridY), p.GetComponent<RoomGridPoint>());
                gridY++;
            }
            gridX++;
        }
    }
}

public class PriorityQueue<T>
{
    private List<(T item, float priority)> elements = new List<(T, float)>();
    public int Count => elements.Count;
    public void Enqueue(T item, float priority)
    {
        elements.Add((item, priority));
    }
    public T Dequeue()
    {
        int bestIndex = 0;
        for (int i = 1; i < elements.Count; i++)
        {
            if (elements[i].priority < elements[bestIndex].priority)
            {
                bestIndex = i;
            }
        }
        T bestItem = elements[bestIndex].item;
        elements.RemoveAt(bestIndex);
        return bestItem;
    }
    // public bool Contains(T item)
    // {
    //     return elements.Exists(e => EqualityComparer<T>.Default.Equals(e.item, item));
    // }
    public void Clear()
    {
        elements.Clear();
    }
}
public struct AstarCell
{
    public readonly bool Explored { get { return prev_x >= 0 || prev_y >= 0; } }    // Hemos explorado ya esta celda?
    public int X;                                                                   // Posicion X de la celda
    public int Y;                                                                   // Posicion Y de la celda (OJO, en 3D esto sera la coordenada Z)
    public int prev_x;                                                              // X del paso anterior usado para llegar hasta esta celda
    public int prev_y;                                                              // Y del paso anterior usado para llegar hasta esta celda
    public readonly float F { get { return G + H; } }                               // G + H, "como de bueno es usar esta celda en nuestra ruta?"
    public float G;                                                                 // Coste acumulado de ruta, "cuantos pasos hemos de tomar para llegar aqui?"
    public float H;                                                                 // Distancia hasta el objetivo.
}

