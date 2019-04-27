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
            Container.DeclareSignal<SceneChangeSignal>();

            Container.Bind<IUIService>().
                To<UIService>().
                AsSingle().
                NonLazy();
            Container.Bind<IGazeService>().
                To<GazeService>().
                AsSingle().
                NonLazy();
            Container.Bind<IGameService>().
                To<GameService>().
                AsSingle().
                NonLazy();            
                
        }
    }
}