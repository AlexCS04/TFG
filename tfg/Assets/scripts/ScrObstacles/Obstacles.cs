using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Obstacles", menuName = "Scriptable Objects/Obstacles")]
[System.Serializable]
public class RulesObs : ScriptableObject //obstaculo
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

    public float health;

    public List<SCT> pool;

}
