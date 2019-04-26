using UnityEngine;
using System.Collections;
using Zenject;

namespace UISystem
{
    public interface IUIService
    {
        void SetMaterial(MaterialTypeEnum materialTypeEnum);
        void SetColor(Color color);
        SignalBus GetSignalBus();
    }
}