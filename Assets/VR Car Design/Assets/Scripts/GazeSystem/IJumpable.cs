using UnityEngine;
namespace GazeSystem
{
    public interface IJumpable
    {
        Vector3 GetPosition();
        void DisableJumpPoint();
        void EnableJumpPoint();
    }
}