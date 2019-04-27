using System.Collections;
using System.Collections.Generic;
using GameSystem;
using UnityEngine;

namespace GazeSystem
{
    public interface IGazeService
    {
        void OnTick();
        void SetPlayerReference(GameObject player);
        void SetGameServiceRef(GameService gameService);
    }
}