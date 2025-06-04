using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class Doors : MonoBehaviour
{

    private CinemachineConfiner2D confiner;
    [SerializeField] private float moveP;

    private bool lastRoom;
    private bool roomClear;

    public Direction dir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        confiner = RoomManager.instance.confiner;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (roomClear)
            {
                Vector3 newPos = collision.gameObject.transform.position;
                switch (dir)
                {
                    case Direction.Up:
                    case Direction.Down:
                        Debug.Log("Wrong assigment");
                        goto case Direction.Left;
                    case Direction.Left:
                        if (RoomManager.instance.actualWagon == 0) RoomManager.instance.actualWagon = RoomManager.instance.numWagons;
                        RoomManager.instance.actualWagon -= 1;
                        confiner.BoundingShape2D = RoomManager.instance.wagonCameraBounds[RoomManager.instance.actualWagon];
                        newPos.x = RoomManager.WAGON_WIDHT + RoomManager.WAGON_WIDHT * (RoomManager.instance.actualWagon % RoomManager.WAGONS) - moveP;
                        break;
                    case Direction.Right:
                        RoomManager.instance.actualWagon += 1;
                        if (RoomManager.instance.actualWagon == RoomManager.WAGONS) RoomManager.instance.actualWagon = 0;
                        confiner.BoundingShape2D = RoomManager.instance.wagonCameraBounds[RoomManager.instance.actualWagon];
                        newPos.x = moveP + RoomManager.WAGON_WIDHT * (RoomManager.instance.actualWagon % RoomManager.WAGONS);
                        if (lastRoom)
                        {
                            RoomManager.instance.GenerarSala();
                            confiner.BoundingShape2D = RoomManager.instance.wagonCameraBounds[RoomManager.instance.actualWagon];
                            lastRoom = false;
                        }

                        break;


                }
                collision.gameObject.transform.position = newPos;
                StartCoroutine("PincheCineMachine");
            }
        }
    }
    IEnumerator PincheCineMachine()
    {
        confiner.gameObject.SetActive(false);
        yield return null;
        confiner.gameObject.SetActive(true);
    }
    public void SetRoomDoor(bool b) { lastRoom = b; }  //The door that triggers a new wagon
    public void SetRoomClear(bool b) { roomClear = b; }

    public void PlaceDoor()
    {
        switch (dir)
        {
            case Direction.Up:
                transform.localPosition = new Vector3(RoomManager.WAGON_WIDHT/2f, RoomManager.WAGON_HEIGHT, 0);
                break;
            case Direction.Down:
                transform.localPosition = new Vector3(RoomManager.WAGON_WIDHT/2f, 0, 0);
                break;
            case Direction.Left:
                transform.localPosition = new Vector3(0, RoomManager.WAGON_HEIGHT / 2f, 0);
                break;
            case Direction.Right:
                transform.localPosition = new Vector3(RoomManager.WAGON_WIDHT, RoomManager.WAGON_HEIGHT / 2f, 0);
                break;
        }
        
    }
}
public enum Direction
{
    Up,
    Down,
    Left,
    Right
}
