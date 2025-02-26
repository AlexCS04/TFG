using System.Collections.Generic;
using UnityEngine;

public class tempPlayerItemArea : MonoBehaviour
{
    
    [SerializeField] private GameObject inv;
    [SerializeField] private FloorBackpack floor;
    [SerializeField] private List<GrounItemtest> itemsArea;
    [SerializeField] private ItemSO teset;

    void Start()
    {
        
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I)){
            OpenInv();
        }
        if(Input.GetKeyDown(KeyCode.H)){
            floor.Bigger(teset);
        }

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("GroundItem"))
        {
            itemsArea.Add(collision.GetComponent<GrounItemtest>());
        }   
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("GroundItem"))
        {
            itemsArea.Remove(collision.GetComponent<GrounItemtest>());
        }  
    }
    private void OpenInv(){
        if (inv.activeSelf)
        {
            inv.SetActive(false);
        }else
        {
            inv.SetActive(true);
            floor.PutItems(itemsArea);
        }
        
    }
}
