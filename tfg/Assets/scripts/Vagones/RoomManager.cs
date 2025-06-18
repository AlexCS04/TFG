using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    public static RoomManager instance{get; private set;}
    public Transform player;

    public System.Random roomRandom;
    public string seed;
    public bool setseed;

    public int wagonCount;
    public int actualWagon;
    public GameObject vagonVacio;

    public CinemachineConfiner2D confiner;
    public Tematica tematica;
    [SerializeField] private List<Tematica> tematicas;
    
    public GameObject[] wagonList= new GameObject[WAGONS];
    public BoxCollider2D[] wagonCameraBounds= new BoxCollider2D[WAGONS];
    public RoomGrid[] wagonGrid= new RoomGrid[WAGONS];
    private List<Bounds> areasRestringidas= new List<Bounds>{
        
    };
    private int enemiesInRoom;

    public const int WAGONS = 5;
    public int numWagons;
    public const int WAGON_WIDHT = 24;
    public const int WAGON_HEIGHT = 12;

    const int CAMBIO_TEAMATICA=8;

    void OnEnable()
    {
        Eventmanager.EnemyDeathEvent += EnemyDeath;
    }
    void OnDisable()
    {
        Eventmanager.EnemyDeathEvent -= EnemyDeath;
    }
    private void EnemyDeath()
    {
        enemiesInRoom -= 1;
        if (enemiesInRoom <= 0) ClearedRoom();
    }

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
        roomRandom=new System.Random(tSeed);
    }
    public void SetSeed()
    {
        int tSeed;
        if (int.TryParse(seed, out int n)) { tSeed = System.Int32.Parse(seed); }
        else { tSeed = seed.GetHashCode(); }
        roomRandom=new System.Random(tSeed);
    }

    void Start()
    {
        if (setseed) SetSeed();
        PlayerPrefs.SetString("Seed", "");
        PlayerPrefs.SetInt("RandomSeed", 0);
        // tematica = tematicas[Random.Range(0,tematicas.Count)];
        tematica = tematicas[roomRandom.Next(0, tematicas.Count)];
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
        if (Input.GetKeyDown(KeyCode.N))
        {
            
        }

    }
    public IEnumerator PincheCineMachine()
    {
        confiner.gameObject.SetActive(false);
        yield return null;
        confiner.gameObject.SetActive(true);
    }
    bool spwnEnemigos;
    List<GameObject> obstColocados=new List<GameObject>();
    public void GenerarSala()
    {
        spwnEnemigos = true;
        obstColocados = new List<GameObject>();
        List<GameObject> obstaculosColocados = new List<GameObject>();
        Vector2 position = new Vector2(WAGON_WIDHT * (wagonCount % WAGONS), 0);
        GameObject v = Instantiate(vagonVacio, position, Quaternion.identity);
        if (numWagons < WAGONS) numWagons += 1;
        areasRestringidas = new List<Bounds>{
            new Bounds(new Vector3(1+WAGON_WIDHT*(wagonCount%WAGONS), WAGON_HEIGHT/2f, 0), new Vector3(2, 3, 0)),
            new Bounds(new Vector3((WAGON_WIDHT-1)+WAGON_WIDHT*(wagonCount%WAGONS), WAGON_HEIGHT/2f, 0), new Vector3(2, 3, 0)),
            new Bounds(new Vector3((WAGON_WIDHT/2)+WAGON_WIDHT*(wagonCount%WAGONS), 0.5f, 0), new Vector3(2, 1, 0)),
            new Bounds(new Vector3((WAGON_WIDHT/2)+WAGON_WIDHT*(wagonCount%WAGONS), WAGON_HEIGHT-0.5f, 0), new Vector3(2, 1, 0)),
        };

        BoxCollider2D b = v.transform.GetChild(0).GetComponent<BoxCollider2D>();
        v.transform.GetChild(1).GetComponent<Doors>().PlaceDoor();
        v.transform.GetChild(2).GetComponent<Doors>().PlaceDoor();
        v.transform.GetChild(2).GetComponent<Doors>().SetRoomDoor(true);
        v.transform.GetChild(4).GetComponent<RoomGrid>().GenerateGrid();
        b.size = new Vector2(WAGON_WIDHT + 0.5f, WAGON_HEIGHT + 2f);
        b.offset = new Vector2(WAGON_WIDHT, WAGON_HEIGHT+1f) / 2;
        Destroy(wagonList[wagonCount % WAGONS]);
        wagonList[wagonCount % WAGONS] = v;
        wagonCameraBounds[wagonCount % WAGONS] = b;
        wagonGrid[wagonCount % WAGONS] = v.transform.GetChild(4).GetComponent<RoomGrid>();
        Conjunto c = SeleccionConjunto();
        SalaEspecial();
        ColocarObstaculos(c);
        if (spwnEnemigos) GenerarEnemigos(c);

        CambioTematica();
        wagonCount++;
        

    }
    private void CambioTematica(){
        if (wagonCount % CAMBIO_TEAMATICA == 0)
        {
            // tematica = tematica.siguientesTematicas[Random.Range(0, tematica.siguientesTematicas.Count)];
            tematica = tematica.siguientesTematicas[roomRandom.Next(0, tematica.siguientesTematicas.Count)];
        }


    }
    private Conjunto SeleccionConjunto()
    {
        // return tematica.conjuntos[Random.Range(0, tematica.conjuntos.Count)];
        return tematica.conjuntos[roomRandom.Next(0, tematica.conjuntos.Count)];
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
        int obstCount= roomRandom.Next(6,11);
        
        
        for (int i = 0; i < obstCount; i++)
        {
            RulesObs obst=c.obstacles[roomRandom.Next(0,c.obstacles.Count)];
            int intentos=0;
            Vector3 position;
            do
            {
                if(!obst.porcentajePos)
                position =new Vector3(
                    // Random.Range(obst.minPosition.x,obst.maxPosition.x)+WAGON_WIDHT*(wagonCount%WAGONS),
                    // Random.Range(obst.minPosition.y,obst.maxPosition.y),
                    (float)(roomRandom.NextDouble()*(obst.maxPosition.x-obst.minPosition.x)+obst.minPosition.x)+WAGON_WIDHT*(wagonCount%WAGONS),
                    (float)(roomRandom.NextDouble()*(obst.maxPosition.y-obst.minPosition.y)+obst.minPosition.y),
                    0

                );
                else
                position =new Vector3(
                    // Random.Range(WAGON_WIDHT*obst.minPerPos.x,WAGON_WIDHT*obst.maxPerPos.x)+WAGON_WIDHT*(wagonCount%WAGONS),
                    // Random.Range(WAGON_HEIGHT*obst.minPerPos.y,WAGON_HEIGHT*obst.maxPerPos.y),
                    (float)(roomRandom.NextDouble()*(WAGON_WIDHT*obst.maxPerPos.x-WAGON_WIDHT*obst.minPerPos.x)+WAGON_WIDHT*obst.minPerPos.x)+WAGON_WIDHT*(wagonCount%WAGONS),
                    (float)(roomRandom.NextDouble()*(WAGON_HEIGHT*obst.maxPerPos.y-WAGON_HEIGHT*obst.minPerPos.y)+WAGON_HEIGHT*obst.minPerPos.y),

                    0

                );
                intentos++;
            } while ((!PosicionValida(position, obst.prefab) || EnAreaRestringida(position)) && intentos<100);
            if(intentos<100){

                Quaternion rotation = Quaternion.Euler(0,0,(float)(roomRandom.NextDouble()*(obst.maxRotation-obst.minRotation)+obst.minRotation));
                GameObject temp = Instantiate(obst.prefab, position, rotation);
                temp.GetComponent<SpriteRenderer>().sprite=obst.sprites[roomRandom.Next(0,obst.sprites.Count)];
                temp.GetComponent<Health>().pool = obst.pool;
                temp.GetComponent<Health>().maxHealth = obst.health;
                //temp.transform.localScale=new Vector3(1,1,1);
                temp.transform.parent=wagonList[wagonCount%WAGONS].transform;
                obstColocados.Add(temp);
            }

        }


    }
    private void GenerarEnemigos(Conjunto c){
        List<GameObject> enemColocar=c.enemies;
        int enemCount= roomRandom.Next(6,11);
        
        for (int i = 0; i < enemCount; i++)
        {
            GameObject enem=enemColocar[roomRandom.Next(0,enemColocar.Count)];
            int intentos=0;
            Vector3 position;
            do
            {
                position=new Vector3(
                    // Random.Range(WAGON_WIDHT*(wagonCount%WAGONS),.9f*WAGON_WIDHT+WAGON_WIDHT*(wagonCount%WAGONS)),
                    // Random.Range(0.25f,WAGON_HEIGHT*.9f),
                    (float)(roomRandom.NextDouble()*(.9f*WAGON_WIDHT+WAGON_WIDHT*(wagonCount%WAGONS)-WAGON_WIDHT*(wagonCount%WAGONS))+WAGON_WIDHT*(wagonCount%WAGONS)),
                    (float)(roomRandom.NextDouble()*(WAGON_HEIGHT*.9f-0.25f)+0.25f),

                    0

                );
                intentos++;
            } while ((!PosicionValida(position, enem) || EnAreaRestringida(position)) && intentos<100);
            if (intentos < 100)
            {

                GameObject e = Instantiate(enem, position, Quaternion.identity);
                e.transform.parent = wagonList[wagonCount % WAGONS].transform;
                enemiesInRoom++;
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
