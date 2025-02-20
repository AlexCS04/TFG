using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Backpack : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float cellSize;

    public Grid<ItemSO> backpackContent; 

    [SerializeField] private GameObject slotPrefab;

    private void Awake(){
        backpackContent = new Grid<ItemSO>(width, height, cellSize, transform.position);

        
    }
    private void Start(){
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Instantiate(slotPrefab, transform);
            }
        }
        GetComponent<GridLayoutGroup>().cellSize = new Vector2(cellSize, cellSize);

        GetComponent<RectTransform>().sizeDelta = new Vector2(width, height) * cellSize;

        //GetComponent<RectTransform>().width = new Vector2(width, height) * cellSize;

    }
    public Vector2 GetGridPos(Vector3 worldPos){
        backpackContent.GetXY(worldPos, out int x, out int y);
        return new Vector2(x,y);
    }
    

    

}
