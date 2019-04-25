﻿using UnityEngine;
using System.Collections;

namespace UISystem
{
    [CreateAssetMenu(fileName ="UI Elements",menuName ="Custom Objects/UI Prefabs",order =0)]
    public class UIScriptableObject : ScriptableObject
    {
        public GameObject menuUIPrefab;
        public GameObject customizeCanvas;
    }
}