using System.Collections.Generic;
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
        new Bounds(new Vector3(1, 4.5f, 0), new Vector3(2, 3, 0)), 
        new Bounds(new Vector3(15, 4.5f, 0), new Vector3(2, 3, 0)), 
        new Bounds(new Vector3(8, 0.5f, 0), new Vector3(2, 1, 0)), 
        new Bounds(new Vector3(8, 8.5f, 0), new Vector3(2, 1, 0)), 
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
        
    }
    bool spwnEnemigos;
    public void GenerarSala(){
        spwnEnemigos=true;
        List<GameObject> obstColocados=new List<GameObject>();
        List<GameObject> obstaculosColocados=new List<GameObject>();
        Vector2 position = new Vector2(WAGON_WIDHT*(wagonCount%WAGONS), 0);
        GameObject v = Instantiate(vagonVacio, position, Quaternion.identity);

        Destroy(wagonList[wagonCount%WAGONS]);
        wagonList[wagonCount%WAGONS]=v;

        CambioTematica();
        Conjunto c = SeleccionConjunto();
        Debug.Log(obstaculosColocados);
        SalaEspecial(obstaculosColocados);
        Debug.Log(obstaculosColocados);
        ColocarObstaculos(c, obstaculosColocados);
        Debug.Log(obstaculosColocados);
        if(spwnEnemigos) GenerarEnemigos(c, obstaculosColocados);

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
    private void SalaEspecial(List<GameObject>oC){

        if(wagonCount==0){
            VagonInicial();
            spwnEnemigos=false;
        }
        else if(wagonCount%seed==0){}//base para las salas especiales


    }
    private void ColocarObstaculos(Conjunto c, List<GameObject>oC){
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
                    random.Next(obst.minPosition.x,obst.maxPosition.x),
                    random.Next(obst.minPosition.y,obst.maxPosition.y),
                    0

                );
                intentos++;
            } while ((!PosicionValida(position,oC) || EnAreaRestringida(position, areasRestringidas)) && intentos<100);
            if(intentos<100){

                Quaternion rotation = Quaternion.Euler(0,0,random.Next(obst.minRotation,obst.maxRotation));
                GameObject temp = Instantiate(obst.prefab, position, rotation);
                temp.GetComponent<SpriteRenderer>().sprite=obst.sprites[random.Next(obst.sprites.Count)];
                temp.transform.localScale=new Vector3(1,1,1);
                temp.transform.parent=wagonList[wagonCount%WAGONS].transform;
                oC.Add(temp);
            }

        }


    }
    private void GenerarEnemigos(Conjunto c, List<GameObject>oC){
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
                    random.Next(WAGON_WIDHT),
                    random.Next(WAGON_HEIGHT),
                    0

                );
                intentos++;
            } while ((!PosicionValida(position,oC) || EnAreaRestringida(position, areasRestringidas)) && intentos<100);
            if(intentos<100){
                
            }

        }
    }

    private bool PosicionValida(Vector3 position, List<GameObject> oC){
        
        return true;
    }
    private bool EnAreaRestringida(Vector3 position, List<Bounds> aR){
        
        foreach (Bounds area in aR)
        {
            if (area.Contains(position))
            {
                Debug.Log(position);
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
