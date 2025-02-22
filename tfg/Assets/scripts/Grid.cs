using UnityEngine;
using System;

public class Grid<TGridObject>
{
    private int width;
    private int height;
    private float cellSize;
    private Vector3 origin;
    private TGridObject[,] gridArray;

    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
    public class OnGridObjectChangedEventArgs : EventArgs {
        public int x;
        public int y;
    }

    public Grid(int width, int height, float cellSize, Vector3 origin){ 
        this.width=width;
        this.height=height;
        this.cellSize=cellSize;
        this.origin=origin;

        gridArray = new TGridObject[width,height];

    }

    public void GetXY(Vector3 worldPosition, out int x, out int y) { // transforma las coordenadas globales a posiciones en el grid
        x = Mathf.FloorToInt((worldPosition - origin).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - origin).y / cellSize);
    }

    public TGridObject GetGridObject(int x, int y) {  // objeto en x,y del grid
        if (x >= 0 && y >= 0 && x < width && y < height) {
            return gridArray[x, y];
        } else {
            return default;
        }
    }
    public TGridObject GetGridObject(Vector3 worldPosition) { // objeto en x,y del grid usando coordenadas globales
        int x, y;
        GetXY(worldPosition, out x, out y); //coordenadas globales a coordenadas del grid
        return GetGridObject(x, y);
    }

    public void SetGridObject(int x, int y, TGridObject value){
        if (x >= 0 && y >= 0 && x < width && y < height) {
            gridArray[x,y] = value;
            TriggerGridObjectChanged(x,y);
        }

    }
    public void RemoveObjectAt(int x, int y){
        SetGridObject(x,y,default);
    }

    public void SetGridObject(Vector3 worldPosition, TGridObject value) {
        GetXY(worldPosition, out int x, out int y);
        SetGridObject(x, y, value);
    }

    public void TriggerGridObjectChanged(int x, int y) {
        OnGridObjectChanged?.Invoke(this, new OnGridObjectChangedEventArgs { x = x, y = y });
    }
    public bool ValidPosition(int x, int y){
        if (x >= 0 && y >= 0 && x < width && y < height) {
           return true;
        }   
        return false;
    }
}
