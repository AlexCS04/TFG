using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class Doors : MonoBehaviour
{

    private CinemachineConfiner2D confiner;
    [SerializeField] private float moveP;
    [SerializeField] private Sprite openDoor;
    [SerializeField] private Sprite closeDoor;
    private GameObject uSure;
    [SerializeField] private GameObject text;
    [SerializeField] private bool exit;

    private bool lastRoom;
    private bool roomClear;

    public Direction dir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        confiner = RoomManager.instance.confiner;
        uSure = RoomManager.instance.uSure;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (dir == Direction.Up || dir == Direction.Down)
            {

                exit = true;
                text.SetActive(true);
                return;
            }
            if (roomClear)
            {
                Vector3 newPos = collision.gameObject.transform.position;
                switch (dir)
                {

                    case Direction.Left:
                        if (RoomManager.instance.actualWagon == 0) RoomManager.instance.actualWagon = RoomManager.instance.numWagons;
                        RoomManager.instance.actualWagon -= 1;
                        confiner.BoundingShape2D = RoomManager.instance.wagonCameraBounds[RoomManager.instance.actualWagon];
                        newPos.x = RoomManager.WAGON_WIDHT + RoomManager.WAGON_WIDHT * (RoomManager.instance.actualWagon % RoomManager.WAGONS) - 2 * moveP;
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
                for (int i = 0; i < RoomManager.instance.numWagons; i++)
                {
                    if (RoomManager.instance.actualWagon == i) RoomManager.instance.wagonList[i].SetActive(true);
                    else RoomManager.instance.wagonList[i].SetActive(false);
                }
                collision.gameObject.transform.position = newPos;
                RoomManager.instance.StartCoroutine("PincheCineMachine");
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && (dir.Equals(Direction.Up) || dir.Equals(Direction.Down))) { exit = false; text.SetActive(false); }
    }
    void Update()
    {
        if (exit&&Input.GetKeyDown(KeyCode.F)) ExitDungeon();
    }
    private void ExitDungeon()
    {
        uSure.SetActive(true);
        Time.timeScale = 0;

    }
    
    public void SetRoomDoor(bool b) { lastRoom = b; }  //The door that triggers a new wagon
    public void SetRoomClear(bool b)
    {
        roomClear = b;
        if (b) GetComponent<SpriteRenderer>().sprite = openDoor;
        else GetComponent<SpriteRenderer>().sprite = closeDoor;
    }

    public void PlaceDoor()
    {
        switch (dir)
        {
            case Direction.Up:
                transform.localPosition = new Vector3(RoomManager.WAGON_WIDHT/2f, RoomManager.WAGON_HEIGHT+.7f, 0);
                break;
            case Direction.Down:
                transform.localPosition = new Vector3(RoomManager.WAGON_WIDHT/2f, .1f, 0);
                break;
            case Direction.Left:
                transform.localPosition = new Vector3(0, RoomManager.WAGON_HEIGHT / 2f, 0);
                break;
            case Direction.Right:
                transform.localPosition = new Vector3(RoomManager.WAGON_WIDHT-.5f, RoomManager.WAGON_HEIGHT / 2f, 0);
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
