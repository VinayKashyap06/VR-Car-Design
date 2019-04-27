using UnityEngine;
using System.Collections;
using GazeSystem;
using Zenject;

namespace UISystem
{
    public interface IUIService
    {
        void SetMaterial(MaterialTypeEnum materialTypeEnum);
        void SetColor(Color color);
        SignalBus GetSignalBus();
        void SetCurrentPlayerControllerRef(PlayerController playerController);
    }
}