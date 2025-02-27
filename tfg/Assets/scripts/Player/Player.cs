using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 m_Movement;
    private bool attacking;
    [SerializeField] private float speed;
    [SerializeField] private float attackSpeed;
    public float regenHealth;
    public float trueHealth;

    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");


        m_Movement.Set(horizontal, vertical);
        // m_Movement.Normalize();

        if(Input.GetAxisRaw("Fire1")!=0){
            StartCoroutine("Attack");
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
}
