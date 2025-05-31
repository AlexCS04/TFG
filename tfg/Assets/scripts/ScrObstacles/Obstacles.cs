using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Obstacles", menuName = "Scriptable Objects/Obstacles")][System.Serializable]
public class RulesObs : ScriptableObject //obstaculos
{
    public GameObject prefab;
    public List<Sprite> sprites;
    public Vector2 minPosition;
    public Vector2 maxPosition;

    public bool porcentajePos;
    public Vector2 minPerPos;
    public Vector2 maxPerPos;

    [Space]
    public float minRotation;
    public float maxRotation;
    public float distanciaObjetos;

}
[CreateAssetMenu(fileName = "Tematicas", menuName = "Scriptable Tematicas/Tematica")][System.Serializable]
public class Tematica: ScriptableObject{

    public string nombre;
    public List<Tematica> siguientesTematicas;
    

    public List<Conjunto> conjuntos;

}
[CreateAssetMenu(fileName = "Tematicas", menuName = "Scriptable Tematicas/Conjunto")][System.Serializable]
public class Conjunto: ScriptableObject
{
        public string nombre;
        public List<RulesObs> obstacles;

        public List<GameObject> enemies;
}
