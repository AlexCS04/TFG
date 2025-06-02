using UnityEngine;

public class RoomBoundary : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject leftWall;
    private GameObject rightWall;
    private GameObject topWall;
    private GameObject bottomWall;
    void Start()
    {
        leftWall = transform.GetChild(0).gameObject;
        rightWall = transform.GetChild(1).gameObject;
        topWall = transform.GetChild(2).gameObject;
        bottomWall = transform.GetChild(3).gameObject;

        leftWall.transform.localPosition = new Vector3(0, RoomManager.WAGON_HEIGHT / 2f, 0);
        leftWall.GetComponent<BoxCollider2D>().size = new Vector2(.5f, RoomManager.WAGON_HEIGHT);
        rightWall.transform.localPosition = new Vector3(RoomManager.WAGON_WIDHT, RoomManager.WAGON_HEIGHT / 2f, 0);
        rightWall.GetComponent<BoxCollider2D>().size = new Vector2(.5f, RoomManager.WAGON_HEIGHT);
        topWall.transform.localPosition = new Vector3(RoomManager.WAGON_WIDHT/2f, RoomManager.WAGON_HEIGHT, 0);
        topWall.GetComponent<BoxCollider2D>().size = new Vector2(RoomManager.WAGON_WIDHT, .5f);
        bottomWall.transform.localPosition = new Vector3(RoomManager.WAGON_WIDHT/2f, 0, 0);
        bottomWall.GetComponent<BoxCollider2D>().size = new Vector2(RoomManager.WAGON_WIDHT,.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
