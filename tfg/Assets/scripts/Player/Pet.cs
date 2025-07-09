using System.Collections;
using UnityEngine;

public class Pet : MonoBehaviour, IInteractable
{
    Vector2 destination;
    [SerializeField] float distance;
    [SerializeField] float speed;
    Rigidbody2D rb;
    Animator anim;
    [SerializeField]GameObject petPart;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        StartCoroutine("RandSpeak");
    }
    void Update()
    {
        if (Vector2.Distance((Vector2)RoomManager.instance.player.position, (Vector2)transform.position) > distance)
        {
            anim.SetBool("IsWalking", true);
            destination = RoomManager.instance.player.position;

        }
        else
        {
            anim.SetBool("IsWalking", false);
            destination = transform.position;
        }
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
        if (direction.x > 0) transform.eulerAngles = new Vector3(0, 0, 0);
        else if (direction.x < 0)transform.eulerAngles = new Vector3(0, 180, 0);
    }

    public bool Interact()
    {
        AudioManager.PlaySound(EffectTypes.pet);
        Instantiate(petPart, transform.position, Quaternion.identity);
        return true;
    }
    IEnumerator RandSpeak()
    {
        yield return new WaitForSeconds(Random.Range(11.6f, 19.8f));
        AudioManager.PlaySound(EffectTypes.bark);
        StartCoroutine("RandSpeak");

    }
}