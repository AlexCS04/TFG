using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Tematicas", menuName = "Scriptable Tematicas/Tematica")]
[System.Serializable]
public class Tematica : ScriptableObject
{

    public string nombre;
    public List<Tematica> siguientesTematicas;
    public List<Sprite> enviromentSprites;


    public List<Conjunto> conjuntos;

    public GameObject boss;

    public MusicTypes ambience;
    public MusicTypes bossMus;

}