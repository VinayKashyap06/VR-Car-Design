using UnityEngine;
using GazeSystem;

namespace GameSystem
{
    public interface IGameService
    {
       // void PerformJump(Vector3 position);
        void PerformJump(IJumpable jumpView);
    }
}