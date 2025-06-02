using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Tematicas", menuName = "Scriptable Tematicas/Tematica")][System.Serializable]
public class Tematica: ScriptableObject{

    public string nombre;
    public List<Tematica> siguientesTematicas;
    

    public List<Conjunto> conjuntos;

}