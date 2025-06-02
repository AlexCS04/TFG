using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Conjunto", menuName = "Scriptable Tematicas/Conjunto")][System.Serializable]
public class Conjunto: ScriptableObject
{
        public string nombre;
        public List<RulesObs> obstacles;

        public List<GameObject> enemies;
}
