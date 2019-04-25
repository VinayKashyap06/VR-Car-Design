using UnityEngine;
using System.Collections;

namespace Commons
{
    [CreateAssetMenu(fileName ="CarScriptableObject",menuName ="Custom Objects/Car Prefab",order =0)]
    public class CarScriptableObject : ScriptableObject
    {
        public GameObject car;
    }
}