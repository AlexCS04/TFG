using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 m_Movement;
    private bool attacking;
    [SerializeField] private float speed;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float healindSpeed;

    private bool openInv;
    public float regenHealth;
    public float trueHealth;

    public List<GroundItem> itemsArea;


    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if(!openInv){ //inventario cerrado
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");


            m_Movement.Set(horizontal, vertical);
            // m_Movement.Normalize();

            if(Input.GetAxisRaw("Fire1")!=0){
                StartCoroutine("Attack");
            }

        }
        if(Input.GetKeyDown(KeyCode.I)){
            OpenInv();
        }

    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + m_Movement * speed*Time.fixedDeltaTime);
    }
    private IEnumerator Attack(){
        if(!attacking){
        attacking=true;
        Debug.Log("Attack");
        yield return new WaitForSeconds(attackSpeed);
        attacking=false;
        }else yield return null;

    }
    private void OpenInv(){
        if (ContainerManager.instance.inventario.activeSelf)
        {
            ContainerManager.instance.inventario.SetActive(false);
            openInv=false;
        }else
        {
            ContainerManager.instance.inventario.SetActive(true);
            ContainerManager.instance.floor.GetComponent<Suelo>().OpenFloor(itemsArea);
            openInv=true;
        }
        
    }
}
