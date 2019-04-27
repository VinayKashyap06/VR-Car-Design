using UnityEngine;
using System.Collections;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName ="PlayerScriptableObject",menuName ="Custom Objects/Player Prefab",order =0)]
    public class PlayerScriptableObject : ScriptableObject
    {
        public GameObject player;
        public GameObject jumpPointPrefab;
    }
}