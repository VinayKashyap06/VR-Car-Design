using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UISystem;
using GameSystem;
using GazeSystem;

namespace Commons
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<PerformButtonFunctionSignal>();

            Container.Bind<IUIService>().
                To<UIService>().
                AsSingle().
                NonLazy();
            Container.Bind(typeof(IInitializable),typeof(IGazeSystem)).
                To<GazeSystemController>().
                AsSingle().
                NonLazy();
            Container.Bind(typeof(IInitializable),typeof(IGameService)).
                To<GameService>().
                AsSingle().
                NonLazy();            
                
        }
    }
}