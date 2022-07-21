using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jam
{
    public class AutomaticTurntable : MonoBehaviour
    {
        bool isTurning = false;

        public bool IsTurning { get => isTurning; set => isTurning = value; }

        void Start()
        {

        }

        void Update()
        {
            if (isTurning)
            {
                transform.Rotate(0, 1, 0);
            }
        }
    }
}
