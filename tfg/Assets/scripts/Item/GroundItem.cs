using UnityEngine;

public class GroundItem : MonoBehaviour
{
    public SCT sct;
    public int stack = 1;
    public int lvl = 1;
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sct.sprite;
    }
}
