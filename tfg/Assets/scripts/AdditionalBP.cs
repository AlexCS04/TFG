using UnityEngine;

public class AdditionalBP : MonoBehaviour
{
    private RectTransform rect;
    private RectTransform sonRect;


    public void ReSize(){
        sonRect= transform.GetChild(0) as RectTransform;
        rect = GetComponent<RectTransform>();
        rect.sizeDelta=sonRect.sizeDelta;


    }
    void Start()
    {
        // ReSize();
    }
}
