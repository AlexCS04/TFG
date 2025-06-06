using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 m_Movement;
    public float speed;
    public float currentSpeed;
    [SerializeField] private List<SCT> testItemSpawn;

    private bool openInv;


    public List<GroundItem> itemsArea;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = speed;
    }


    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");


        m_Movement.Set(horizontal, vertical);
        if (m_Movement.x > 0) transform.eulerAngles = new Vector3(0, 0, 0);
        else if (m_Movement.x < 0) transform.eulerAngles = new Vector3(0, 180, 0);
        if (Input.GetKeyDown(KeyCode.I))
        {
            OpenInv();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            ItemSpwnManager.instance.SpawnItem(testItemSpawn, transform.position);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            GetComponent<PlayerHealth>().TakeDamage(2, 1);
        }

    }
    void FixedUpdate()
    {
        if (!openInv)
        {
            rb.MovePosition(rb.position + m_Movement * currentSpeed * Time.fixedDeltaTime);
        }
    }

    private void OpenInv()
    {
        if (ContainerManager.instance.inventario.activeSelf)
        {
            ContainerManager.instance.CloseInventory();
            openInv = false;
        }
        else
        {
            ContainerManager.instance.OpenInventory(itemsArea);
            openInv = true;
        }

    }
    private void Attack()
    {
        if (!GetComponent<Attack>()) return;
        GetComponent<Attack>().AttackAction();

    }
}
