using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 m_Movement;
    public float speed;

    public float mochilaMaxPeso;
    public float mochilaPeso;
    public float currentSpeed;
    // [SerializeField] private List<SCT> testItemSpawn;

    private bool openInv;
    private Animator animator;

    [SerializeField] GameObject pause;
    public List<GroundItem> itemsArea;
    public List<IInteractable> interactables;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = speed;
        animator = GetComponent<Animator>();
        interactables = new List<IInteractable>();
    }


    void Update()
    {
        float horizontal = 0;
        float vertical = 0;
        if (!openInv)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
        }


        m_Movement.Set(horizontal, vertical);
        if (m_Movement.x > 0) transform.eulerAngles = new Vector3(0, 0, 0);
        else if (m_Movement.x < 0) transform.eulerAngles = new Vector3(0, 180, 0);
        if (horizontal != 0 || vertical != 0) animator.SetBool("Running", true);
        else animator.SetBool("Running", false);
        if (Input.GetKeyDown(KeyCode.I))
        {
            OpenInv();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (openInv) OpenInv();
            else OpenPause();
        }
        // if (Input.GetKeyDown(KeyCode.J))
        // {
        //     ItemSpwnManager.instance.SpawnItem(testItemSpawn, transform.position);
        // }
        if (Input.GetButton("Fire1") && !openInv)
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.F)&&!openInv)
        {
            for (int i = interactables.Count - 1; i >= 0; i--)
            {
                IInteractable ii = interactables[i];
                if (ii.Interact()){ if(interactables.Contains(ii)) interactables.RemoveAt(i); } //Necesario por ShopItem

            }
        }
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + m_Movement * currentSpeed * Time.fixedDeltaTime);
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
    private void OpenPause()
    {
        if (pause == null) return;
        if (pause.activeSelf)
        {
            pause.GetComponent<Pause>().ClosePause();
        }
        else
        {
            pause.GetComponent<Pause>().OpenPause();
        }

    }
    private void Attack()
    {
        if (!GetComponent<Attack>()) return;
        GetComponent<Attack>().AttackAction(Camera.main.ScreenToWorldPoint(Input.mousePosition));

    }

}
