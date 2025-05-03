using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    public static RoomManager instance{get; private set;}

    public static System.Random random= new System.Random();
    public int seed;
    public bool setseed;

    public int wagonCount;
    public GameObject vagonVacio;
    public Tematica tematica;
    [SerializeField] private List<Tematica> tematicas;
    
    private GameObject[] wagonList= new GameObject[5];
    private List<Bounds> areasRestringidas= new List<Bounds>{
        
    };

    const int WAGONS = 5;
    const int WAGON_WIDHT = 16;
    const int WAGON_HEIGHT = 9;

    const int CAMBIO_TEAMATICA=8;

    void Awake()
    {
        instance=this;
        
    }
    
    void Start()
    {
        if(setseed) random = new System.Random(seed);
        tematica = tematicas[random.Next(tematicas.Count)];
        GenerarSala();

    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            GenerarSala();
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
        areasRestringidas= new List<Bounds>{
            new Bounds(new Vector3(1+WAGON_WIDHT*(wagonCount%WAGONS), 4.5f, 0), new Vector3(2, 3, 0)), 
            new Bounds(new Vector3(15+WAGON_WIDHT*(wagonCount%WAGONS), 4.5f, 0), new Vector3(2, 3, 0)), 
            new Bounds(new Vector3(8+WAGON_WIDHT*(wagonCount%WAGONS), 0.5f, 0), new Vector3(2, 1, 0)), 
            new Bounds(new Vector3(8+WAGON_WIDHT*(wagonCount%WAGONS), 8.5f, 0), new Vector3(2, 1, 0)), 
        };

        Destroy(wagonList[wagonCount%WAGONS]);
        wagonList[wagonCount%WAGONS]=v;
        CambioTematica();
        Conjunto c = SeleccionConjunto();
        SalaEspecial();
        ColocarObstaculos(c);
        if(spwnEnemigos) GenerarEnemigos(c);

        wagonCount++;

    }
    private void CambioTematica(){
        if(wagonCount%CAMBIO_TEAMATICA==0){
            tematica= tematica.siguientesTematicas[random.Next(tematica.siguientesTematicas.Count)];
        }


    }
    private Conjunto SeleccionConjunto(){
        return tematica.conjuntos[random.Next(tematica.conjuntos.Count)];
    }
    private void SalaEspecial(){

        if(wagonCount==0){
            VagonInicial();
            spwnEnemigos=false;
        }
        else if(wagonCount%seed==0){}//base para las salas especiales, seed esta para que no de error la sintaxis


    }
    private void ColocarObstaculos(Conjunto c){
        List<RulesObs> obstColocar=c.obstacles;
        int obstCount= random.Next(6,10);
        
        for (int i = 0; i < obstCount; i++)
        {
            RulesObs obst=c.obstacles[random.Next(c.obstacles.Count)];
            int intentos=0;
            Vector3 position;
            do
            {
                position=new Vector3(
                    random.Next(obst.minPosition.x,obst.maxPosition.x)+WAGON_WIDHT*(wagonCount%WAGONS),
                    random.Next(obst.minPosition.y,obst.maxPosition.y),
                    0

                );
                intentos++;
            } while ((!PosicionValida(position, obst.prefab) || EnAreaRestringida(position)) && intentos<100);
            if(intentos<100){

                Quaternion rotation = Quaternion.Euler(0,0,random.Next(obst.minRotation,obst.maxRotation));
                GameObject temp = Instantiate(obst.prefab, position, rotation);
                temp.GetComponent<SpriteRenderer>().sprite=obst.sprites[random.Next(obst.sprites.Count)];
                //temp.transform.localScale=new Vector3(1,1,1);
                temp.transform.parent=wagonList[wagonCount%WAGONS].transform;
                obstColocados.Add(temp);
            }

        }


    }
    private void GenerarEnemigos(Conjunto c){
        List<GameObject> enemColocar=c.enemies;
        int enemCount= random.Next(6,10);
        
        for (int i = 0; i < enemCount; i++)
        {
            GameObject enem=c.enemies[random.Next(c.enemies.Count)];
            int intentos=0;
            Vector3 position;
            do
            {
                position=new Vector3(
                    random.Next(WAGON_WIDHT)+WAGON_WIDHT*(wagonCount%WAGONS),
                    random.Next(WAGON_HEIGHT),
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
        
        //Debug.Log("pos: "+ position);
        //Debug.Log("size: "+ obst.GetComponent<Collider2D>().bounds.size);       
        float x1=position.x-obst.GetComponent<Collider2D>().bounds.size.x/2;
        float y1=position.y-obst.GetComponent<Collider2D>().bounds.size.y/2;
        float x2=position.x+obst.GetComponent<Collider2D>().bounds.size.x/2;
        float y2=position.y+obst.GetComponent<Collider2D>().bounds.size.y/2;
        foreach (GameObject item in obstColocados)
        {
            //Debug.Log("Opos: "+ item.transform.position);
            //Debug.Log("Osize: "+ item.GetComponent<Collider2D>().bounds.size);    
            float a1=item.transform.position.x-item.GetComponent<Collider2D>().bounds.size.x/2;
            float b1=item.transform.position.y-item.GetComponent<Collider2D>().bounds.size.y/2;
            float a2=item.transform.position.x+item.GetComponent<Collider2D>().bounds.size.x/2;
            float b2=item.transform.position.y+item.GetComponent<Collider2D>().bounds.size.y/2;
            
            if(a2>x1)
                if (a1<x2)
                    if(b2>y1)
                        if (b1<y2)
                            return false;
            
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


    private void VagonInicial(){}

void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Color para las áreas prohibidas
        foreach (Bounds area in areasRestringidas)
        {
            Gizmos.DrawWireCube(area.center, area.size);
        }
    }


}
