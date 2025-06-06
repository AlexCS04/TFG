using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    public static RoomManager instance{get; private set;}
    public Transform player;

    public string seed;
    public bool setseed;

    public int wagonCount;
    public int actualWagon;
    public GameObject vagonVacio;

    public CinemachineConfiner2D confiner;
    public Tematica tematica;
    [SerializeField] private List<Tematica> tematicas;
    
    private GameObject[] wagonList= new GameObject[WAGONS];
    public BoxCollider2D[] wagonCameraBounds= new BoxCollider2D[WAGONS];
    private List<Bounds> areasRestringidas= new List<Bounds>{
        
    };

    public const int WAGONS = 5;
    public int numWagons;
    public const int WAGON_WIDHT = 24;
    public const int WAGON_HEIGHT = 12;

    const int CAMBIO_TEAMATICA=8;

    void Awake()
    {
        instance = this;
        wagonList= new GameObject[WAGONS];
        wagonCameraBounds= new BoxCollider2D[WAGONS];
        GenerateRandomSeed();
        ReadSeed();
    }
    private void ReadSeed()
    {
        seed = PlayerPrefs.GetString("Seed");
        if (PlayerPrefs.GetInt("RandomSeed") == 1) setseed = true;
    }
    private void GenerateRandomSeed()
    {
        int tSeed = (int)System.DateTime.Now.Ticks;
        Random.InitState(tSeed);
    }
    public void SetSeed()
    {
        int tSeed;
        if (int.TryParse(seed, out int n)) { tSeed = System.Int32.Parse(seed); }
        else { tSeed = seed.GetHashCode(); }
        Random.InitState(tSeed);
    }

    void Start()
    {
        if (setseed) SetSeed();
        PlayerPrefs.SetString("Seed", "");
        PlayerPrefs.SetInt("RandomSeed", 0);
        tematica = tematicas[Random.Range(0,tematicas.Count)];
        GenerarSala();
        //GenerarSala();
        confiner.BoundingShape2D = wagonCameraBounds[0];

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ClearedRoom();
        }

    }
    bool spwnEnemigos;
    List<GameObject> obstColocados=new List<GameObject>();
    public void GenerarSala(){
        spwnEnemigos=true;
        obstColocados=new List<GameObject>();
        List<GameObject> obstaculosColocados=new List<GameObject>();
        Vector2 position = new Vector2(WAGON_WIDHT*(wagonCount%WAGONS), 0);
        GameObject v = Instantiate(vagonVacio, position, Quaternion.identity);
        if (numWagons < WAGONS) numWagons += 1;
        areasRestringidas = new List<Bounds>{
            new Bounds(new Vector3(1+WAGON_WIDHT*(wagonCount%WAGONS), WAGON_HEIGHT/2f, 0), new Vector3(2, 3, 0)), 
            new Bounds(new Vector3((WAGON_WIDHT-1)+WAGON_WIDHT*(wagonCount%WAGONS), WAGON_HEIGHT/2f, 0), new Vector3(2, 3, 0)), 
            new Bounds(new Vector3((WAGON_WIDHT/2)+WAGON_WIDHT*(wagonCount%WAGONS), 0.5f, 0), new Vector3(2, 1, 0)), 
            new Bounds(new Vector3((WAGON_WIDHT/2)+WAGON_WIDHT*(wagonCount%WAGONS), WAGON_HEIGHT-0.5f, 0), new Vector3(2, 1, 0)), 
        };

        BoxCollider2D b = v.GetComponentInChildren<BoxCollider2D>();
        v.transform.GetChild(1).GetComponent<Doors>().PlaceDoor();
        v.transform.GetChild(2).GetComponent<Doors>().PlaceDoor();
        v.transform.GetChild(2).GetComponent<Doors>().SetRoomDoor(true);
        b.size = new Vector2(WAGON_WIDHT, WAGON_HEIGHT);
        b.offset = new Vector2(WAGON_WIDHT, WAGON_HEIGHT)/2;
        Destroy(wagonList[wagonCount%WAGONS]);
        wagonList[wagonCount%WAGONS]=v;
        Destroy(wagonCameraBounds[wagonCount%WAGONS]);
        wagonCameraBounds[wagonCount%WAGONS]=b;
        CambioTematica();
        Conjunto c = SeleccionConjunto();
        SalaEspecial();
        ColocarObstaculos(c);
        if(spwnEnemigos) GenerarEnemigos(c);

        wagonCount++;

    }
    private void CambioTematica(){
        if(wagonCount%CAMBIO_TEAMATICA==0){
            tematica= tematica.siguientesTematicas[Random.Range(0,tematica.siguientesTematicas.Count)];
        }


    }
    private Conjunto SeleccionConjunto(){
        return tematica.conjuntos[Random.Range(0,tematica.conjuntos.Count)];
    }
    private void SalaEspecial(){

        if(wagonCount==0){
            VagonInicial();
            spwnEnemigos=false;
        }
        else if(wagonCount%1==0){}//base para las salas especiales, 1 esta para que no de error la sintaxis


    }
    private void ColocarObstaculos(Conjunto c){
        List<RulesObs> obstColocar=c.obstacles;
        int obstCount= Random.Range(6,10);
        
        for (int i = 0; i < obstCount; i++)
        {
            RulesObs obst=c.obstacles[Random.Range(0,c.obstacles.Count)];
            int intentos=0;
            Vector3 position;
            do
            {
                if(!obst.porcentajePos)
                position =new Vector3(
                    Random.Range(obst.minPosition.x,obst.maxPosition.x)+WAGON_WIDHT*(wagonCount%WAGONS),
                    Random.Range(obst.minPosition.y,obst.maxPosition.y),
                    0

                );
                else
                position =new Vector3(
                    Random.Range(WAGON_WIDHT*obst.minPerPos.x,WAGON_WIDHT*obst.maxPerPos.x)+WAGON_WIDHT*(wagonCount%WAGONS),
                    Random.Range(WAGON_HEIGHT*obst.minPerPos.y,WAGON_HEIGHT*obst.maxPerPos.y),
                    0

                );
                intentos++;
            } while ((!PosicionValida(position, obst.prefab) || EnAreaRestringida(position)) && intentos<100);
            if(intentos<100){

                Quaternion rotation = Quaternion.Euler(0,0,Random.Range(obst.minRotation,obst.maxRotation));
                GameObject temp = Instantiate(obst.prefab, position, rotation);
                temp.GetComponent<SpriteRenderer>().sprite=obst.sprites[Random.Range(0,obst.sprites.Count)];
                //temp.transform.localScale=new Vector3(1,1,1);
                temp.transform.parent=wagonList[wagonCount%WAGONS].transform;
                obstColocados.Add(temp);
            }

        }


    }
    private void GenerarEnemigos(Conjunto c){
        List<GameObject> enemColocar=c.enemies;
        int enemCount= Random.Range(6,10);
        
        for (int i = 0; i < enemCount; i++)
        {
            GameObject enem=enemColocar[Random.Range(0,enemColocar.Count)];
            int intentos=0;
            Vector3 position;
            do
            {
                position=new Vector3(
                    Random.Range(WAGON_WIDHT*(wagonCount%WAGONS),.9f*WAGON_WIDHT+WAGON_WIDHT*(wagonCount%WAGONS)),
                    Random.Range(0.25f,WAGON_HEIGHT*.9f),
                    0

                );
                intentos++;
            } while ((!PosicionValida(position, enem) || EnAreaRestringida(position)) && intentos<100);
            if(intentos<100){
                
                GameObject e =Instantiate(enem, position, Quaternion.identity);
                e.transform.parent=wagonList[wagonCount%WAGONS].transform;
            }

        }
    }

    private bool PosicionValida(Vector3 position, GameObject obst){
        
        GameObject e =Instantiate(obst, position, Quaternion.identity);
        // Debug.Log("pos: "+ position);
        // Debug.Log("size: "+ e.GetComponent<Collider2D>().bounds.size);       
        float x1=position.x-e.GetComponent<Collider2D>().bounds.size.x/2;
        float y1=position.y-e.GetComponent<Collider2D>().bounds.size.y/2;
        float x2=position.x+e.GetComponent<Collider2D>().bounds.size.x/2;
        float y2=position.y+e.GetComponent<Collider2D>().bounds.size.y/2;
        Destroy(e);
        foreach (GameObject item in obstColocados)
        {
            // Debug.Log("Opos: " + item.transform.position);
            // Debug.Log("Osize: " + item.GetComponent<Collider2D>().bounds.size);
            float a1 = item.transform.position.x - item.GetComponent<Collider2D>().bounds.size.x / 2;
            float b1 = item.transform.position.y - item.GetComponent<Collider2D>().bounds.size.y / 2;
            float a2 = item.transform.position.x + item.GetComponent<Collider2D>().bounds.size.x / 2;
            float b2 = item.transform.position.y + item.GetComponent<Collider2D>().bounds.size.y / 2;

            if (a2 > x1)
                if (a1 < x2)
                    if (b2 > y1)
                        if (b1 < y2)
                        {
                            // Debug.Log("Try again");
                            return false;
                        }

        }


        return true;
    }
    private bool EnAreaRestringida(Vector3 position){
        
        foreach (Bounds area in areasRestringidas)
        {
            if (area.Contains(position))
            {
                return true;
            }
        }
        return false;

    }


    private void VagonInicial()
    {

        ClearedRoom();
    }
    public void ClearedRoom()
    {
        wagonList[actualWagon].transform.GetChild(1).GetComponent<Doors>().SetRoomClear(true);
        wagonList[actualWagon].transform.GetChild(2).GetComponent<Doors>().SetRoomClear(true);
    }

void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Color para las áreas prohibidas
        foreach (Bounds area in areasRestringidas)
        {
            Gizmos.DrawWireCube(area.center, area.size);
        }
    }


}
