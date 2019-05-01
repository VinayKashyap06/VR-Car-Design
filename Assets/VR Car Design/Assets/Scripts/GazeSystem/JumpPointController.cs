using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GazeSystem
{
    public class JumpPointController : MonoBehaviour, IJumpable
    {
        public void DisableJumpPoint()
        {
            gameObject.SetActive(false);
        }

        public void EnableJumpPoint()
        {
            gameObject.SetActive(true);
        }

        public Vector3 GetPosition()
        {
            return this.transform.position;
        }
    }
}