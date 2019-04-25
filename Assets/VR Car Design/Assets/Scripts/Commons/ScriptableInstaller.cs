using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UISystem;

namespace Commons
{
    [CreateAssetMenu(fileName = "Scriptable Settings", menuName = "Custom Objects/Installer/Scriptable Settings", order = 0)]
    public class ScriptableInstaller : ScriptableObjectInstaller
    {
        public MaterialScriptableObject materialScriptableObject;
        public CarScriptableObject carScriptableObject;
       
        public override void InstallBindings()
        {
            Container.BindInstances(materialScriptableObject);
            Container.BindInstances(carScriptableObject);            
        }
    }
}