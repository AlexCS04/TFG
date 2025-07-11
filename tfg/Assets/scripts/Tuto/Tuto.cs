using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tuto : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI tutoText;
    [SerializeField]List<GameObject> equipment;
    [SerializeField]GameObject weapon;
    [SerializeField]List<GameObject> food;
    [SerializeField] SCT consumable;
    [SerializeField] SCT sword;
    [SerializeField] SCT money;
    [SerializeField] PlayerHealth playerH;
    [SerializeField] Color higlihter;
    bool invOpen;

    void OnEnable()
    {
        Eventmanager.OpenInvEvent += Open;
        Eventmanager.CloseInvEvent += Close;
    }
    void OnDisable()
    {
        Eventmanager.OpenInvEvent -= Open;
        Eventmanager.CloseInvEvent -= Close;

    }
    private void Open() {
        invOpen = true;
    }
    private void Close() {
        invOpen = false;
    }
    void Start()
    {
        tutoText.text = "";
        StartCoroutine("TutorialCor");
        
    }

    
    void Update()
    {

    }
    IEnumerator TutorialCor()
    {
        yield return null;
        playerH.currentHealth = 6;
        playerH.regenHealth = 8;
        playerH.TutoFood();
        playerH.ActHealthVisual();
        yield return null;
        yield return null;
        GameObject wagon = RoomManager.instance.wagonList[0];
        Destroy(wagon.transform.GetChild(2).gameObject);
        Destroy(wagon.transform.GetChild(1).gameObject);
        tutoText.text = "Move using the WASD keys";
        yield return new WaitForSeconds(4f);
        tutoText.text = "To open your inventory press the I key";
        yield return new WaitForSeconds(4f);
        tutoText.text = "Try oppening it near the item on the floor";
        yield return new WaitForSeconds(3f);
        while (!invOpen)
        {
            yield return null;
        }
        foreach (GameObject item in equipment)
        {
            item.transform.GetChild(0).GetComponentInChildren<Image>().color = higlihter;
        }
        tutoText.text = "This are your equipment slots";
        yield return new WaitForSeconds(3.4f);
        tutoText.text = "Form top to bottom: Helmet, Chestplate, Pants and Boots";
        yield return new WaitForSeconds(4.7f);
        foreach (GameObject item in equipment)
        {
            item.transform.GetChild(0).GetComponent<Image>().color = Color.white;
        }
        weapon.transform.GetChild(0).GetComponentInChildren<Image>().color = higlihter;
        tutoText.text = "This is your weapen slot";
        yield return new WaitForSeconds(3.4f);
        tutoText.text = "Click and drag the item to your weapon slot";
        weapon.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
        yield return new WaitForSeconds(4f);
        tutoText.text = "To attack press the mouse left button";
        yield return new WaitForSeconds(4f);
        ItemSpwnManager.instance.SpawnItem(sword, RoomManager.instance.player.position, 1);
        tutoText.text = "The melee weapons attack on the direction you are looking";
        yield return new WaitForSeconds(6f);
        tutoText.text = "To rotate an object, press the R key while holding it";
        yield return new WaitForSeconds(4f);
        tutoText.text = "Your food decreases overtime";
        yield return new WaitForSeconds(3f);
        ItemSpwnManager.instance.SpawnShopItem(consumable, RoomManager.instance.player.position);
        tutoText.text = "Try buying this strange fruit";
        yield return new WaitForSeconds(3f);
        ItemSpwnManager.instance.SpawnItem(money, RoomManager.instance.player.position, 69);
        tutoText.text = "Here's some money";
        yield return new WaitForSeconds(3f);
        tutoText.text = "To buy it press the interact key, F";
        yield return new WaitForSeconds(3f);
        tutoText.text = "Now equip it on your consumable slots";
        yield return new WaitForSeconds(.5f);
        while (!invOpen)
        {
            yield return null;
        }
        foreach (GameObject item in food)
        {
            item.transform.GetChild(0).GetComponentInChildren<Image>().color = higlihter;
        }
        tutoText.text = "This are your consumable slots";
        yield return new WaitForSeconds(4f);
        foreach (GameObject item in food)
        {
            item.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
        }
        tutoText.text = "Pressing 1 or 2 to use them";
        yield return new WaitForSeconds(3.4f);
        tutoText.text = "If you are well fed, you'll regen some health";
        yield return new WaitForSeconds(5f);
        tutoText.text = "The blue bar is your shield";
        yield return new WaitForSeconds(3.4f);
        tutoText.text = "You won't lose health until it gets depleated";
        yield return new WaitForSeconds(4f);
        tutoText.text = "Every time you enter a wagon it gets replenished";
        yield return new WaitForSeconds(4f);
        tutoText.text = "The last slots on your equipments are for rings";
        yield return new WaitForSeconds(4f);
        tutoText.text = "To exit the dungeon, interact with the top or bottom doors";
        yield return new WaitForSeconds(5f);
        tutoText.text = "Or press esc";



    }
}
