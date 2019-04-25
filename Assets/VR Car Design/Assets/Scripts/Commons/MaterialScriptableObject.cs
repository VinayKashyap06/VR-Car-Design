using UnityEngine;
using System.Collections.Generic;

namespace Commons
{
    [CreateAssetMenu(fileName ="MaterialScriptableObject",menuName ="Custom Objects/Material List",order =0)]
    public class MaterialScriptableObject : ScriptableObject
    {
        public List<MaterialListStructure> materials = new List<MaterialListStructure>();
    }
}