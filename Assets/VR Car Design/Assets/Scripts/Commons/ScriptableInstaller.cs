using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ScriptableObjects;

namespace Commons
{
    [CreateAssetMenu(fileName = "Scriptable Settings", menuName = "Custom Objects/Installer/Scriptable Settings", order = 0)]
    public class ScriptableInstaller : ScriptableObjectInstaller
    {
        public MaterialScriptableObject materialScriptableObject;
        public CarScriptableObject carScriptableObject;
        public UIScriptableObject uiScriptableObject;
        public PlayerScriptableObject playerScriptableObject;
       
        public override void InstallBindings()
        {
            Container.BindInstances(materialScriptableObject);
            Container.BindInstances(carScriptableObject);            
            Container.BindInstances(uiScriptableObject);            
            Container.BindInstances(playerScriptableObject);            
        }
    }
}