using UnityEngine;
using TMPro;

public class Descriptor : MonoBehaviour
{
    public static Descriptor instance { get; private set; }
    void Awake()
    {
        instance = this;
    }
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private GameObject descriptor;
    [SerializeField] private RectTransform textBox;
    [SerializeField] private float height;

    void Start()
    {
        AdjustVertical();
    }
    public void Description(string des)
    {
        descriptor.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        descriptor.SetActive(true);
        tmp.text = des;
        AdjustVertical();
    }
    public void Off()
    {
        descriptor.SetActive(false);
        tmp.text = "";
    }
    private void AdjustVertical()
    {
        string[] paragraphs = tmp.text.Split("\n");
        int paragraphCount = paragraphs.Length;     
        float newHeight = paragraphCount * height;
        Vector2 size = textBox.sizeDelta;
        size.y = newHeight;
        textBox.sizeDelta = size;

    }
}
