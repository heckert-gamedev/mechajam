using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jam
{
    public class BasesManager : MonoBehaviour
    {
        List<AutomaticTurntable> exhibits = new List<AutomaticTurntable>();
        Vector2 _currentSelection = Vector3.zero;
        [SerializeField] float spacing = 32f;

        AutomaticTurntable _currentTurntable;

        public AutomaticTurntable CurrentTurntable { get => _currentTurntable; set => _currentTurntable = value; }

        void Start()
        {
            AutomaticTurntable at;
            foreach (Transform t in transform)
            {
                at = t.GetComponent<AutomaticTurntable>();
                if (at != null)
                {
                    exhibits.Add(at);
                }
            }
            _currentTurntable = exhibits[0];
            _currentTurntable.IsTurning = true;
        }

        internal bool GetTargetPosition(Vector3 otherPosition, Vector2 moveInput, out Vector3 targetPosition)
        {
            Vector3 checkPosition = otherPosition;
            checkPosition.y = 1f;
            checkPosition.x += spacing * moveInput.x;
            checkPosition.z += spacing * moveInput.y;

            Debug.Log($"Check position: {checkPosition}");

            foreach (AutomaticTurntable at in exhibits)
            {
                if (at.transform.position.x == checkPosition.x && at.transform.position.z == checkPosition.z)
                {
                    targetPosition = checkPosition;
                    targetPosition.y = otherPosition.y;
                    _currentTurntable.IsTurning = false;
                    _currentTurntable = at;
                    _currentTurntable.IsTurning = true;
                    return true;
                }
            }
            targetPosition = Vector3.zero;
            return false;
        }

    }
}
