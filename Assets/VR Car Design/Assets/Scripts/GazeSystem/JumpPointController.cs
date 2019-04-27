using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GazeSystem
{
    public class JumpPointController : MonoBehaviour, IJumpable
    {
        public void DisableJumpPoint()
        {
            this.gameObject.SetActive(false);
        }

        public void EnableJumpPoint()
        {
            this.gameObject.SetActive(true);
        }

        public Vector3 GetPosition()
        {
            return this.transform.position;
        }
    }
}