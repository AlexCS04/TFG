using UnityEngine;

public class Pet : MonoBehaviour, IInteractable
{
    Vector2 destination;
    float distance = 2.8f;
    float speed = 6;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Vector2.Distance((Vector2)RoomManager.instance.player.position, (Vector2)transform.position) > distance)
        {
            // Vector3 randomOffset = Random.insideUnitSphere * 2f;
            destination = RoomManager.instance.player.position; //+ randomOffset;
        }
        else destination = transform.position;
    }
    void FixedUpdate()
    {
        Vector2 direction = (destination - (Vector2)transform.position).normalized;
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        FaceDir();
    }
    private void FaceDir()
    {
        Vector2 direction = (destination - (Vector2)transform.position).normalized;
        if (direction.x >= 0) transform.eulerAngles = new Vector3(0, 0, 0);
        else transform.eulerAngles = new Vector3(0, 180, 0);
    }

    public bool Interact()
    {
        Debug.Log("Petted");
        return false;
    }
}