using UnityEngine;
using UnityEngine.EventSystems;

public class InvItem : MonoBehaviour, IPointerDownHandler
{
    
    public ItemSO itemSO;
    private Dir dir;
    private CanvasGroup canvasGroup;
    public Backpack myBackpack;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();   
    }


    public Dir GetDir(){
        return dir;
    }
    public void NextDir(){
        switch (dir)
        {
            
            default:
                case Dir.Up:
                dir=Dir.Right;
                break;
                case Dir.Right:
                dir=Dir.Down;
                break;
                case Dir.Down:
                dir=Dir.Left;
                break;
                case Dir.Left:
                dir=Dir.Up;
                break;
        }
    }
    public int GetRotationAngle(Dir dir) {
        switch (dir) {
            default:
            case Dir.Down:  return 180;
            case Dir.Left:  return 270;
            case Dir.Up:    return 0;
            case Dir.Right: return 90;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!BackpackManager.instance.HasItem()){
            canvasGroup.alpha = .7f;
            canvasGroup.blocksRaycasts = false;
            BackpackManager.instance.StartDrag(this);
        }
        else{
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            myBackpack.TwoItems(BackpackManager.instance.SelItem(), this);
            BackpackManager.instance.EndDrag();
        }

            
    }
}
